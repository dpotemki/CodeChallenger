namespace CodeCompilator.Service.Models
{
    public class ExecuteCodeResponce
    {
        public object Result { get; set; }
        public TimeSpan ExecutionTime { get; set; }
        public long MemoryUsage { get; set; }
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public string CompilationErrorMessage { get; set; }
    }
}
