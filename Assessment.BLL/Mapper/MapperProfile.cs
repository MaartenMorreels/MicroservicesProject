using Assessment.BLL.DTOs;
using Assessment.DAL.Entities;
using AutoMapper;

namespace Assessment.BLL.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AnswerDTO, Answer>();
            CreateMap<Answer, AnswerDTO>();

            CreateMap<DAL.Entities.Assessment, AssessmentDTO>();
            CreateMap<AssessmentDTO, DAL.Entities.Assessment>();

            CreateMap<AssessmentOfCandidateDTO, AssessmentOfCandidate>();
            CreateMap<AssessmentOfCandidate, AssessmentOfCandidateDTO>();

            CreateMap<AssessmentOfEmployeeDTO, AssessmentOfEmployee>();
            CreateMap<AssessmentOfEmployee, AssessmentOfEmployeeDTO>();

            CreateMap<QuestionDTO, Question>();
            CreateMap<Question, QuestionDTO>();

            CreateMap<QuestionAndAnswerOfAssessmentDTO, QuestionAndAnswerOfAssessment>();
            CreateMap<QuestionAndAnswerOfAssessment, QuestionAndAnswerOfAssessmentDTO>();

            CreateMap<QuestionApplicationDomainBackEndDTO, QuestionApplicationDomainBackEnd>();
            CreateMap<QuestionApplicationDomainBackEnd, QuestionApplicationDomainBackEndDTO>();

            CreateMap<QuestionApplicationDomainFrontEndDTO, QuestionApplicationDomainFrontEnd>();
            CreateMap<QuestionApplicationDomainFrontEnd, QuestionApplicationDomainFrontEndDTO>();

            CreateMap<QuestionApplicationLanguageDTO, QuestionApplicationLanguage>();
            CreateMap<QuestionApplicationLanguage, QuestionApplicationLanguageDTO>();

            CreateMap<QuestionaryDTO, Questionary>();
            CreateMap<Questionary, QuestionaryDTO>();

            CreateMap<QuestionCompositionDTO, QuestionComposition>();
            CreateMap<QuestionComposition, QuestionCompositionDTO>();
        }
    }
}
