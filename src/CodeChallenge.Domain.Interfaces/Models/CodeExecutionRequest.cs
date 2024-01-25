namespace CodeChallenge.Domain.Interfaces.Models
{
    public class CodeExecutionRequest
    {
        public Guid ChallengeId { get; set; }
        public string CodeToRun { get; set; }
        public string EntryPointMethodName { get; set; }
        public List<ExecutionTestCase> ExecutionTestCases { get; set; } = new List<ExecutionTestCase>();
    }

}
