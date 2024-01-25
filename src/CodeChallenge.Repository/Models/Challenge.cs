using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace CodeChallenge.Repository.Models
{
    public class Challenge
    {
        [Key]
        public Guid ChallengeId { get; set; }
        public string Name { get; set; }
        public string Difficulty { get; set; }
        public string Type { get; set; }

        public string MainMethodName { get; set; }
        public ICollection<ChallengeDescription> Descriptions { get; set; }
        public ICollection<ChallengeSolution> Solutions { get; set; }
        public ICollection<ChallengeCodeTemplate> CodeTemplates { get; set; }
        public ICollection<TestCase> TestCases { get; set; }
    }

    public class ChallengeDescription
    {
        [Key]
        public Guid DescriptionId { get; set; }
        public Guid ChallengeId { get; set; }
        public string Language { get; set; }
        public string HtmlContent { get; set; }

        public Challenge Challenge { get; set; }
    }

    public class ChallengeSolution
    {
        [Key]
        public Guid SolutionId { get; set; }
        public Guid ChallengeId { get; set; }
        public string Language { get; set; }
        public string HtmlContent { get; set; }

        public Challenge Challenge { get; set; }
    }

    public class ChallengeCodeTemplate
    {
        [Key]
        public Guid TemplateId { get; set; }
        public Guid ChallengeId { get; set; }
        public string ProgrammingLanguage { get; set; }
        public string CodeTemplate { get; set; }

        public Challenge Challenge { get; set; }
    }

    public class TestCase
    {
        [Key]
        public Guid TestCaseId { get; set; }
        public Guid ChallengeId { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public string InputParametersJson { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public string ExpectedResultJson { get; set; }

        [NotMapped]
        public List<InputParameter> InputParameters
        {
            get => JsonConvert.DeserializeObject<List<InputParameter>>(InputParametersJson);
            set => InputParametersJson = JsonConvert.SerializeObject(value);
        }
        [NotMapped]
        public InputParameter ExpectedResult
        {
            get => JsonConvert.DeserializeObject<InputParameter>(ExpectedResultJson);
            set => ExpectedResultJson = JsonConvert.SerializeObject(value);
        }

        public Challenge Challenge { get; set; }
    }

    public class InputParameter
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }

}
