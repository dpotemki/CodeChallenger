using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System.Reflection;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using CodeCompilator.Service.Models;
using CodeExecutionContracts.Models;
using Newtonsoft.Json.Linq;
using System.Runtime.Loader;

namespace CodeCompilator.Service.Services
{
    public class CodeRunnerV2
    {

        public class CustomAssemblyLoadContext : AssemblyLoadContext
        {
            public CustomAssemblyLoadContext() : base(isCollectible: true)
            {
            }

            protected override Assembly Load(AssemblyName assemblyName)
            {
                return null;
            }

        }

        public async Task<ExecuteCodeResponce> ExecuteCodeAsync(string code, object[] parameters, int timeoutInSeconds, string mainMethodName)
        {
            ExecuteCodeResponce executeCodeResponce = new ExecuteCodeResponce();

            var syntaxTree = CSharpSyntaxTree.ParseText(code);
            var assemblyName = Path.GetRandomFileName();


            var references = new MetadataReference[]
{
    MetadataReference.CreateFromFile(typeof(object).GetTypeInfo().Assembly.Location), // System.Private.CoreLib.dll для базовых типов
    MetadataReference.CreateFromFile(typeof(Enumerable).GetTypeInfo().Assembly.Location), // System.Linq.dll для LINQ
    MetadataReference.CreateFromFile(typeof(List<>).GetTypeInfo().Assembly.Location), // System.Collections.dll для коллекций
    MetadataReference.CreateFromFile(typeof(Dictionary<,>).GetTypeInfo().Assembly.Location), // System.Collections.dll для словарей
    MetadataReference.CreateFromFile(typeof(Task).GetTypeInfo().Assembly.Location), // System.Threading.Tasks.dll для асинхронности
    MetadataReference.CreateFromFile(typeof(Console).GetTypeInfo().Assembly.Location), // System.Console.dll для ввода/вывода в консоль
    //MetadataReference.CreateFromFile(typeof(System.Net.Http.HttpClient).GetTypeInfo().Assembly.Location), // System.Net.Http.dll для HTTP запросов , do not use it, ddos and so on
    MetadataReference.CreateFromFile(typeof(System.Xml.XmlDocument).GetTypeInfo().Assembly.Location), // System.Xml.ReaderWriter.dll для работы с XML
    MetadataReference.CreateFromFile(typeof(System.Data.DataTable).GetTypeInfo().Assembly.Location), // System.Data.Common.dll для работы с данными
    MetadataReference.CreateFromFile(typeof(System.Text.Json.JsonSerializer).GetTypeInfo().Assembly.Location), // System.Text.Json.dll для работы с JSON
    // Add other references here
};

            var systemRuntimeAssembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == "System.Runtime");
            if (systemRuntimeAssembly != null)
            {
                references = references.Concat(new[] { MetadataReference.CreateFromFile(systemRuntimeAssembly.Location) }).ToArray();
            }
            else
            {

            }
            var compilation = CSharpCompilation.Create(
                assemblyName,
                new[] { syntaxTree },
                references,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (var ms = new MemoryStream())
            {
                EmitResult result = compilation.Emit(ms);
                if (!result.Success)
                {
                    executeCodeResponce.CompilationErrorMessage = result.Diagnostics
                        .Select(x => x.GetMessage())
                        .Aggregate((x, y) => x + "\n" + y);

                    return executeCodeResponce;
                }

                ms.Seek(0, SeekOrigin.Begin);
                CustomAssemblyLoadContext assemblyLoadContext = null;
                try
                {
                    assemblyLoadContext = new CustomAssemblyLoadContext();
                    var assembly = assemblyLoadContext.LoadFromStream(ms);
                    var type = assembly.GetTypes()[0];
                    var method = type.GetMethods().FirstOrDefault(m => m.Name == mainMethodName); // Execution main method name

                    if (method == null)
                    {
                        throw new Exception("Missing method: " + mainMethodName);
                    }
                    var stopwatch = Stopwatch.StartNew();
                    var memoryBefore = GC.GetTotalMemory(true);

                    CancellationTokenSource cts = new CancellationTokenSource();
                    var task = Task.Run(() =>
                    {
                        try
                        {
                            return method.Invoke(method.IsStatic ? null : Activator.CreateInstance(type), parameters);
                        }
                        catch (Exception ex)
                        {
                            executeCodeResponce.ErrorMessage = "Method Invoke Execution error: " + ex.Message;
                            return null;
                        }
                    }, cts.Token);

                    if (await Task.WhenAny(task, Task.Delay(timeoutInSeconds * 1000)) == task && !task.IsFaulted)
                    {
                        stopwatch.Stop();
                        var memoryAfter = GC.GetTotalMemory(false);

                        executeCodeResponce.IsSuccessful = true;
                        executeCodeResponce.Result = task.Result;
                        executeCodeResponce.ExecutionTime = stopwatch.Elapsed;
                        executeCodeResponce.MemoryUsage = memoryAfter - memoryBefore;
                    }
                    else
                    {
                        cts.Cancel();
                        executeCodeResponce.ErrorMessage = "The task did not complete on time";
                    }
                }
                catch (Exception ex)
                {
                    executeCodeResponce.ErrorMessage = "Execution error: " + ex.Message;
                }
                finally
                {
                    assemblyLoadContext?.Unload();
                }
            }

            return executeCodeResponce;
        }
        public object[] DeserializeParameters(List<InputParameter> inputParameters)
        {
            var result = new List<object>();

