namespace CodeChallenge.Domain.Interfaces.Models
{
    public class ChallengeDetail
    {
        public Guid ChallengeId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Difficulty { get; set; }
        public string Description { get; set; }
        public string Solution { get; set; }
        public string CodeTemplate { get; set; }
    }
}
