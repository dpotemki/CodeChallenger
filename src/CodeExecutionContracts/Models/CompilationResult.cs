using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeExecutionContracts.Models
{
    public class CompilationResult
    {
        public Guid Id { get; set; }
        public bool IsSuccessful => TestCasesCount == PassedTestCasesCount;

        public TimeSpan ExecutionTime { get; set; }
        public long MemoryUsageInBytes { get; set; }

        public InputParameter ExpectedResult { get; set; }
        public List<InputParameter> TestCase { get; set; }
        public InputParameter ActualResult { get; set; }
        public int TestCasesCount { get; set; }
        public int PassedTestCasesCount { get; set; }
        public string ErrorMessage { get; set; }
        public string CompilationErrorMessage { get; set; }
    }
}