            foreach (var param in inputParameters)
            {
                result.Add(InputParameterToObject(param));
            }

            return result.ToArray();
        }

        public object InputParameterToObject(InputParameter inputParameter)
        {
            string type = inputParameter.Type;
            string value = inputParameter.Value;
            object result = null;
            switch (type)
            {
                case "DateTime":
                    result = DateTime.Parse(value);
                    break;
                case "int":
                    result = int.Parse(value);
                    break;
                case "long":
                    result = long.Parse(value);
                    break;
                case "double":
                    result = double.Parse(value);
                    break;
                case "bool":
                    result = bool.Parse(value);
                    break;
                case "string":
                    result = value;
                    break;
                case "int[]":
                    result = System.Text.Json.JsonSerializer.Deserialize<int[]>(value);
                    break;
                case "List<int>":
                    result = System.Text.Json.JsonSerializer.Deserialize<List<int>>(value);
                    break;
                case "string[]":
                    result = System.Text.Json.JsonSerializer.Deserialize<string[]>(value);
                    break;
                case "List<string>":
                    result = System.Text.Json.JsonSerializer.Deserialize<List<string>>(value);
                    break;
                    // Дополнительные типы можно добавлять по аналогии
            }
            return result;
        }
        public InputParameter ObjectToInputParameter(object obj)
        {
            if (obj == null) return new InputParameter { Type = "null", Value = null };

            var type = obj.GetType();
            var inputParameter = new InputParameter { Type = type.Name };

            switch (type.Name)
            {
                case "DateTime":
                    inputParameter.Value = ((DateTime)obj).ToString("o"); // ISO 8601 format
                    break;
                case "Int32":
                    inputParameter.Value = obj.ToString();
                    break;
                case "Int64":
                    inputParameter.Value = obj.ToString();
                    break;
                case "Double":
                    inputParameter.Value = obj.ToString();
                    break;
                case "Boolean":
                    inputParameter.Value = obj.ToString();
                    break;
                case "String":
                    inputParameter.Value = (string)obj;
                    break;
                case "Int32[]":
                    inputParameter.Value = System.Text.Json.JsonSerializer.Serialize(obj);
                    break;
                case "List`1":
                    if (obj is List<int>)
                        inputParameter.Type = "List<int>";
                    else if (obj is List<string>)
                        inputParameter.Type = "List<string>";
                    inputParameter.Value = System.Text.Json.JsonSerializer.Serialize(obj);
                    break;
                case "String[]":
                    inputParameter.Value = System.Text.Json.JsonSerializer.Serialize(obj);
                    break;
                    // Дополнительные типы можно добавлять по аналогии
            }
            return inputParameter;
        }
        public bool CompareResults(object actual, object expected)
        {
            if (actual == null || expected == null)
                return actual == expected;

            Type actualType = actual.GetType();
            Type expectedType = expected.GetType();

            if (actualType != expectedType)
                return false;

            switch (actual)
            {
                case int intValue:
                    return intValue == (int)expected;
                case long longValue:
                    return longValue == (long)expected;
                case double doubleValue:
                    // Для double используем метод сравнения с допуском погрешности
                    return Math.Abs(doubleValue - (double)expected) < 1e-6;
                case bool boolValue:
                    return boolValue == (bool)expected;
                case string stringValue:
                    return stringValue == (string)expected;
                case int[] intArray:
                    return intArray.SequenceEqual((int[])expected);
                case List<int> intList:
                    return intList.SequenceEqual((List<int>)expected);
                case string[] stringArray:
                    return stringArray.SequenceEqual((string[])expected);
                case List<string> stringList:
                    return stringList.SequenceEqual((List<string>)expected);
                case DateTime dateTimeValue:
                    return dateTimeValue == (DateTime)expected;
                default:
                    throw new InvalidOperationException("Unsupported type encountered in comparison.");
            }
        }


        //public async Task<ExecuteCodeResponce> ExecuteCodeAsync(string code, object[] parameters, int timeoutInSeconds)
        //{
        //    ExecuteCodeResponce executeCodeResponce = new ExecuteCodeResponce();

