using System;
using System.Collections.Generic;
using System.Linq;
using Assessment.BLL.DTOs;
using Assessment.BLL.Services;
using Assessment.BLL.Helper;
using Assessment.DAL.Entities;
using Assessment.DAL.Repositories.Interfaces;
using AutoMapper;
using Moq;

using Xunit;

namespace Assessment.BLL.Tests
{
    public class QuestionAndAnswerOfAssessmentServiceTest
    {
        private int _ownerId = 123456789;
        private MockRepository _factory;
        private Mock<IQuestionaryRepo> _mockQuestionaryRepo;
        private Mock<IQuestionRepo> _mockQuestionRepo;
        private Mock<IAnswerRepo> _mockAnswerRepo;
        private Mock<IAssessmentRepo> _mockAssessmentRepo;
        private List<Question> _listOfQuestions;
        private DAL.Entities.Assessment _Assessment = new DAL.Entities.Assessment();
        private IMapper _mapper;
        private QuestionAndAnswerOfAssessmentDTO _questionAndAnswerOfAssessmentDto;
        private Mock<IQuestionsAndAnswersOfAssessmentREPO> _mockQuestionsAndAnswersOfAssessmentRepo;

        public QuestionAndAnswerOfAssessmentServiceTest()
        {
            MapperProfile map = new MapperProfile();
            _mapper = map.Mapper;
        }

        public void Init()
        {
            _factory = new MockRepository(MockBehavior.Loose);
            _mockQuestionaryRepo = _factory.Create<IQuestionaryRepo>();
            _mockQuestionRepo = _factory.Create<IQuestionRepo>();
            _mockAnswerRepo = _factory.Create<IAnswerRepo>();
            _mockAssessmentRepo = _factory.Create<IAssessmentRepo>();
            _mockQuestionsAndAnswersOfAssessmentRepo = _factory.Create<IQuestionsAndAnswersOfAssessmentREPO>();

            _listOfQuestions = new List<Question>
            {
                new Question{Id = 1,QuestionPhrase = "Dit is de eerste test.", QuestionaryId = 1, AllowedTime = 30, QuestionDifficultyId = 31,
                    Answers = new List<Answer>
                    {
                        new Answer{Correct = true,Id = 1,Text = "correct antwoord"}
                    }

                },
                new Question
                {
                    Id = 2,QuestionPhrase = "Dit is de tweede test.", QuestionaryId = 1, AllowedTime = 30, QuestionDifficultyId = 31,
                    Answers = new List<Answer>
                    {
                        new Answer{Correct = false,Id = 2,Text = "foutief antwoord"}
                    }
                },
                new Question{Id = 3,QuestionPhrase = "Dit is de derde test.", QuestionaryId = 1, AllowedTime = 30, QuestionDifficultyId = 31},
                new Question{Id = 4,QuestionPhrase = "Dit is de vierde test.", QuestionaryId = 1, AllowedTime = 30, QuestionDifficultyId = 32},
                new Question{Id = 5,QuestionPhrase = "Dit is de vijfde test.", QuestionaryId = 1, AllowedTime = 30, QuestionDifficultyId = 31}
            };

            _Assessment = new DAL.Entities.Assessment
            {
                Id = 12,
                AssessmentIdentifier = Guid.NewGuid(),
                OwnerId = _ownerId,
                Feedback = "Dit is de  eerste assessment.",
                ListOfQuestionAndAnswerOfAssessment = new List<QuestionAndAnswerOfAssessment>
                {
                    new QuestionAndAnswerOfAssessment
                    {
                        AssessmentId = 12,
                        AnswerId = 1,
                        QuestionId = 1,
                        OwnerId = _Assessment.OwnerId,
                    },
                    new QuestionAndAnswerOfAssessment
                    {
                        AssessmentId=12,
                        AnswerId=1,
                        QuestionId=2,
                        OwnerId=_Assessment.OwnerId
                        
                    }    
                }
            };

            _questionAndAnswerOfAssessmentDto = _mapper.Map<QuestionAndAnswerOfAssessmentDTO>(_Assessment.ListOfQuestionAndAnswerOfAssessment.FirstOrDefault());
        }

