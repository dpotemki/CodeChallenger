using CodeChallanger.UI.Models;
using CodeChallenge.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CodeChallanger.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        private readonly IChallengeRepository _challengeRepository;


        public HomeController(ILogger<HomeController> logger, IChallengeRepository challengeRepository)
        {
            _logger = logger;
            _challengeRepository = challengeRepository;
        }

        public async Task<IActionResult> Index()
        {

            var challangeList = await _challengeRepository.GetChallengesAsync(new ChallengeFilter { });
            return View(challangeList);
        }

        [HttpGet("Home/Challenge/{challengeId}")]
        public async Task<IActionResult> Challange(Guid challengeId, string lang = "ru", string programmingLanguage = "csharp")
        {
            var res = await _challengeRepository.GetChallengeDetailAsync(new ChallengeDetailRequest { ChallengeId = challengeId, Language = lang, ProgrammingLanguage = programmingLanguage });

            return View(res);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
