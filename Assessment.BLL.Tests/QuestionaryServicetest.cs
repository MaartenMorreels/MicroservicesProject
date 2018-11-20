using Assessment.BLL.DTOs;
using Assessment.BLL.Services;
using Assessment.BLL.Helper;
using Assessment.DAL.Entities;
using Assessment.DAL.Repositories.Interfaces;
using AutoMapper;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Assessment.BLL.Tests
{
    public class QuestionaryServiceTest
    {
        private IMapper _mapper;

        public QuestionaryServiceTest()
        {
            MapperProfile map = new MapperProfile();
            _mapper = map.Mapper;
        }

        [Fact]
        public void CanAAdminCreateANewQuestionary_TheAddMethodMustBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionaryRepo = factory.Create<IQuestionaryRepo>();

            QuestionaryDTO questionaryDto = new QuestionaryDTO { Id = 1, Description = "Eerste questionaryDto" };

            QuestionaryService questionaryService = new QuestionaryService(mockQuestionaryRepo.Object);

            questionaryService.AddQuestionary(questionaryDto, EnumHelper.PermissionsUser.Admin);

            mockQuestionaryRepo.Verify(x => x.Add(_mapper.Map<Questionary>(questionaryDto)), Times.Once);
        }

        [Fact]
        public void CanAAdminCreateANewQuestionary_IReceiveTheAddedQuestionary()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionaryRepo = factory.Create<IQuestionaryRepo>();

            Questionary Questionary = new Questionary
            {
                Id = 1,
                Description = "Eerste questionaryDto",
                Questions = new List<Question>()
            };
            mockQuestionaryRepo.Setup(x => x.Add(Questionary)).Returns(Questionary);

            QuestionaryService questionaryService = new QuestionaryService(mockQuestionaryRepo.Object);

            var response = questionaryService.AddQuestionary(_mapper.Map<QuestionaryDTO>(Questionary), EnumHelper.PermissionsUser.Admin);

            Assert.Equal(Questionary.Id, response.Id);
            Assert.Equal(Questionary.Description, response.Description);
        }

        [Fact]
        public void CanAOtherUserThanAdminCanCreateAQuestionary_TheAddMethodMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionaryRepo = factory.Create<IQuestionaryRepo>();

            QuestionaryDTO questionaryDto = new QuestionaryDTO { Id = 1, Description = "Eerste questionaryDto" };

            QuestionaryService questionaryService = new QuestionaryService(mockQuestionaryRepo.Object);

            questionaryService.AddQuestionary(questionaryDto, EnumHelper.PermissionsUser.GDPR);
            questionaryService.AddQuestionary(questionaryDto, EnumHelper.PermissionsUser.Read);
            questionaryService.AddQuestionary(questionaryDto, EnumHelper.PermissionsUser.Write);
            questionaryService.AddQuestionary(questionaryDto, EnumHelper.PermissionsUser.Owner);

            mockQuestionaryRepo.Verify(x => x.Add(_mapper.Map<Questionary>(questionaryDto)), Times.Never);
        }

        [Fact]
        public void CanAAdminCreateANewQuestionaryWithNoDescription_TheAddMethodMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionaryRepo = factory.Create<IQuestionaryRepo>();

            QuestionaryDTO questionaryDto = new QuestionaryDTO { Id = 1 };

            QuestionaryService questionaryService = new QuestionaryService(mockQuestionaryRepo.Object);

            questionaryService.AddQuestionary(questionaryDto, EnumHelper.PermissionsUser.Admin);

            mockQuestionaryRepo.Verify(x => x.Add(_mapper.Map<Questionary>(questionaryDto)), Times.Never);
        }

        [Fact]
        public void CanAAdminCreateANewquestionaryWithNoDescription_IWouldLikeToHaveANullObject()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionaryRepo = factory.Create<IQuestionaryRepo>();

            QuestionaryDTO questionaryDto = new QuestionaryDTO { Id = 1 };

            QuestionaryService questionaryService = new QuestionaryService(mockQuestionaryRepo.Object);

            var responseQuestionary = questionaryService.AddQuestionary(questionaryDto, EnumHelper.PermissionsUser.Admin);

            Assert.Null(responseQuestionary);
        }

        [Fact]
        public void CanAAdminGetAQuestionaryById_TheGetByIdMethodMustBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionaryRepo = factory.Create<IQuestionaryRepo>();

            QuestionaryService questionaryService = new QuestionaryService(mockQuestionaryRepo.Object);

            questionaryService.GetQuestionaryById(1, EnumHelper.PermissionsUser.Admin);

            mockQuestionaryRepo.Verify(x => x.GetById(1), Times.Once);
        }

        [Fact]
        public void CanAAdminGetAQuestionaryById_IReceiveAQuestionary()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionaryRepo = factory.Create<IQuestionaryRepo>();

            Questionary questionary = new Questionary
            {
                Id = 1,
                Description = "Eerste questionaryDto",
            };

            mockQuestionaryRepo.Setup(x => x.GetById(questionary.Id)).Returns(questionary);

            QuestionaryService questionaryService = new QuestionaryService(mockQuestionaryRepo.Object);

            var response = questionaryService.GetQuestionaryById(1, EnumHelper.PermissionsUser.Admin);

            Assert.Equal(questionary.Id, response.Id);
            Assert.Equal(questionary.Description, response.Description);
        }

        [Fact]
        public void CanAOtherUserThanAdminGetAQuestionaryById_TheGetByIdMethodMustBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionaryRepo = factory.Create<IQuestionaryRepo>();

            QuestionaryService questionaryService = new QuestionaryService(mockQuestionaryRepo.Object);

            questionaryService.GetQuestionaryById(1, EnumHelper.PermissionsUser.GDPR);
            questionaryService.GetQuestionaryById(1, EnumHelper.PermissionsUser.Read);
            questionaryService.GetQuestionaryById(1, EnumHelper.PermissionsUser.Write);
            questionaryService.GetQuestionaryById(1, EnumHelper.PermissionsUser.Owner);

            mockQuestionaryRepo.Verify(x => x.GetById(1), Times.Never);
        }

        [Fact]
        public void CanAAdminGetAllQuestionaries_TheGetAllQuestionariesMethodMustBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionaryRepo = factory.Create<IQuestionaryRepo>();

            QuestionaryService questionaryService = new QuestionaryService(mockQuestionaryRepo.Object);

            questionaryService.GetAllQuestionaries(EnumHelper.PermissionsUser.Admin);

            mockQuestionaryRepo.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public void CanAAdminGetAllQuestionaries_IReceiveAListOfQuestionaries()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionaryRepo = factory.Create<IQuestionaryRepo>();

            var listOfQuestionaries = new List<Questionary>
            {
                 new Questionary
                {
                    Id = 1,
                    Description = "Eerste questionaryDto",
                },
                new Questionary
                {
                    Id = 2,
                    Description = "Tweede questionaryDto",
                }
            };

            mockQuestionaryRepo.Setup(x => x.GetAll()).Returns(listOfQuestionaries.AsQueryable());

            QuestionaryService questionaryService = new QuestionaryService(mockQuestionaryRepo.Object);

            var response = questionaryService.GetAllQuestionaries(EnumHelper.PermissionsUser.Admin);

            Assert.Equal(listOfQuestionaries.FirstOrDefault().Id, response.FirstOrDefault().Id);
            Assert.Equal(listOfQuestionaries.FirstOrDefault().Description, response.FirstOrDefault().Description);
        }

        [Fact]
        public void CanAOtherUserThanAdminGetAllQuestionaries_TheGetAllQuestionariesMethodMustBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionaryRepo = factory.Create<IQuestionaryRepo>();

            QuestionaryService questionaryService = new QuestionaryService(mockQuestionaryRepo.Object);

            questionaryService.GetAllQuestionaries(EnumHelper.PermissionsUser.GDPR);
            questionaryService.GetAllQuestionaries(EnumHelper.PermissionsUser.Read);
            questionaryService.GetAllQuestionaries(EnumHelper.PermissionsUser.Write);
            questionaryService.GetAllQuestionaries(EnumHelper.PermissionsUser.Owner);

            mockQuestionaryRepo.Verify(x => x.GetAll(), Times.Never);
        }

        [Fact]
        public void CanAAdminUpdateAQuestionary_TheUpdateMethodMustBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionaryRepo = factory.Create<IQuestionaryRepo>();

            QuestionaryDTO questionaryDto = new QuestionaryDTO { Id = 1, Description = "Eerste questionaryDto" };
            QuestionaryService questionaryService = new QuestionaryService(mockQuestionaryRepo.Object);

            questionaryService.UpdateQuestionaries(questionaryDto, EnumHelper.PermissionsUser.Admin);

            mockQuestionaryRepo.Verify(x => x.Update(_mapper.Map<Questionary>(questionaryDto)), Times.Once);
        }

        [Fact]
        public void CanAAdminUpdateAQuestionary_IReceiveTheUpdatedQuestionary()
        {
            Questionary questionary = new Questionary
            {
                Id = 1,
                Description = "Eerste questionaryDto",
                Questions = new List<Question>()
            };

            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionaryRepo = factory.Create<IQuestionaryRepo>();

            questionary.Description = "Eerste questionaryDto aangepast";

            mockQuestionaryRepo.Setup(x => x.Update(questionary)).Returns(questionary);

            QuestionaryService questionaryService = new QuestionaryService(mockQuestionaryRepo.Object);

            var response = questionaryService.UpdateQuestionaries(_mapper.Map<QuestionaryDTO>(questionary), EnumHelper.PermissionsUser.Admin);

            Assert.Equal(questionary.Id, response.Id);
            Assert.Equal(questionary.Description, response.Description);
        }

        [Fact]
        public void CanAOtherUserThanAdminUpdateAQuestionary_TheUpdateMethodMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionaryRepo = factory.Create<IQuestionaryRepo>();

            QuestionaryDTO questionaryDto = new QuestionaryDTO { Id = 1, Description = "Eerste questionaryDto" };
            QuestionaryService questionaryService = new QuestionaryService(mockQuestionaryRepo.Object);

            questionaryService.UpdateQuestionaries(questionaryDto, EnumHelper.PermissionsUser.GDPR);
            questionaryService.UpdateQuestionaries(questionaryDto, EnumHelper.PermissionsUser.Read);
            questionaryService.UpdateQuestionaries(questionaryDto, EnumHelper.PermissionsUser.Write);
            questionaryService.UpdateQuestionaries(questionaryDto, EnumHelper.PermissionsUser.Owner);

            mockQuestionaryRepo.Verify(x => x.Update(_mapper.Map<Questionary>(questionaryDto)), Times.Never);
        }
    }
}