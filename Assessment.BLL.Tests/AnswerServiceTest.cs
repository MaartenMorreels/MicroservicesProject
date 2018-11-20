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
    public class AnswerServiceTest
    {
        #region private fields

        private IMapper _mapper;

        #endregion private fields

        #region public constructor

        public AnswerServiceTest()
        {
            MapperProfile map = new MapperProfile();
            _mapper = map.Mapper;
        }
        #endregion constructor

        #region public methods

        [Fact]
        public void CanAnAdminCreateANewAnswerForAQuestion_TheGetByIdOfQuestionMethodMustBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionaryRepo = factory.Create<IQuestionaryRepo>();
            var mockQuestionRepo = factory.Create<IQuestionRepo>();
            var mockAnswerRepo = factory.Create<IAnswerRepo>();

            Questionary questionary = new Questionary { Id = 1, Description = "Eerste questionary" };
            Question question = new Question { Id = 20, QuestionPhrase = "Eerste vraag", QuestionaryId = 1 };
            Answer answer = new Answer { Id = 30, Correct = true, Text = "Eerste Antwoord op de eerste vraag", QuestionId = 20 };

            AnswerService answerService = new AnswerService(mockAnswerRepo.Object, mockQuestionRepo.Object, mockQuestionaryRepo.Object);
            
            
            mockQuestionaryRepo.Setup(x => x.GetById(questionary.Id)).Returns(questionary);
            mockQuestionRepo.Setup(x => x.GetById(question.Id)).Returns(question);

            answerService.AddAnswer(_mapper.Map<AnswerDTO>(answer), EnumHelper.PermissionsUser.Admin);

            mockQuestionRepo.Verify(x => x.GetById(question.Id), Times.Once);

        }

        [Fact]
        public void CanAnAdminCreateANewAnswerForAQuestion_TheAddAnswerMethodMustBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionaryRepo = factory.Create<IQuestionaryRepo>();
            var mockQuestionRepo = factory.Create<IQuestionRepo>();
            var mockAnswerRepo = factory.Create<IAnswerRepo>();

            Questionary questionary = new Questionary { Id = 1, Description = "Eerste questionary" };
            Question question = new Question { Id = 20, QuestionPhrase = "Eerste vraag", QuestionaryId = 1 };
            Answer answer = new Answer { Id = 30, Correct = true, Text = "Eerste Antwoord op de eerste vraag", QuestionId = 20 };


            AnswerService answerService = new AnswerService(mockAnswerRepo.Object, mockQuestionRepo.Object, mockQuestionaryRepo.Object);

            mockQuestionaryRepo.Setup(x => x.GetById(questionary.Id)).Returns(questionary);
            mockQuestionRepo.Setup(x => x.GetById(question.Id)).Returns(question);

            answerService.AddAnswer(_mapper.Map<AnswerDTO>(answer), EnumHelper.PermissionsUser.Admin);

            mockAnswerRepo.Verify(x => x.Add(answer), Times.Once);

        }

        [Fact]
        public void CanAnAdminCreateANewAnswerForAQuestion_IRecieveTheCreatedAnswer()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionaryRepo = factory.Create<IQuestionaryRepo>();
            var mockQuestionRepo = factory.Create<IQuestionRepo>();
            var mockAnswerRepo = factory.Create<IAnswerRepo>();

            Questionary questionary = new Questionary { Id = 1, Description = "Eerste questionary" };
            Question question = new Question { Id = 20, QuestionPhrase = "Eerste vraag", QuestionaryId = 1 };
            Answer answer = new Answer { Id = 30, Correct = true, Text = "Eerste Antwoord op de eerste vraag", QuestionId = 20 };


            AnswerService answerService = new AnswerService(mockAnswerRepo.Object, mockQuestionRepo.Object, mockQuestionaryRepo.Object);

            mockQuestionaryRepo.Setup(x => x.GetById(questionary.Id)).Returns(questionary);
            mockQuestionRepo.Setup(x => x.GetById(question.Id)).Returns(question);
            mockAnswerRepo.Setup(x => x.Add(answer)).Returns(answer);

            var response = answerService.AddAnswer(_mapper.Map<AnswerDTO>(answer), EnumHelper.PermissionsUser.Admin);

            Assert.Equal(answer.Id, response.Id);
            Assert.Equal(answer.Correct, response.Correct);
            Assert.Equal(answer.Text, response.Text);
            
        }

        [Fact]
        public void CanAnOtherUserThanAdminCreateANewAnswerForAQuestion_TheGetByIDOfQuestionaryMethodMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionaryRepo = factory.Create<IQuestionaryRepo>();
            var mockQuestionRepo = factory.Create<IQuestionRepo>();
            var mockAnswerRepo = factory.Create<IAnswerRepo>();

            Questionary questionary = new Questionary { Id = 1, Description = "Eerste questionary" };
            Question question = new Question { Id = 20, QuestionPhrase = "Eerste vraag", QuestionaryId = 1 };
            Answer answer = new Answer { Id = 30, Correct = true, Text = "Eerste Antwoord op de eerste vraag", QuestionId = 20 };


            AnswerService answerService = new AnswerService(mockAnswerRepo.Object, mockQuestionRepo.Object, mockQuestionaryRepo.Object);

            mockQuestionaryRepo.Setup(x => x.GetById(questionary.Id)).Returns(questionary);

            answerService.AddAnswer(_mapper.Map<AnswerDTO>(answer), EnumHelper.PermissionsUser.GDPR);
            answerService.AddAnswer(_mapper.Map<AnswerDTO>(answer), EnumHelper.PermissionsUser.Owner);
            answerService.AddAnswer(_mapper.Map<AnswerDTO>(answer), EnumHelper.PermissionsUser.Read);
            answerService.AddAnswer(_mapper.Map<AnswerDTO>(answer), EnumHelper.PermissionsUser.Write);

            mockQuestionaryRepo.Verify(x => x.GetById(questionary.Id), Times.Never);

        }

        [Fact]
        public void CanAnOtherUserThanAdminCreateANewAnswerForAQuestion_TheGetByIdOfQuestionMethodMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionaryRepo = factory.Create<IQuestionaryRepo>();
            var mockQuestionRepo = factory.Create<IQuestionRepo>();
            var mockAnswerRepo = factory.Create<IAnswerRepo>();

            Questionary questionary = new Questionary { Id = 1, Description = "Eerste questionary" };
            Question question = new Question { Id = 20, QuestionPhrase = "Eerste vraag", QuestionaryId = 1 };
            Answer answer = new Answer { Id = 30, Correct = true, Text = "Eerste Antwoord op de eerste vraag", QuestionId = 20 };


            AnswerService answerService = new AnswerService(mockAnswerRepo.Object, mockQuestionRepo.Object, mockQuestionaryRepo.Object);

            mockQuestionaryRepo.Setup(x => x.GetById(questionary.Id)).Returns(questionary);
            mockQuestionRepo.Setup(x => x.GetById(question.Id)).Returns(question);

            answerService.AddAnswer(_mapper.Map<AnswerDTO>(answer), EnumHelper.PermissionsUser.GDPR);
            answerService.AddAnswer(_mapper.Map<AnswerDTO>(answer), EnumHelper.PermissionsUser.Owner);
            answerService.AddAnswer(_mapper.Map<AnswerDTO>(answer), EnumHelper.PermissionsUser.Read);
            answerService.AddAnswer(_mapper.Map<AnswerDTO>(answer), EnumHelper.PermissionsUser.Write);

            mockQuestionRepo.Verify(x => x.GetById(question.Id), Times.Never);

        }

        [Fact]
        public void CanAnOtherUserThanAdminCreateANewAnswerForAQuestion_TheAddAnswerMethodMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionaryRepo = factory.Create<IQuestionaryRepo>();
            var mockQuestionRepo = factory.Create<IQuestionRepo>();
            var mockAnswerRepo = factory.Create<IAnswerRepo>();

            Questionary questionary = new Questionary { Id = 1, Description = "Eerste questionary" };
            Question question = new Question { Id = 20, QuestionPhrase = "Eerste vraag", QuestionaryId = 1 };
            Answer answer = new Answer { Id = 30, Correct = true, Text = "Eerste Antwoord op de eerste vraag", QuestionId = 20 };


            AnswerService answerService = new AnswerService(mockAnswerRepo.Object, mockQuestionRepo.Object, mockQuestionaryRepo.Object);

            mockQuestionaryRepo.Setup(x => x.GetById(questionary.Id)).Returns(questionary);
            mockQuestionRepo.Setup(x => x.GetById(question.Id)).Returns(question);

            answerService.AddAnswer(_mapper.Map<AnswerDTO>(answer), EnumHelper.PermissionsUser.GDPR);
            answerService.AddAnswer(_mapper.Map<AnswerDTO>(answer), EnumHelper.PermissionsUser.Owner);
            answerService.AddAnswer(_mapper.Map<AnswerDTO>(answer), EnumHelper.PermissionsUser.Read);
            answerService.AddAnswer(_mapper.Map<AnswerDTO>(answer), EnumHelper.PermissionsUser.Write);

            mockAnswerRepo.Verify(x => x.Add(answer), Times.Never);

        }

        [Fact]
        public void CanAnAdminGetAnAnswerById_IRecieveAnAnswer()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionaryRepo = factory.Create<IQuestionaryRepo>();
            var mockQuestionRepo = factory.Create<IQuestionRepo>();
            var mockAnswerRepo = factory.Create<IAnswerRepo>();

            Answer answer = new Answer { Id = 30, Correct = true, Text = "Eerste Antwoord op de eerste vraag", QuestionId = 20 };

            AnswerService answerService = new AnswerService(mockAnswerRepo.Object, mockQuestionRepo.Object, mockQuestionaryRepo.Object);

            mockAnswerRepo.Setup(x => x.GetById(answer.Id)).Returns(answer);

            var response = answerService.GetAnswerById(answer.Id, EnumHelper.PermissionsUser.Admin);

            Assert.Equal(answer.Id, response.Id);
            Assert.Equal(answer.Correct, response.Correct);
            Assert.Equal(answer.Text, response.Text);
        }

        [Fact]
        public void CanAnOtherUserThanAdminGetAnAnswerById_IRecieveAnAnswer()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionaryRepo = factory.Create<IQuestionaryRepo>();
            var mockQuestionRepo = factory.Create<IQuestionRepo>();
            var mockAnswerRepo = factory.Create<IAnswerRepo>();

            Answer answer = new Answer { Id = 30, Correct = true, Text = "Eerste Antwoord op de eerste vraag", QuestionId = 20 };

            AnswerService answerService = new AnswerService(mockAnswerRepo.Object, mockQuestionRepo.Object, mockQuestionaryRepo.Object);

            var answerDto = _mapper.Map<AnswerDTO>(answer);

            mockAnswerRepo.Setup(x => x.GetById(answer.Id)).Returns(answer);

            answerService.GetAnswerById(answerDto.Id, EnumHelper.PermissionsUser.GDPR);
            answerService.GetAnswerById(answerDto.Id, EnumHelper.PermissionsUser.Owner);
            answerService.GetAnswerById(answerDto.Id, EnumHelper.PermissionsUser.Read);
            answerService.GetAnswerById(answerDto.Id, EnumHelper.PermissionsUser.Write);

            mockAnswerRepo.Verify(x => x.GetById(answer.Id), Times.Never);
        }

        [Fact]
        public void CanAnAdminUpdateAnAnswer_TheUpdateOfAnswerMethodMustBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionaryRepo = factory.Create<IQuestionaryRepo>();
            var mockQuestionRepo = factory.Create<IQuestionRepo>();
            var mockAnswerRepo = factory.Create<IAnswerRepo>();

            Answer answer = new Answer { Id = 30, Correct = true, Text = "Eerste Antwoord op de eerste vraag", QuestionId = 20 };

            AnswerService answerService = new AnswerService(mockAnswerRepo.Object, mockQuestionRepo.Object, mockQuestionaryRepo.Object);

            answerService.UpdateAnswer(_mapper.Map<AnswerDTO>(answer), EnumHelper.PermissionsUser.Admin);

            mockAnswerRepo.Verify(x => x.Update(answer), Times.Once());
        }

        [Fact]
        public void CanAnAdminUpdateAnAnswer_IRecieveTheUpdatedAnswer()
        {
            Answer answer = new Answer { Id = 30, Correct = true, Text = "Eerste Antwoord op de eerste vraag", QuestionId = 20 };

            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionaryRepo = factory.Create<IQuestionaryRepo>();
            var mockQuestionRepo = factory.Create<IQuestionRepo>();
            var mockAnswerRepo = factory.Create<IAnswerRepo>();

            answer.Text = "dit antwoord is veranderd";

            AnswerService answerService = new AnswerService(mockAnswerRepo.Object, mockQuestionRepo.Object, mockQuestionaryRepo.Object);
            mockAnswerRepo.Setup(x => x.Update(answer)).Returns(answer);

            var response = answerService.UpdateAnswer(_mapper.Map<AnswerDTO>(answer), EnumHelper.PermissionsUser.Admin);

            Assert.Equal(answer.Id, response.Id);
            Assert.Equal(answer.Correct, response.Correct);
            Assert.Equal(answer.Text, response.Text);
            
        }

        [Fact]
        public void CanAnOtherUserThanAdminUpdateAnAnswer_TheUpdateOfAnswerMethodOMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionaryRepo = factory.Create<IQuestionaryRepo>();
            var mockQuestionRepo = factory.Create<IQuestionRepo>();
            var mockAnswerRepo = factory.Create<IAnswerRepo>();

            Answer answer = new Answer { Id = 30, Correct = true, Text = "Eerste Antwoord op de eerste vraag", QuestionId = 20 };

            AnswerService answerService = new AnswerService(mockAnswerRepo.Object, mockQuestionRepo.Object, mockQuestionaryRepo.Object);

            mockAnswerRepo.Setup(x => x.GetById(answer.Id)).Returns(answer);

            answerService.UpdateAnswer(_mapper.Map<AnswerDTO>(answer), EnumHelper.PermissionsUser.GDPR);
            answerService.UpdateAnswer(_mapper.Map<AnswerDTO>(answer), EnumHelper.PermissionsUser.Owner);
            answerService.UpdateAnswer(_mapper.Map<AnswerDTO>(answer), EnumHelper.PermissionsUser.Read);
            answerService.UpdateAnswer(_mapper.Map<AnswerDTO>(answer), EnumHelper.PermissionsUser.Write);

            mockAnswerRepo.Verify(x => x.Update(answer), Times.Never);
        }

        [Fact]
        public void CanAnAdminGetAllAnswerLinkedToAQuestion_IRecieveAnListOffAnswers()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionaryRepo = factory.Create<IQuestionaryRepo>();
            var mockQuestionRepo = factory.Create<IQuestionRepo>();
            var mockAnswerRepo = factory.Create<IAnswerRepo>();

            Question question = new Question { Id = 20, QuestionPhrase = "Eerste vraag", QuestionaryId = 1 };
            List<Answer> listAnswers = new List<Answer>
            {
                new Answer { Id = 30, Correct = true, Text = "Eerste Antwoord op de eerste vraag", QuestionId = 20 },
                new Answer { Id = 31, Correct = true, Text = "Tweede Antwoord op de eerste vraag", QuestionId = 20 }

            };

            AnswerService answerService = new AnswerService(mockAnswerRepo.Object, mockQuestionRepo.Object, mockQuestionaryRepo.Object);
            mockAnswerRepo.SetupSequence(x => x.GetAll()).Returns(listAnswers.AsQueryable());

            var response = answerService.GetAllAnswersByQuestionId(question.Id, EnumHelper.PermissionsUser.Admin);

            Assert.Equal(listAnswers.FirstOrDefault().Id, response.FirstOrDefault().Id);
            Assert.Equal(listAnswers.FirstOrDefault().Correct, response.FirstOrDefault().Correct);
            Assert.Equal(response.FirstOrDefault().Text, response.FirstOrDefault().Text);
        }

    }
    #endregion public methods
}
