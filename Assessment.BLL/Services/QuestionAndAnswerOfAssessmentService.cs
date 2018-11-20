using System.Collections.Generic;
using System.Linq;
using Assessment.BLL.DTOs;
using Assessment.BLL.Mapper;
using Assessment.BLL.Services.Interfaces;
using Assessment.DAL.Entities;
using Assessment.DAL.Repositories.Interfaces;
using AutoMapper;
using EnumHelper = Assessment.BLL.Helper.EnumHelper;

namespace Assessment.BLL.Services
{
    public class QuestionAndAnswerOfAssessmentService : IQuestionAndAnswerOfAssessmentService
    {
        private IMapper _mapper;
        private IQuestionRepo _questionRepo;
        private IAssessmentRepo _assessmentRepo;
        private IQuestionsAndAnswersOfAssessmentREPO _questionsAndAnswersOfAssessmentRepo;
        public QuestionAndAnswerOfAssessmentService(IQuestionRepo questionRepo, IAssessmentRepo assessmentRepo, IQuestionsAndAnswersOfAssessmentREPO questionsAndAnswersOfAssessmentRepo)
        {
            _questionRepo = questionRepo;
            _assessmentRepo = assessmentRepo;
            _questionsAndAnswersOfAssessmentRepo = questionsAndAnswersOfAssessmentRepo;
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });

            _mapper = config.CreateMapper();
        }

        public IAssessmentRepo assessmentRepo
        {
            get
            {
                return _assessmentRepo;
            }
            set
            {
                _assessmentRepo = value;
            }
        }
        public IQuestionRepo questionRepo
        {
            get
            {
                return _questionRepo;
            }
            set
            {
                _questionRepo = value;
            }
        }
        public IQuestionsAndAnswersOfAssessmentREPO questionsAndAnswersOfAssessmentRepo
        {
            get
            {
                return _questionsAndAnswersOfAssessmentRepo;
            }
            set
            {
                _questionsAndAnswersOfAssessmentRepo = value;
            }
        }

        //public List<QuestionAndAnswerOfAssessment> AddAnAnswerOfAssessment( QuestionAndAnswerOfAssessment questionAndAnswerOfAssessment, EnumHelper.PermissionsUser permission)
        //{
        //    if (permission == EnumHelper.PermissionsUser.Owner)
        //    {
        //        var assessment = _assessmentRepo.GetById(questionAndAnswerOfAssessment.AssessmentId);
        //        var question = _questionRepo.GetById(questionAndAnswerOfAssessment.QuestionId);
        //        if (assessment != null && question != null )
        //        {
        //            assessment.QuestionAndAnswerOfAssessment.ToList().Add(questionAndAnswerOfAssessment);
        //            return assessment.QuestionAndAnswerOfAssessment.ToList();
        //        }
        //    }
        //    return null;
        //}

        public List<QuestionAndAnswerOfAssessmentDTO> AddAnAnswerOfAssessment(QuestionAndAnswerOfAssessmentDTO questionAndAnswerOfAssessmentDTO, EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Owner)
            {
                var assessmentENT = _mapper.Map<QuestionAndAnswerOfAssessment>(questionAndAnswerOfAssessmentDTO);
                var question = questionRepo.GetById(assessmentENT.QuestionId);
                var assessment = assessmentRepo.GetById(assessmentENT.AssessmentId);
                var list = questionsAndAnswersOfAssessmentRepo.GetAll().ToList();
                var returnList = new List<QuestionAndAnswerOfAssessmentDTO>();

                if(assessment!=null && question != null)
                {
                    foreach(var qa in list)
                    {
                        returnList.Add(_mapper.Map<QuestionAndAnswerOfAssessmentDTO>(qa));
                    }
                    return returnList;
                }
            }
            return null;
        }
    }
}
