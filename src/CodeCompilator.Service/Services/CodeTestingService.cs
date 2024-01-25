using CodeCompilator.Service.Interfaces;
using CodeExecutionContracts.Models;

namespace CodeCompilator.Service.Services
{
    public class CodeTestingService : ICodeTestingService
    {

        public async Task<CompilationResult> TestAndEvaluateCode(ExecuteCodeRequest submission, CancellationToken stoppingToken)
        {
            var executionTimeInSeconds = 5;

            CompilationResult compilationResult = new CompilationResult();
            compilationResult.TestCasesCount = submission.TestCases.Count;
            compilationResult.Id = submission.Id;

            var codeRunnerService = new CodeRunnerV2();


            var totalExecutionTime = new TimeSpan();
            var totalMemoryUsage = 0L;
            for (var i = 0; i < submission.TestCases.Count; i++)
            {
                var testCase = submission.TestCases[i];
                var inputParametrs = codeRunnerService.DeserializeParameters(testCase.InputParameters);
                var res = await codeRunnerService.ExecuteCodeAsync(submission.CodeToExecute, inputParametrs, executionTimeInSeconds, submission.MainMethodName);

                totalExecutionTime += res.ExecutionTime;
                totalMemoryUsage += res.MemoryUsage;

                if (res.IsSuccessful)
                {
                    var expectedObject = codeRunnerService.InputParameterToObject(testCase.ExpectedResult);
                    var compareResult = codeRunnerService.CompareResults(expectedObject, res.Result);

                    if (compareResult)
                    {
                        compilationResult.PassedTestCasesCount++;
                    }
                    else
                    {
                        var actualObject = codeRunnerService.ObjectToInputParameter(res.Result);
                        compilationResult.ActualResult = actualObject;
                        compilationResult.ExpectedResult = testCase.ExpectedResult;
                        compilationResult.TestCase = testCase.InputParameters;
                        compilationResult.ErrorMessage = "Test case failed";
                        break;

                    }
                }
                else
                {
                    compilationResult.ErrorMessage += res.ErrorMessage;
                    compilationResult.CompilationErrorMessage += res.CompilationErrorMessage;
                    break;
                }
            }
            compilationResult.ExecutionTime = totalExecutionTime;
            compilationResult.MemoryUsageInBytes = totalMemoryUsage;  
            return compilationResult;

        }
    }
}
