using Assessment.BLL.DTOs;
using Assessment.DAL.Entities;
using AutoMapper;

namespace Assessment.BLL.Tests
{
    public class MapperProfile : Profile
    {
        public IMapper _mapper;

        public MapperProfile()
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<QuestionaryDTO, Questionary>();
                cfg.CreateMap<Questionary, QuestionaryDTO>();
                cfg.CreateMap<AnswerDTO, Answer>();
                cfg.CreateMap<Answer, AnswerDTO>();
                cfg.CreateMap<QuestionDTO, Question>();
                cfg.CreateMap<Question, QuestionDTO>();
                cfg.CreateMap<DAL.Entities.Assessment, AssessmentDTO>();
                cfg.CreateMap<AssessmentDTO, DAL.Entities.Assessment>();
                cfg.CreateMap<QuestionAndAnswerOfAssessment, QuestionAndAnswerOfAssessmentDTO>();
                cfg.CreateMap<QuestionAndAnswerOfAssessmentDTO, QuestionAndAnswerOfAssessment>();
                cfg.CreateMap<QuestionApplicationLanguage, QuestionApplicationLanguageDTO>();
                cfg.CreateMap<QuestionApplicationLanguageDTO, QuestionApplicationLanguage>();
                cfg.CreateMap<AssessmentOfCandidate, AssessmentOfCandidateDTO>();
                cfg.CreateMap<AssessmentOfCandidateDTO, AssessmentOfCandidate>();
                cfg.CreateMap<AssessmentOfEmployee, AssessmentOfEmployeeDTO>();
                cfg.CreateMap<AssessmentOfEmployeeDTO, AssessmentOfEmployee>();
                cfg.CreateMap<QuestionComposition, QuestionCompositionDTO>();
                cfg.CreateMap<QuestionCompositionDTO, QuestionComposition>();
                cfg.CreateMap<QuestionApplicationDomainFrontEnd, QuestionApplicationDomainFrontEndDTO>();
                cfg.CreateMap<QuestionApplicationDomainFrontEndDTO, QuestionApplicationDomainFrontEnd>();
                cfg.CreateMap<QuestionApplicationDomainBackEnd, QuestionApplicationDomainBackEndDTO>();
                cfg.CreateMap<QuestionApplicationDomainBackEndDTO, QuestionApplicationDomainBackEnd>();
                cfg.CreateMap<QuestionComposition, QuestionCompositionDTO>();
                cfg.CreateMap<QuestionCompositionDTO, QuestionComposition>();
            });

            IMapper mapper = config.CreateMapper();
            _mapper = mapper;
        }

        public IMapper Mapper
        {
            get { return _mapper; }
        }
    }
}
