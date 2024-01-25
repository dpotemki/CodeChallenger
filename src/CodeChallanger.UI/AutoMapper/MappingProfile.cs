using AutoMapper;
using CodeChallenge.Domain.Interfaces.Models;
using CodeExecutionContracts.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CodeExecutionRequest, ExecuteCodeRequest>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ChallengeId))
            .ForMember(dest => dest.CodeToExecute, opt => opt.MapFrom(src => src.CodeToRun))
            .ForMember(dest => dest.MainMethodName, opt => opt.MapFrom(src => src.EntryPointMethodName))
            .ForMember(dest => dest.TestCases, opt => opt.MapFrom(src => src.ExecutionTestCases));

        CreateMap<ExecutionTestCase, TestCase>()
            .ForMember(dest => dest.InputParameters, opt => opt.MapFrom(src => src.InputParameters))
            .ForMember(dest => dest.ExpectedResult, opt => opt.MapFrom(src => src.ExpectedResult));

        CreateMap<ExecutionInputParameter, InputParameter>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.ParameterType))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.ParameterValue));

        // Обратный маппинг
        CreateMap<ExecuteCodeRequest, CodeExecutionRequest>()
            //.ForMember(dest => dest.ChallengeId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CodeToRun, opt => opt.MapFrom(src => src.CodeToExecute))
            .ForMember(dest => dest.EntryPointMethodName, opt => opt.MapFrom(src => src.MainMethodName))
            .ForMember(dest => dest.ExecutionTestCases, opt => opt.MapFrom(src => src.TestCases));

        CreateMap<TestCase, ExecutionTestCase>()
            .ForMember(dest => dest.InputParameters, opt => opt.MapFrom(src => src.InputParameters))
            .ForMember(dest => dest.ExpectedResult, opt => opt.MapFrom(src => src.ExpectedResult));

        CreateMap<InputParameter, ExecutionInputParameter>()
            .ForMember(dest => dest.ParameterType, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.ParameterValue, opt => opt.MapFrom(src => src.Value));
    }
}
