namespace CodeChallenge.Domain.Interfaces
{
    public class ChallengeDetailRequest
    {
        public Guid ChallengeId { get; set; }
        public string Language { get; set; }
        public string ProgrammingLanguage { get; set; }
    }
}
