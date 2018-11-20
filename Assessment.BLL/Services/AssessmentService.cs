using System.Collections.Generic;
using System.Linq;
using Assessment.BLL.DTOs;
using Assessment.BLL.Mapper;
using Assessment.BLL.Services.Interfaces;
using Assessment.DAL.Repositories.Interfaces;
using AutoMapper;
using EnumHelper = Assessment.BLL.Helper.EnumHelper;

namespace Assessment.BLL.Services
{
    public class AssessmentService : IAssessmentService
    {
        private IMapper _mapper;
        private IQuestionaryRepo _questionaryRepo;
        private IQuestionRepo _questionRepo;
        private IAssessmentRepo _assessmentRepo;

        public AssessmentService(IQuestionaryRepo questionaryRepo, IQuestionRepo questionRepo, IAssessmentRepo assessmentRepo)
        {
            _questionaryRepo = questionaryRepo;
            _questionRepo = questionRepo;
            _assessmentRepo = assessmentRepo;

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });

            _mapper = config.CreateMapper();
        }

        public IQuestionaryRepo questionaryRepo
        {
            get
            {
                return _questionaryRepo;
            }
            set
            {
                _questionaryRepo = value;
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

        public AssessmentDTO AddAssessment(int questionaryId, AssessmentDTO assessmentDto, EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin && assessmentDto.OwnerId != 0)
            {
                var questionary = _questionaryRepo.GetById(questionaryId);
                if (questionary != null)
                {
                    var questions = _questionRepo.GetAll().Where(x => x.QuestionDifficultyId == assessmentDto.QuestionDifficultyId && x.QuestionaryId == questionaryId);
                    if (questions.Any())
                    {
                        var assessment = _mapper.Map<DAL.Entities.Assessment>(assessmentDto);
                        var returnValue = _assessmentRepo.Add(assessment);
                        return _mapper.Map<AssessmentDTO>(returnValue);
                    }
                }
            }
            return null;
        }

        public AssessmentDTO GetAssessmentById(int assessmentId, EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                var assessment = _assessmentRepo.GetById(assessmentId);
                return _mapper.Map<AssessmentDTO>(assessment);
            }

            return null;
        }

        public List<AssessmentDTO> GetAllAssessments(EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                var assessments = _assessmentRepo.GetAll();
                var assessmentDtos = new List<AssessmentDTO>();
                foreach (var assessment in assessments)
                {
                    assessmentDtos.Add(_mapper.Map<AssessmentDTO>(assessment));
                }
                return assessmentDtos;
            }

            return null;
        }

        public AssessmentDTO UpdateAssessment(AssessmentDTO assessmentDto, EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                var assessment = _mapper.Map<DAL.Entities.Assessment>(assessmentDto);
                var returnValue = _assessmentRepo.Update(assessment);
                return _mapper.Map<AssessmentDTO>(returnValue);
            }

            return null;
        }

        public AssessmentDTO GenerateResult(int assessmentId, int questionaryId, EnumHelper.PermissionsUser permission)
        {
            DAL.Entities.Assessment assessment = null;
            var totalQuestions = 0;
            var totalCorrectAnswers = 0;
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                assessment = _assessmentRepo.GetById(assessmentId);
                if (assessment != null && assessment?.ListOfQuestionAndAnswerOfAssessment != null)
                {
                    //reset assessment percentage
                    assessment.AssessmentPercentage = 0;

                    var uniqueListOfQuestionIds = assessment.ListOfQuestionAndAnswerOfAssessment.ToList().OrderBy(x => x.QuestionId).Select(x => x.QuestionId).Distinct();
                    totalQuestions = uniqueListOfQuestionIds.Count();
                    foreach (var questionId in uniqueListOfQuestionIds)
                    {
                        var question = _questionRepo.GetById(questionId);
                        if (question?.Answers != null)
                        {
                            foreach (var questionAndAnswerOfAssessment in assessment.ListOfQuestionAndAnswerOfAssessment.Where(x => x.QuestionId == questionId).ToList())
                            {
                                var answer = question.Answers.FirstOrDefault(x => x.Id == questionAndAnswerOfAssessment.AnswerId && x.Correct == true);
                                if (answer != null)
                                    totalCorrectAnswers++;
                            }
                        }
                    }
                }
            }

            if (totalQuestions > 0)
            {
                double perc = ((double)totalCorrectAnswers / (double)totalQuestions) * 100;
                assessment.AssessmentPercentage = (double)perc;
            }

            return _mapper.Map<AssessmentDTO>(assessment);

        }
    }
}
