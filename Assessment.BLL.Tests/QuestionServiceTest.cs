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
    public class QuestionServiceTest
    {
        private IMapper _mapper;

        public QuestionServiceTest()
        {
            MapperProfile map = new MapperProfile();
            _mapper = map.Mapper;
        }
        [Fact]
        public void CanAnAdminCreateANewQuestion_TheGetByIdOfQuestionaryMethodMustBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionaryRepo = factory.Create<IQuestionaryRepo>();
            var mockQuestionRepo = factory.Create<IQuestionRepo>();

            Questionary questionary = new Questionary { Id = 1, Description = "Eerste questionary" };
            QuestionDTO questionDto = new QuestionDTO { Id = 20, QuestionPhrase = "Eerste vraag", QuestionaryId = 1 };

            QuestionService questionService = new QuestionService(mockQuestionaryRepo.Object, mockQuestionRepo.Object);

            questionService.AddQuestion(questionDto, EnumHelper.PermissionsUser.Admin);

            mockQuestionaryRepo.Verify(x => x.GetById(questionary.Id), Times.Once);
        }
        [Fact]
        public void CanAnAdminCreateANewQuestion_TheAddOfQuestionMethodMustBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionaryRepo = factory.Create<IQuestionaryRepo>();
            var mockQuestionRepo = factory.Create<IQuestionRepo>();

            Questionary questionary = new Questionary { Id = 1, Description = "Eerste questionary" };
            QuestionDTO questionDto = new QuestionDTO { Id = 20, QuestionPhrase = "Eerste vraag", QuestionaryId = 1 };

            QuestionService questionService = new QuestionService(mockQuestionaryRepo.Object, mockQuestionRepo.Object);


            mockQuestionaryRepo.Setup(x => x.GetById(questionDto.QuestionaryId)).Returns(questionary);

            questionService.AddQuestion(questionDto, EnumHelper.PermissionsUser.Admin);

            mockQuestionRepo.Verify(x => x.Add(_mapper.Map<Question>(questionDto)), Times.Once);
        }

        [Fact]
        public void CanAnAdminCreateANewQuestion_IReceiveAQuestion()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionaryRepo = factory.Create<IQuestionaryRepo>();
            var mockQuestionRepo = factory.Create<IQuestionRepo>();

            Questionary questionary = new Questionary
            {
                Id = 1,
                Description = "Eerste questionary",
                Questions = new List<Question>()
            };
            Question Question = new Question
            {
                Id = 20,
                QuestionPhrase = "Eerste vraag",
                QuestionaryId = 1,
                Answers = new List<Answer>(),
                QuestionAndAnswerOfAssessment = new List<QuestionAndAnswerOfAssessment>()
            };
            Question p = new Question();
            QuestionService questionService = new QuestionService(mockQuestionaryRepo.Object, mockQuestionRepo.Object);

            mockQuestionaryRepo.Setup(x => x.GetById(Question.QuestionaryId)).Returns(questionary);
            mockQuestionRepo.Setup(x => x.Add(Question)).Returns(Question);

            var response = questionService.AddQuestion(_mapper.Map<QuestionDTO>(Question), EnumHelper.PermissionsUser.Admin);
            Assert.Equal(Question.Id, response.Id);
            Assert.Equal(Question.QuestionPhrase, response.QuestionPhrase);
            Assert.Equal(Question.QuestionaryId, response.QuestionaryId);

        }

        [Fact]
        public void CanAnOtherUserThanAdminCreateANewQuestion_TheGetByIdOfQuestionaryMethodMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionaryRepo = factory.Create<IQuestionaryRepo>();
            var mockQuestionRepo = factory.Create<IQuestionRepo>();

            Questionary questionary = new Questionary
            {
                Id = 1,
                Description = "Eerste questionary",
                Questions = new List<Question>()
            };
            QuestionDTO QuestionDTO = new QuestionDTO
            {
                Id = 20,
                QuestionPhrase = "Eerste vraag",
                QuestionaryId = 1,
                Answers = new List<AnswerDTO>(),
                QuestionAndAnswerOfAssessment = new List<QuestionAndAnswerOfAssessmentDTO>()
            };

            QuestionService questionService = new QuestionService(mockQuestionaryRepo.Object, mockQuestionRepo.Object);

            mockQuestionaryRepo.Setup(x => x.GetById(questionary.Id)).Returns(questionary);

            questionService.AddQuestion(QuestionDTO, EnumHelper.PermissionsUser.GDPR);
            questionService.AddQuestion(QuestionDTO, EnumHelper.PermissionsUser.Write);
            questionService.AddQuestion(QuestionDTO, EnumHelper.PermissionsUser.Read);
            questionService.AddQuestion(QuestionDTO, EnumHelper.PermissionsUser.Owner);

            mockQuestionaryRepo.Verify(x => x.GetById(questionary.Id), Times.Never);
        }

        [Fact]
        public void CanAnOtherUserThanAdminCreateANewQuestion_TheAddOfQuestionMethodMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionaryRepo = factory.Create<IQuestionaryRepo>();
            var mockQuestionRepo = factory.Create<IQuestionRepo>();

            Questionary questionary = new Questionary
            {
                Id = 1,
                Description = "Eerste questionary",
                Questions = new List<Question>()
            };
            QuestionDTO QuestionDTO = new QuestionDTO
            {
                Id = 20,
                QuestionPhrase = "Eerste vraag",
                QuestionaryId = 1,
                Answers = new List<AnswerDTO>(),
                QuestionAndAnswerOfAssessment = new List<QuestionAndAnswerOfAssessmentDTO>()
            };

            QuestionService questionService = new QuestionService(mockQuestionaryRepo.Object, mockQuestionRepo.Object);

            mockQuestionaryRepo.Setup(x => x.GetById(questionary.Id)).Returns(questionary);

            questionService.AddQuestion(QuestionDTO, EnumHelper.PermissionsUser.GDPR);
            questionService.AddQuestion(QuestionDTO, EnumHelper.PermissionsUser.Write);
            questionService.AddQuestion(QuestionDTO, EnumHelper.PermissionsUser.Read);
            questionService.AddQuestion(QuestionDTO, EnumHelper.PermissionsUser.Owner);

            mockQuestionRepo.Verify(x => x.Add(_mapper.Map<Question>(QuestionDTO)), Times.Never);
        }


        [Fact]
        public void CanAnAdminGetAQuestionById_TheGetByIdMethodMustBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionaryRepo = factory.Create<IQuestionaryRepo>();
            var mockQuestionRepo = factory.Create<IQuestionRepo>();

            QuestionService questionService = new QuestionService(mockQuestionaryRepo.Object, mockQuestionRepo.Object);

            questionService.GetQuestionById(20, EnumHelper.PermissionsUser.Admin);

            mockQuestionRepo.Verify(x => x.GetById(20), Times.Once);
        }

        [Fact]
        public void CanAnAdminGetAQuestionById_IReceiveTheCorrectQuestion()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionaryRepo = factory.Create<IQuestionaryRepo>();
            var mockQuestionRepo = factory.Create<IQuestionRepo>();

            Question Question = new Question
            {
                Id = 20,
                QuestionPhrase = "Eerste vraag",
                QuestionaryId = 1
            };

            QuestionService questionService = new QuestionService(mockQuestionaryRepo.Object, mockQuestionRepo.Object);

            mockQuestionRepo.Setup(x => x.GetById(20)).Returns(Question);

            var returnedQuestion = questionService.GetQuestionById(20, EnumHelper.PermissionsUser.Admin);
            Assert.Equal(Question.Id, returnedQuestion.Id);
            Assert.Equal(Question.QuestionPhrase, returnedQuestion.QuestionPhrase);
            Assert.Equal(Question.QuestionaryId, returnedQuestion.QuestionaryId);

        }

        [Fact]
        public void CanAnOtherUserThanAdminGetAQuestionById_IReceiveNoQuestion()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionaryRepo = factory.Create<IQuestionaryRepo>();
            var mockQuestionRepo = factory.Create<IQuestionRepo>();

            Questionary questionary = new Questionary { Id = 1, Description = "Eerste questionary" };
            Question Question = new Question { Id = 20, QuestionPhrase = "Eerste vraag", QuestionaryId = 1 };
            questionary.Questions = new List<Question> { Question };

            QuestionService questionService = new QuestionService(mockQuestionaryRepo.Object, mockQuestionRepo.Object);

            mockQuestionRepo.Setup(x => x.GetById(20)).Returns(Question);

            questionService.GetQuestionById(20, EnumHelper.PermissionsUser.GDPR);
            questionService.GetQuestionById(20, EnumHelper.PermissionsUser.Owner);
            questionService.GetQuestionById(20, EnumHelper.PermissionsUser.Read);
            questionService.GetQuestionById(20, EnumHelper.PermissionsUser.Write);

            mockQuestionRepo.Verify(x => x.GetById(20), Times.Never);
        }

        [Fact]
        public void CanAnAdminGetAllQuestionByQuestionaryId_IReceiveAListOfQuestions()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionaryRepo = factory.Create<IQuestionaryRepo>();
            var mockQuestionRepo = factory.Create<IQuestionRepo>();

            Questionary questionary = new Questionary
            {
                Id = 1,
                Description = "Eerste questionary",
            };

            var listOfQuestions = new List<Question>
            {
                new Question
                {
                    Id = 20,
                    QuestionPhrase = "Eerste vraag",
                    QuestionaryId = 1,
                },
                new Question
                {
                    Id = 21,
                    QuestionPhrase = "Tweede vraag",
                    QuestionaryId = 1,
                }
            };

            QuestionService questionService = new QuestionService(mockQuestionaryRepo.Object, mockQuestionRepo.Object);

            mockQuestionaryRepo.Setup(x => x.Add(questionary)).Returns(questionary);

            mockQuestionRepo.Setup(x => x.GetAllQuestionsByQuestionaryId(questionary.Id)).Returns(listOfQuestions.AsQueryable());

            var response = questionService.GetAllQuestionsByQuestionaryId(questionary.Id, EnumHelper.PermissionsUser.Admin);

            Assert.Equal(listOfQuestions.FirstOrDefault().Id, response.FirstOrDefault().Id);
            Assert.Equal(listOfQuestions.FirstOrDefault().QuestionPhrase, response.FirstOrDefault().QuestionPhrase);

        }

        [Fact]
        public void CanAnAdminUpdateAQuestion_TheUpdateOfQuestionMethodMustBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionaryRepo = factory.Create<IQuestionaryRepo>();
            var mockQuestionRepo = factory.Create<IQuestionRepo>();

            QuestionaryDTO questionaryDto = new QuestionaryDTO { Id = 1, Description = "Eerste questionary" };
            QuestionDTO QuestionDto1 = new QuestionDTO { Id = 20, QuestionPhrase = "Eerste vraag", QuestionaryId = 1 };
            QuestionDTO QuestionDto2 = new QuestionDTO { Id = 21, QuestionPhrase = "Tweede vraag", QuestionaryId = 1 };
            questionaryDto.Questions = new List<QuestionDTO> { QuestionDto1, QuestionDto2 };

            QuestionService questionService = new QuestionService(mockQuestionaryRepo.Object, mockQuestionRepo.Object);

            mockQuestionRepo.Setup(x => x.GetById(20)).Returns(_mapper.Map<Question>(QuestionDto1));
            QuestionDto1.QuestionPhrase = "Eerste vraag, is geupdate";

            questionService.UpdateQuestion(QuestionDto1, EnumHelper.PermissionsUser.Admin);

            mockQuestionRepo.Verify(x => x.Update(_mapper.Map<Question>(QuestionDto1)), Times.Once);
        }

        [Fact]
        public void CanAnAdminUpdateAQuestion_IReceiveTheUpdatedQuestion()
        {
            Question Question1 = new Question
            {
                Id = 20,
                QuestionPhrase = "Eerste vraag",
                QuestionaryId = 1,
                Answers = new List<Answer>(),
                QuestionAndAnswerOfAssessment = new List<QuestionAndAnswerOfAssessment>()
            };

            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionaryRepo = factory.Create<IQuestionaryRepo>();
            var mockQuestionRepo = factory.Create<IQuestionRepo>();

            Question1.QuestionPhrase = "Eerste vraag, is geüpdatet";

            QuestionService questionService = new QuestionService(mockQuestionaryRepo.Object, mockQuestionRepo.Object);

            mockQuestionRepo.Setup(x => x.Update(Question1)).Returns(Question1);


            var returnValue = questionService.UpdateQuestion(_mapper.Map<QuestionDTO>(Question1), EnumHelper.PermissionsUser.Admin);

            Assert.Equal(Question1.Id, returnValue.Id);
            Assert.Equal(Question1.QuestionPhrase, returnValue.QuestionPhrase);
        }

        [Fact]
        public void CanAnOtherUserThanAdminUpdateAQuestion_TheUpdateOfQuestionMethodMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionaryRepo = factory.Create<IQuestionaryRepo>();
            var mockQuestionRepo = factory.Create<IQuestionRepo>();

            Questionary questionary = new Questionary { Id = 1, Description = "Eerste questionary" };
            QuestionDTO QuestionDto1 = new QuestionDTO
            {
                Id = 20,
                QuestionPhrase = "Eerste vraag",
                QuestionaryId = 1,
                Answers = new List<AnswerDTO>(),
                QuestionAndAnswerOfAssessment = new List<QuestionAndAnswerOfAssessmentDTO>()
            };

            QuestionService questionService = new QuestionService(mockQuestionaryRepo.Object, mockQuestionRepo.Object);

            questionService.UpdateQuestion(QuestionDto1, EnumHelper.PermissionsUser.GDPR);
            questionService.UpdateQuestion(QuestionDto1, EnumHelper.PermissionsUser.Owner);
            questionService.UpdateQuestion(QuestionDto1, EnumHelper.PermissionsUser.Read);
            questionService.UpdateQuestion(QuestionDto1, EnumHelper.PermissionsUser.Write);

            mockQuestionRepo.Verify(x => x.Update(_mapper.Map<Question>(QuestionDto1)), Times.Never);
        }


    }
}
