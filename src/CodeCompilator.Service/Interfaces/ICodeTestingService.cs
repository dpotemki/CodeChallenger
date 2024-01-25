using CodeExecutionContracts.Models;

namespace CodeCompilator.Service.Interfaces
{
    public interface ICodeTestingService
    {
        Task<CompilationResult> TestAndEvaluateCode(ExecuteCodeRequest submission, CancellationToken stoppingToken);

    }
}
