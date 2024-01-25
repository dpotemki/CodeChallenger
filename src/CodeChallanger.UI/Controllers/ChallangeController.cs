using AutoMapper;
using CodeChallanger.UI.Configurations;
using CodeChallanger.UI.Services;
using CodeChallenge.Domain.Interfaces;
using CodeExecutionContracts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CodeChallanger.UI.Controllers
{
    [Route("api/[controller]")]
    public class ChallangeController : Controller
    {

        private readonly ChallengeServices _challengeServices;


        public ChallangeController(
            ChallengeServices challengeServices
            )
        {
            _challengeServices = challengeServices;
        }
        [HttpPost("run")]
        public async Task<IActionResult> Run([FromBody] ChallangeRequest challangeRequest)
        {
            var taskId = await _challengeServices.SetCompilationRequest(challangeRequest);
            return Json(new ChallangeResponse { executionId = taskId });
        }


        [HttpGet("status/{executionId}")]
        public IActionResult Status(Guid executionId)
        {
            var status = _challengeServices.CheckCompilationStatus(executionId);
            return Json(new ChallangeStatusResponse { status = status });
        }


        [HttpGet("result/{executionId}")]
        public IActionResult Result(Guid executionId)
        {
            var result = _challengeServices.GetCompilationResult(executionId);
            return View("/Views/Home/_CompilationResult.cshtml", result);
        }
    }
}
