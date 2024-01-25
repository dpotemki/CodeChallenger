using BenchmarkDotNet.Attributes;
using CodeCompilator.Service.Interfaces;
using CodeCompilator.Service.Services;
using CodeExecutionContracts.Models;

namespace CodeCompilator.ServiceTests
{
    public class UnitTest1
    {
        [Fact]
        public async Task CheckExpectedResult()
        {
            CompilationResult result = ExecutedResultTestMethod();

            Assert.True(result.IsSuccessful);


        }
  

        private  CompilationResult ExecutedResultTestMethod()
        {
            var executeCodeRequest = new ExecuteCodeRequest
            {
                Id = Guid.NewGuid(),
                MainMethodName = "SumOfDigits",
                CodeToExecute = @"
        public class Solution
        {
            public int SumOfDigits(int number)
            {
                int sum = 0;
                while (number != 0)
                {
                    sum += number % 10;
                    number /= 10;
                }
                return sum;
            }
        }",
                TestCases = new List<TestCase>
    {
        new TestCase
        {
            InputParameters = new List<InputParameter>
            {
                new InputParameter { Type = "int", Value = "123" }
            },
            ExpectedResult = new InputParameter { Type = "int", Value = "6" }
        },
        new TestCase
        {
            InputParameters = new List<InputParameter>
            {
                new InputParameter { Type = "int", Value = "-123" }
            },
            ExpectedResult = new InputParameter { Type = "int", Value = "-6" }
        },
        new TestCase
        {
            InputParameters = new List<InputParameter>
            {
                new InputParameter { Type = "int", Value = "0" }
            },
            ExpectedResult = new InputParameter { Type = "int", Value = "0" }
        }
    }
            };

            var codeRunner22 = new CodeTestingService();
            var result = codeRunner22.TestAndEvaluateCode(executeCodeRequest, default).Result;
            return result;
        }
    }
}