        [Fact]
        public void AnOwnerCanGiveAnAnswerToAnAssessment_AssessmentGetByIdMethodMustBeCalled()
        {
            Init();
            QuestionAndAnswerOfAssessmentService questionAndAnswerOfAssessmentService = new QuestionAndAnswerOfAssessmentService(_mockQuestionRepo.Object, _mockAssessmentRepo.Object,_mockQuestionsAndAnswersOfAssessmentRepo.Object);
           
            _mockQuestionRepo.Setup(x => x.GetById(_Assessment.ListOfQuestionAndAnswerOfAssessment.FirstOrDefault().QuestionId));
            _mockAssessmentRepo.Setup(x => x.GetById(_Assessment.ListOfQuestionAndAnswerOfAssessment.FirstOrDefault().AssessmentId));

            questionAndAnswerOfAssessmentService.AddAnAnswerOfAssessment(_questionAndAnswerOfAssessmentDto, EnumHelper.PermissionsUser.Owner);

            _mockAssessmentRepo.Verify(x => x.GetById(_Assessment.ListOfQuestionAndAnswerOfAssessment.FirstOrDefault().AssessmentId), Times.Once);
        }

        [Fact]
        public void CanAnOtherUserThanAnOwnerCanGiveAnswerToAnAssessment_GetByIdOfAssessmentMethodMustNotBeCalled()
        {
            Init();
            QuestionAndAnswerOfAssessmentService questionAndAnswerOfAssessmentService = new QuestionAndAnswerOfAssessmentService(_mockQuestionRepo.Object, _mockAssessmentRepo.Object, _mockQuestionsAndAnswersOfAssessmentRepo.Object);

            _mockQuestionRepo.Setup(x => x.GetById(_Assessment.ListOfQuestionAndAnswerOfAssessment.FirstOrDefault().QuestionId));
            _mockAssessmentRepo.Setup(x => x.GetById(_Assessment.ListOfQuestionAndAnswerOfAssessment.FirstOrDefault().AssessmentId));
            

            questionAndAnswerOfAssessmentService.AddAnAnswerOfAssessment(_questionAndAnswerOfAssessmentDto, EnumHelper.PermissionsUser.Admin);
            questionAndAnswerOfAssessmentService.AddAnAnswerOfAssessment(_questionAndAnswerOfAssessmentDto, EnumHelper.PermissionsUser.Read);
            questionAndAnswerOfAssessmentService.AddAnAnswerOfAssessment(_questionAndAnswerOfAssessmentDto, EnumHelper.PermissionsUser.Write);
            questionAndAnswerOfAssessmentService.AddAnAnswerOfAssessment(_questionAndAnswerOfAssessmentDto, EnumHelper.PermissionsUser.GDPR);


            _mockAssessmentRepo.Verify(x => x.GetById(_Assessment.ListOfQuestionAndAnswerOfAssessment.FirstOrDefault().AssessmentId), Times.Never);
        }

        [Fact]
        public void AnOwnerCanGiveanswerToAnAssessment_GetByIdOfQuestionMethodMustBeCalled()
        {
            Init();
            QuestionAndAnswerOfAssessmentService questionAndAnswerOfAssessmentService = new QuestionAndAnswerOfAssessmentService(_mockQuestionRepo.Object, _mockAssessmentRepo.Object, _mockQuestionsAndAnswersOfAssessmentRepo.Object);

            _mockQuestionRepo.Setup(x => x.GetById(_Assessment.ListOfQuestionAndAnswerOfAssessment.FirstOrDefault().QuestionId));
            _mockAssessmentRepo.Setup(x => x.GetById(_Assessment.ListOfQuestionAndAnswerOfAssessment.FirstOrDefault().AssessmentId));

            questionAndAnswerOfAssessmentService.AddAnAnswerOfAssessment(_questionAndAnswerOfAssessmentDto, EnumHelper.PermissionsUser.Owner);

            _mockQuestionRepo.Verify(x => x.GetById(_Assessment.ListOfQuestionAndAnswerOfAssessment.FirstOrDefault().QuestionId), Times.Once);
        }

