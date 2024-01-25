using AutoMapper;
using CodeChallanger.UI.Configurations;
using CodeChallanger.UI.Controllers;
using CodeChallanger.UI.Interfaces;
using CodeChallenge.Domain.Interfaces;
using CodeExecutionContracts.Models;
using Microsoft.Extensions.Options;

namespace CodeChallanger.UI.Services
{
    public class ChallengeServices
    {
        private readonly IChallengeRepository _challengeRepository;
        private readonly RabbitMQPublisher _rabbitMQPublisher;
        private readonly IOptions<RabbitMQOptions> _queueOptions;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public ChallengeServices(
            IChallengeRepository challengeRepository,
            RabbitMQPublisher rabbitMQPublisher,
            IOptions<RabbitMQOptions> queueOptions,
            IMapper mapper,
            ICacheService cacheService)
        {
            _challengeRepository = challengeRepository;
            _rabbitMQPublisher = rabbitMQPublisher;
            _queueOptions = queueOptions;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public const int StoreIntervalInMinutes = 10;
        public void SetCompilationResult(CompilationResult compilationResult)
        {
            _cacheService.Set(compilationResult.Id.ToString() + "_request", "Completed", TimeSpan.FromMinutes(StoreIntervalInMinutes));
            _cacheService.Set(compilationResult.Id.ToString() + "_result", compilationResult, TimeSpan.FromMinutes(StoreIntervalInMinutes));
        }

        public string CheckCompilationStatus(Guid taskId)
        {
            var status = _cacheService.Get<string>(taskId + "_request");
            return status;
        }
        public async Task<Guid> SetCompilationRequest(ChallangeRequest challangeRequest)
        {
            //All this logic must be is service , or sended to mediator
            var hardcodedSupportedOnlyOneLanguage = "csharp";
            var chalangeExecuteModel = await _challengeRepository.GetChallengeForExecutionAsync(
                        new ChallengeExecutionRequest
                        {
                            ChallengeId = challangeRequest.challengeId,
                            ProgrammingLanguage = hardcodedSupportedOnlyOneLanguage
                        });

            chalangeExecuteModel.CodeToRun = challangeRequest.code;

            var queueName = _queueOptions.Value.CSharpProducerQueueName;
            //if(chalangeExecuteModel.ProgrammingLanguage == "java")
            //    queueName = _queueOptions.Value.JavaProducerQueueName;  


            var convertedResult = _mapper.Map<ExecuteCodeRequest>(chalangeExecuteModel);
            var taskId = Guid.NewGuid();
            convertedResult.Id = taskId;

            _cacheService.Set(taskId + "_request", "Pending", TimeSpan.FromMinutes(StoreIntervalInMinutes));
            _rabbitMQPublisher.PublishExecuteCodeRequest(convertedResult, queueName);
            return taskId;
        }

        public CompilationResult GetCompilationResult(Guid taskId)
        {
            var result = _cacheService.Get<CompilationResult>(taskId + "_result");
            return result;
        }

    }
}
