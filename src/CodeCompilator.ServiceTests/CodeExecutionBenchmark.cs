using BenchmarkDotNet.Attributes;
using CodeExecutionContracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCompilator.ServiceTests
{
    public static class CodeExecutionBenchmark
    {
        [Benchmark]
        public static void TestAndEvaluateCodeBenchmark()
        {
            var result = ExecutedResultTestMethod();
            if (!result.IsSuccessful)
            {
                throw new InvalidOperationException("Benchmark test failed to execute code successfully.");
            }
        }

        private CompilationResult ExecutedResultTestMethod()
        {
            // Здесь аналогичная реализация вызова вашего метода и получение результата
            return new CompilationResult { IsSuccessful = true }; // Примерный результат
        }
    }
}
