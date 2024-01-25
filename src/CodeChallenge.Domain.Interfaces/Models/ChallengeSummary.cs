namespace CodeChallenge.Domain.Interfaces.Models
{
    public class ChallengeSummary
    {
        public Guid ChallengeId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Difficulty { get; set; }
    }
}
