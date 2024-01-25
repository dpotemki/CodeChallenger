namespace CodeChallenge.Domain.Interfaces
{
    public class ChallengeExecutionRequest
    {
        public Guid ChallengeId { get; set; }
        public string ProgrammingLanguage { get; set; }
    }
}
