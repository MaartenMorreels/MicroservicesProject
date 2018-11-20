using System;
using System.Collections.Generic;
using System.Linq;
using Assessment.BLL.DTOs;
using Assessment.BLL.Helper;
using Assessment.BLL.Services;
using Assessment.DAL.Entities;
using Assessment.DAL.Repositories.Interfaces;
using AutoMapper;
using Moq;
using Xunit;

namespace Assessment.BLL.Tests
{
    public class AssessmentServiceTest
    {
        #region private fields

        private IMapper _mapper;
        private int _ownerId = 123456789;
        private MockRepository _factory;
        private Mock<IQuestionaryRepo> _mockQuestionaryRepo;
        private Mock<IQuestionRepo> _mockQuestionRepo;
        private Mock<IAnswerRepo> _mockAnswerRepo;
        private Mock<IAssessmentRepo> _mockAssessmentRepo;
        private Questionary _questionary;
        private DAL.Entities.Assessment _Assessment;
        private List<Question> _listQuestions;
        private List<QuestionAndAnswerOfAssessment> _listQuestionsAndAnswersOfAssessments;
        private Mock<IQuestionsAndAnswersOfAssessmentREPO> _mockQuestionsAndAnswersOfAssessmentRepo;
        private AssessmentService _assessmentService;

        #endregion private fields

        #region public constructor

        public AssessmentServiceTest()
        {
            MapperProfile map = new MapperProfile();
            _mapper = map.Mapper;
        }
        #endregion constructor

        #region Init

        public void Init()
        {
            _factory = new MockRepository(MockBehavior.Loose);
            _mockQuestionaryRepo = _factory.Create<IQuestionaryRepo>();
            _mockQuestionRepo = _factory.Create<IQuestionRepo>();
            _mockAnswerRepo = _factory.Create<IAnswerRepo>();
            _mockAssessmentRepo = _factory.Create<IAssessmentRepo>();
            _mockQuestionsAndAnswersOfAssessmentRepo = _factory.Create<IQuestionsAndAnswersOfAssessmentREPO>();

            _questionary = new Questionary { Id = 1, Description = "Eerste test"};
            _Assessment = new DAL.Entities.Assessment { Id = 12, AssessmentIdentifier = Guid.NewGuid(), OwnerId = _ownerId, Feedback = "Dit is de  eerste assessment.", QuestionDifficultyId = 31};

            _listQuestions = new List<Question>
            {
                new Question
                {
                    Id = 1,QuestionPhrase = "Dit is de eerste test.", QuestionaryId = 1, AllowedTime = 30, QuestionDifficultyId = 31,
                    Answers = new List<Answer>
                    {
                        new Answer{Correct = true, Id = 1, Text = "Eerste antwoord"}
                    }
                },
                new Question
                {
                    Id = 2,QuestionPhrase = "Dit is de tweede test.", QuestionaryId = 1, AllowedTime = 30, QuestionDifficultyId = 31,
                    Answers = new List<Answer>
                    {
                        new Answer{Correct = false, Id = 2, Text = "Eerste antwoord"}
                    }
                },
                new Question
                {
                    Id = 3,QuestionPhrase = "Dit is de derde test.", QuestionaryId = 1, AllowedTime = 30, QuestionDifficultyId = 31,
                    Answers = new List<Answer>
                    {
                        new Answer{Correct = false, Id = 3, Text = "Eerste antwoord"}
                    }
                },
                new Question
                {
                    Id = 4,QuestionPhrase = "Dit is de vierde test.", QuestionaryId = 1, AllowedTime = 30, QuestionDifficultyId = 31,
                    Answers = new List<Answer>
                    {
                        new Answer{Correct = true, Id = 4, Text = "Eerste antwoord"}
                    }
                }
            };

            _listQuestionsAndAnswersOfAssessments = new List<QuestionAndAnswerOfAssessment>
            {
                new QuestionAndAnswerOfAssessment{Id = 1, OwnerId = _ownerId, QuestionId = 1, AnswerId = 1, AssessmentId = 1},
                new QuestionAndAnswerOfAssessment{Id = 2, OwnerId = _ownerId, QuestionId = 2, AnswerId = 2, AssessmentId = 1},
                new QuestionAndAnswerOfAssessment{Id = 3, OwnerId = _ownerId, QuestionId = 3, AnswerId = 3, AssessmentId = 1},
                new QuestionAndAnswerOfAssessment{Id = 4, OwnerId = _ownerId, QuestionId = 4, AnswerId = 4, AssessmentId = 1},
            };

            _assessmentService = new AssessmentService(_mockQuestionaryRepo.Object, _mockQuestionRepo.Object, _mockAssessmentRepo.Object);
          
        }

