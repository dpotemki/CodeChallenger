namespace CodeChallenge.Domain.Interfaces.Models
{
    public class ExecutionTestCase
    {
        public List<ExecutionInputParameter> InputParameters { get; set; }
        public ExecutionInputParameter ExpectedResult { get; set; }
    }

}
