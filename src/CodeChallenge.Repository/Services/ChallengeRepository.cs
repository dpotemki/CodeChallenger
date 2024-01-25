using CodeChallenge.Domain.Interfaces;
using CodeChallenge.Domain.Interfaces.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge.Repository.Services
{
    public class ChallengeRepository : IChallengeRepository
    {
        private readonly CodeChallengerContext _context;

        public ChallengeRepository(CodeChallengerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ChallengeSummary>> GetChallengesAsync(ChallengeFilter filter)
        {
            return await _context.Challenges
                //.Where(c => c.Type == filter.Type && c.Difficulty == filter.Difficulty)
                .Select(c => new ChallengeSummary
                {
                    ChallengeId = c.ChallengeId,
                    Name = c.Name,
                    Type = c.Type,
                    Difficulty = c.Difficulty
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ChallengeDetail> GetChallengeDetailAsync(ChallengeDetailRequest request)
        {
            var challenge = await _context.Challenges
                .Include(c => c.Descriptions)
                .Include(c => c.Solutions)
                .Include(c => c.CodeTemplates)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ChallengeId == request.ChallengeId);

            var description = challenge.Descriptions.FirstOrDefault(d => d.Language == request.Language);
            var solution = challenge.Solutions.FirstOrDefault(s => s.Language == request.Language);
            var codeTemplate = challenge.CodeTemplates.FirstOrDefault(ct => ct.ProgrammingLanguage == request.ProgrammingLanguage);

            return new ChallengeDetail
            {
                ChallengeId = challenge.ChallengeId,
                Name = challenge.Name,
                Type = challenge.Type,
                Difficulty = challenge.Difficulty,
                Description = description?.HtmlContent,
                Solution = solution?.HtmlContent,
                CodeTemplate = codeTemplate?.CodeTemplate
            };
        }

        public async Task<CodeExecutionRequest> GetChallengeForExecutionAsync(ChallengeExecutionRequest request)
        {
            var codeTemplate = await _context.ChallengeCodeTemplates
                .AsNoTracking()
                .FirstOrDefaultAsync(ct => ct.ChallengeId == request.ChallengeId && ct.ProgrammingLanguage == request.ProgrammingLanguage);

            var testCases = await _context.TestCases
                .Where(tc => tc.ChallengeId == request.ChallengeId)
                .AsNoTracking()
                .ToListAsync();

            var mainMethodName = await _context.Challenges.Where(c => c.ChallengeId == request.ChallengeId).Select(c => c.MainMethodName).FirstOrDefaultAsync();
            return new CodeExecutionRequest
            {
                ChallengeId = request.ChallengeId,
                CodeToRun = codeTemplate?.CodeTemplate,
                EntryPointMethodName = mainMethodName,
                ExecutionTestCases = testCases.Select(tc => new ExecutionTestCase
                {
                    InputParameters = tc.InputParameters.Select(x => new ExecutionInputParameter { ParameterType = x.Type, ParameterValue = x.Value }).ToList(),
                    ExpectedResult = new ExecutionInputParameter { ParameterValue = tc.ExpectedResult.Value, ParameterType = tc.ExpectedResult.Type }
                }).ToList()
            };
        }
    }

}