        #endregion Init

        #region Tests

        [Fact]
        public void CanAnAdminCreateANewAssessment_TheGetByIdOfQuestionaryMustBeCalled()
        {
            Init();

            _mockQuestionaryRepo.Setup(x => x.GetById(_questionary.Id)).Returns(_questionary);

            _assessmentService.AddAssessment(_questionary.Id, _mapper.Map<AssessmentDTO>(_Assessment), EnumHelper.PermissionsUser.Admin);

            _mockQuestionaryRepo.Verify(x => x.GetById(_questionary.Id), Times.Once);
        }

        [Fact]
        public void CanAOtherUserThanAdminCreateANewAssessment_TheGetByIdOfQuestionaryMustNotBeCalled()
        {
            Init();

            _mockQuestionaryRepo.Setup(x => x.GetById(_questionary.Id)).Returns(_questionary);

            _assessmentService.AddAssessment(_questionary.Id, _mapper.Map<AssessmentDTO>(_Assessment), EnumHelper.PermissionsUser.GDPR);
            _assessmentService.AddAssessment(_questionary.Id, _mapper.Map<AssessmentDTO>(_Assessment), EnumHelper.PermissionsUser.Read);
            _assessmentService.AddAssessment(_questionary.Id, _mapper.Map<AssessmentDTO>(_Assessment), EnumHelper.PermissionsUser.Write);
            _assessmentService.AddAssessment(_questionary.Id, _mapper.Map<AssessmentDTO>(_Assessment), EnumHelper.PermissionsUser.Owner);

            _mockQuestionaryRepo.Verify(x => x.GetById(_questionary.Id), Times.Never);
        }


        [Fact]
        public void CanAnAdminCreateANewAssessment_TheAddAssessmentMethodMustBeCalled()
        {
            Init();

            _mockQuestionaryRepo.Setup(x => x.GetById(_questionary.Id)).Returns(_questionary);
            _mockQuestionRepo.Setup(x => x.GetAll()).Returns(_listQuestions.Where(x => x.QuestionaryId == _questionary.Id && x.QuestionDifficultyId == 31).AsQueryable());
            _mockAssessmentRepo.Setup(x => x.Add(_Assessment)).Returns(_Assessment);

            _assessmentService.AddAssessment(_questionary.Id, _mapper.Map<AssessmentDTO>(_Assessment), EnumHelper.PermissionsUser.Admin);

            _mockAssessmentRepo.Verify(x => x.Add(_Assessment), Times.Once);
        }

        [Fact]
        public void CanAnAdminUpdateAnAssessment_TheUpdateAssessmentMethodMustBeCalled()
        {
            Init();

            _Assessment.Feedback = "Assessment feedback is aangepast"; // Gaat dit problemen geven wanneer een andere test nog bezig zou zijn terwijl deze de update doet??

            _mockAssessmentRepo.Setup(x => x.Update(_Assessment)).Returns(_Assessment);

            _assessmentService.UpdateAssessment(_mapper.Map<AssessmentDTO>(_Assessment), EnumHelper.PermissionsUser.Admin);

            _mockAssessmentRepo.Verify(x => x.Update(_Assessment), Times.Once);
        }

        [Fact]
        public void CanAOtherUserThanAdminUpdateAnAssessment_TheUpdateOFAssessmentMethodMustNotBeCalled()
        {
            Init();

            _Assessment.Feedback = "Assessment feedback is aangepast"; // Gaat dit problemen geven wanneer een andere test nog bezig zou zijn terwijl deze de update doet??

            _mockAssessmentRepo.Setup(x => x.Update(_Assessment)).Returns(_Assessment);

            _assessmentService.UpdateAssessment(_mapper.Map<AssessmentDTO>(_Assessment), EnumHelper.PermissionsUser.GDPR);
            _assessmentService.UpdateAssessment(_mapper.Map<AssessmentDTO>(_Assessment), EnumHelper.PermissionsUser.Read);
            _assessmentService.UpdateAssessment(_mapper.Map<AssessmentDTO>(_Assessment), EnumHelper.PermissionsUser.Write);
            _assessmentService.UpdateAssessment(_mapper.Map<AssessmentDTO>(_Assessment), EnumHelper.PermissionsUser.Owner);

            _mockAssessmentRepo.Verify(x => x.Update(_Assessment), Times.Never);
        }

