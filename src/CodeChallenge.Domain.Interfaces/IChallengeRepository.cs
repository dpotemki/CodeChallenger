using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeChallenge.Domain.Interfaces.Models;

namespace CodeChallenge.Domain.Interfaces
{
    public interface IChallengeRepository
    {
        Task<IEnumerable<ChallengeSummary>> GetChallengesAsync(ChallengeFilter filter);
        Task<ChallengeDetail> GetChallengeDetailAsync(ChallengeDetailRequest request);
        Task<CodeExecutionRequest> GetChallengeForExecutionAsync(ChallengeExecutionRequest request);
    }

}