        //    var syntaxTree = CSharpSyntaxTree.ParseText(code);
        //    var assemblyName = Path.GetRandomFileName();
        //    var references = new MetadataReference[]
        //    {
        //    MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
        //        // Добавьте другие необходимые ссылки здесь
        //    };
        //    var compilation = CSharpCompilation.Create(
        //        assemblyName,
        //        new[] { syntaxTree },
        //        references,
        //        new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        //    using (var ms = new MemoryStream())
        //    {
        //        EmitResult result = compilation.Emit(ms);
        //        if (!result.Success)
        //        {
        //            executeCodeResponce.CompilationErrorMessage += result.Diagnostics
        //                .Select(x => x.GetMessage())
        //                .Aggregate((x, y) => x + "\n" + y);

        //            return executeCodeResponce;
        //        }

        //        ms.Seek(0, SeekOrigin.Begin);
        //        var assembly = Assembly.Load(ms.ToArray());

        //        //Todo decide what to do with multiple classes, create some convention
        //        //var method = type.GetMethods().FirstOrDefault(m => m.Name == "Main"); // Предполагается, что метод для выполнения называется Main

        //        var type = assembly.GetTypes()[0];
        //        var method = type.GetMethods()[0];

        //        // Измерение времени и памяти
        //        var stopwatch = Stopwatch.StartNew();
        //        var memoryBefore = GC.GetTotalMemory(true);

        //        CancellationTokenSource cts = new CancellationTokenSource();
        //        var task = Task.Run(() =>
        //        {
        //            try
        //            {
        //                return method.Invoke(method.IsStatic ? null : Activator.CreateInstance(type), parameters);
        //            }
        //            catch (Exception ex)
        //            {
        //                executeCodeResponce.ErrorMessage = "Ошибка выполнения: " + ex.Message;
        //                return null;
        //            }
        //        }, cts.Token);

        //        if (await Task.WhenAny(task, Task.Delay(timeoutInSeconds * 1000)) == task && !task.IsFaulted)
        //        {
        //            // Задача завершилась вовремя и без исключений
        //            stopwatch.Stop();
        //            var memoryAfter = GC.GetTotalMemory(false);

        //            executeCodeResponce.IsSuccessful = true;
        //            executeCodeResponce.Result = task.Result;
        //            executeCodeResponce.ExecutionTime = stopwatch.Elapsed;
        //            executeCodeResponce.MemoryUsage = memoryAfter - memoryBefore;
        //            return executeCodeResponce;
        //        }
        //        else
        //        {
        //            // Задача не завершилась вовремя или произошло исключение
        //            cts.Cancel();
        //            executeCodeResponce.ErrorMessage = "Задача не завершилась вовремя";
        //            return executeCodeResponce;
        //        }
        //    }
        //}

        //    //using (var ms = new MemoryStream())
        //    //{
        //    //    EmitResult result = compilation.Emit(ms);
        //    //    if (!result.Success)
        //    //    {

        //    //        executeCodeResponce.CompilationErrorMessage += result.Diagnostics.Select(x => x.GetMessage()).Aggregate((x, y) => x + "\n" + y);

        //    //        return executeCodeResponce;
        //    //    }

        //    //    ms.Seek(0, SeekOrigin.Begin);
        //    //    var assembly = Assembly.Load(ms.ToArray());
        //    //    var type = assembly.GetTypes()[0];
        //    //    var method = type.GetMethods()[0];

        //    //    // Измерение времени и памяти
        //    //    var stopwatch = Stopwatch.StartNew();
        //    //    var memoryBefore = GC.GetTotalMemory(true);

        //    //    object classInstance = method.IsStatic ? null : Activator.CreateInstance(type);
        //    //    var task = Task.Run(() => method.Invoke(classInstance, parameters));

        //    //    if (await Task.WhenAny(task, Task.Delay(timeoutInSeconds * 1000)) == task)
        //    //    {
        //    //        // Задача завершилась вовремя
        //    //        stopwatch.Stop();
        //    //        var memoryAfter = GC.GetTotalMemory(false);

        //    //        executeCodeResponce.IsSuccessful = true;
        //    //        executeCodeResponce.Result = task.Result;
        //    //        executeCodeResponce.ExecutionTime = stopwatch.Elapsed;
        //    //        executeCodeResponce.MemoryUsage = memoryAfter - memoryBefore;
        //    //        return executeCodeResponce;
        //    //    }
        //    //    else
        //    //    {
        //    //        // Задача не завершилась вовремя
        //    //        executeCodeResponce.ErrorMessage = "Задача не завершилась вовремя";
        //    //        return executeCodeResponce;
        //    //    }
        //    //}
        //}
    }
}