        [Fact]
        public void CanAnAdminUpdateAnAssessment_IRecieveTheUpdatedAssessment()
        {
            Init();

            _Assessment.Feedback = "Assessment feedback is aangepast"; // Gaat dit problemen geven wanneer een andere test nog bezig zou zijn terwijl deze de update doet??

            _mockAssessmentRepo.Setup(x => x.Update(_Assessment)).Returns(_Assessment);

            var response = _assessmentService.UpdateAssessment(_mapper.Map<AssessmentDTO>(_Assessment), EnumHelper.PermissionsUser.Admin);

            Assert.Equal(_Assessment.Id, response.Id);
            Assert.Equal(_Assessment.Feedback, response.Feedback);
        }


        [Fact]
        public void CanAnAdminSeeTheScoreForAnAssessment_TheGetByIdOfAssessmentMethodMustBeCalled()
        {
            Init();

            _mockAssessmentRepo.Setup(x => x.GetById(_Assessment.Id)).Returns(_Assessment);

            _assessmentService.GenerateResult(_mapper.Map<AssessmentDTO>(_Assessment).Id, _questionary.Id, EnumHelper.PermissionsUser.Admin);

            _mockAssessmentRepo.Verify(x => x.GetById(_Assessment.Id), Times.Once);
        }

        [Fact]
        public void CanAnAdminSeeTheScoreForAnAssessment_TheGetByIdOfQuestionMustBeCalled()
        {
            Init();

            _Assessment.ListOfQuestionAndAnswerOfAssessment = _listQuestionsAndAnswersOfAssessments;

            _mockAssessmentRepo.Setup(x => x.GetById(_Assessment.Id)).Returns(_Assessment);
            _mockQuestionRepo.Setup(x => x.GetById(1)).Returns(_listQuestions.FirstOrDefault(x => x.Id == 1));
            _mockQuestionRepo.Setup(x => x.GetById(2)).Returns(_listQuestions.FirstOrDefault(x => x.Id == 2));
            _mockQuestionRepo.Setup(x => x.GetById(3)).Returns(_listQuestions.FirstOrDefault(x => x.Id == 3));
            _mockQuestionRepo.Setup(x => x.GetById(4)).Returns(_listQuestions.FirstOrDefault(x => x.Id == 4));

            _assessmentService.GenerateResult(_mapper.Map<AssessmentDTO>(_Assessment).Id, _questionary.Id, EnumHelper.PermissionsUser.Admin);

            _mockQuestionRepo.Verify(x => x.GetById(1), Times.Once);
            _mockQuestionRepo.Verify(x => x.GetById(2), Times.Once);
            _mockQuestionRepo.Verify(x => x.GetById(3), Times.Once);
            _mockQuestionRepo.Verify(x => x.GetById(4), Times.Once);
        }

        [Fact]
        public void CanAnAdminSeeTheScoreForAnAssessment_IRecieveAnAssessmentWithAPercentage_50()
        {
            Init();

            _Assessment.ListOfQuestionAndAnswerOfAssessment = _listQuestionsAndAnswersOfAssessments;

            _mockAssessmentRepo.Setup(x => x.GetById(_Assessment.Id)).Returns(_Assessment);
            _mockQuestionRepo.Setup(x => x.GetById(1)).Returns(_listQuestions.FirstOrDefault(x => x.Id == 1));
            _mockQuestionRepo.Setup(x => x.GetById(2)).Returns(_listQuestions.FirstOrDefault(x => x.Id == 2));
            _mockQuestionRepo.Setup(x => x.GetById(3)).Returns(_listQuestions.FirstOrDefault(x => x.Id == 3));
            _mockQuestionRepo.Setup(x => x.GetById(4)).Returns(_listQuestions.FirstOrDefault(x => x.Id == 4));

            var assessment = _assessmentService.GenerateResult(_mapper.Map<AssessmentDTO>(_Assessment).Id, _questionary.Id,
                EnumHelper.PermissionsUser.Admin);

            Assert.Equal(50d, assessment.AssessmentPercentage);
        }

        #endregion Tests

    }
}