        [Fact]
        public void CanAnOtherUserThanAnOwnerCanGiveanswerToAnAssessment_GetByIdOfQuestionMethodMustNotBeCalled()
        {
            Init();
            QuestionAndAnswerOfAssessmentService questionAndAnswerOfAssessmentService = new QuestionAndAnswerOfAssessmentService(_mockQuestionRepo.Object, _mockAssessmentRepo.Object, _mockQuestionsAndAnswersOfAssessmentRepo.Object);

            _mockQuestionRepo.Setup(x => x.GetById(_Assessment.ListOfQuestionAndAnswerOfAssessment.FirstOrDefault().QuestionId));
            _mockAssessmentRepo.Setup(x => x.GetById(_Assessment.ListOfQuestionAndAnswerOfAssessment.FirstOrDefault().AssessmentId));

            questionAndAnswerOfAssessmentService.AddAnAnswerOfAssessment(_questionAndAnswerOfAssessmentDto, EnumHelper.PermissionsUser.Admin);
            questionAndAnswerOfAssessmentService.AddAnAnswerOfAssessment(_questionAndAnswerOfAssessmentDto, EnumHelper.PermissionsUser.Read);
            questionAndAnswerOfAssessmentService.AddAnAnswerOfAssessment(_questionAndAnswerOfAssessmentDto, EnumHelper.PermissionsUser.Write);
            questionAndAnswerOfAssessmentService.AddAnAnswerOfAssessment(_questionAndAnswerOfAssessmentDto, EnumHelper.PermissionsUser.GDPR);

            _mockQuestionRepo.Verify(x => x.GetById(_Assessment.ListOfQuestionAndAnswerOfAssessment.FirstOrDefault().QuestionId), Times.Never);
        }

        [Fact]
        public void AnOwnerCanGiveAnAnswerToAnAssessment_IReceiveAListOfQuestionAndAnswersOfAssessment()
        {
            Init();
            QuestionAndAnswerOfAssessmentService questionAndAnswerOfAssessmentService = new QuestionAndAnswerOfAssessmentService(_mockQuestionRepo.Object, _mockAssessmentRepo.Object, _mockQuestionsAndAnswersOfAssessmentRepo.Object);

            var question = _listOfQuestions.FirstOrDefault(x => x.Id == _Assessment.ListOfQuestionAndAnswerOfAssessment.FirstOrDefault().QuestionId);
            _mockQuestionRepo.Setup(x => x.GetById(_Assessment.ListOfQuestionAndAnswerOfAssessment.FirstOrDefault().QuestionId)).Returns(question);
            _mockAssessmentRepo.Setup(x => x.GetById(_Assessment.ListOfQuestionAndAnswerOfAssessment.FirstOrDefault().AssessmentId)).Returns(_Assessment);

            _mockQuestionsAndAnswersOfAssessmentRepo.Setup(x => x.GetAll()).Returns(_Assessment.ListOfQuestionAndAnswerOfAssessment.AsQueryable());
            var response = questionAndAnswerOfAssessmentService.AddAnAnswerOfAssessment(_questionAndAnswerOfAssessmentDto, EnumHelper.PermissionsUser.Owner);

            Assert.Equal(_Assessment.ListOfQuestionAndAnswerOfAssessment.FirstOrDefault().Id, response.FirstOrDefault().Id);
            Assert.Equal(_Assessment.ListOfQuestionAndAnswerOfAssessment.FirstOrDefault().OwnerId, response.FirstOrDefault().OwnerId);
            
        }

    }
}
