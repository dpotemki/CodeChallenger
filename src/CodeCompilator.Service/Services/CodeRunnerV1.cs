using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System.Reflection;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace CodeCompilator.Service.Services
{
    public class CodeRunnerV1
    {
        public async Task<(object result, TimeSpan executionTime, long memoryUsage)> ExecuteCodeAsync(string code, object[] parameters, int timeoutInSeconds)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(code);
            var assemblyName = Path.GetRandomFileName();
            var references = new MetadataReference[]
            {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                // Добавьте другие необходимые ссылки здесь
            };
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
                    // Обработка ошибок компиляции
                    return (null, TimeSpan.Zero, 0);
                }

                ms.Seek(0, SeekOrigin.Begin);
                var assembly = Assembly.Load(ms.ToArray());
                var type = assembly.GetTypes()[0];
                var method = type.GetMethods()[0];

                // Измерение времени и памяти
                var stopwatch = Stopwatch.StartNew();
                var memoryBefore = GC.GetTotalMemory(true);

                var task = Task.Run(() => method.Invoke(null, parameters));
                if (await Task.WhenAny(task, Task.Delay(timeoutInSeconds * 1000)) == task)
                {
                    // Задача завершилась вовремя
                    stopwatch.Stop();
                    var memoryAfter = GC.GetTotalMemory(false);
                    return (task.Result, stopwatch.Elapsed, memoryAfter - memoryBefore);
                }
                else
                {
                    // Задача не завершилась вовремя
                    return (null, TimeSpan.Zero, 0);
                }
            }
        }


        public object[] DeserializeParameters(string json)
        {
            var rawParameters = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, List<Dictionary<string, string>>>>(json)["parameters"];
            var result = new List<object>();

            foreach (var param in rawParameters)
            {
                string type = param["type"];
                string value = param["value"];

                switch (type)
                {
                    case "DateTime":
                        result.Add(DateTime.Parse(value));
                        break;
                    case "int":
                        result.Add(int.Parse(value));
                        break;
                    case "long":
                        result.Add(long.Parse(value));
                        break;
                    case "double":
                        result.Add(double.Parse(value));
                        break;
                    case "bool":
                        result.Add(bool.Parse(value));
                        break;
                    case "string":
                        result.Add(value);
                        break;
                    case "int[]":
                        result.Add(System.Text.Json.JsonSerializer.Deserialize<int[]>(value));
                        break;
                    case "List<int>":
                        result.Add(System.Text.Json.JsonSerializer.Deserialize<List<int>>(value));
                        break;
                    case "string[]":
                        result.Add(System.Text.Json.JsonSerializer.Deserialize<string[]>(value));
                        break;
                    case "List<string>":
                        result.Add(System.Text.Json.JsonSerializer.Deserialize<List<string>>(value));
                        break;
                        // Дополнительные типы можно добавлять по аналогии
                }
            }

            return result.ToArray();
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

    }
}
