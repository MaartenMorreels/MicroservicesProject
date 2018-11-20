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
    public class QuestionApplicationLanguageServiceTest
    {
        private IMapper _mapper;

        public QuestionApplicationLanguageServiceTest()
        {
            MapperProfile map = new MapperProfile();
            _mapper = map.Mapper;
        }

        [Fact]
        public void CanAnAdminCreateANewQuestionApplicationLanguage_TheAddMethodMustBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionApplicationLanguageRepo = factory.Create<IQuestionApplicationLanguageREPO>();

            QuestionApplicationLanguageDTO questionApplicationLanguageDto = new QuestionApplicationLanguageDTO { Id = 1, ApplicationLanguageId = 1, QuestionCompositionId = 1 };

            QuestionApplicationLanguageService questionApplicationLanguageService =new QuestionApplicationLanguageService(mockQuestionApplicationLanguageRepo.Object);

            questionApplicationLanguageService.AddQuestionApplicationLanguage(questionApplicationLanguageDto, EnumHelper.PermissionsUser.Admin);

            mockQuestionApplicationLanguageRepo.Verify(x => x.Add(_mapper.Map<QuestionApplicationLanguage>(questionApplicationLanguageDto)), Times.Once);
        }

        [Fact]
        public void CanAnAdminCreateANewQuestionApplicationLanguage_IReceiveTheAddedQuestionApplicationLanguage()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionApplicationLanguageRepo = factory.Create<IQuestionApplicationLanguageREPO>();

            QuestionApplicationLanguage questionApplicationLanguage = new QuestionApplicationLanguage
            {
                Id = 1,
                ApplicationLanguageId = 1,
                QuestionCompositionId = 1
            };
            mockQuestionApplicationLanguageRepo.Setup(x => x.Add(questionApplicationLanguage)).Returns(questionApplicationLanguage);

            QuestionApplicationLanguageService questionApplicationLanguageService = new QuestionApplicationLanguageService(mockQuestionApplicationLanguageRepo.Object);

            var response = questionApplicationLanguageService.AddQuestionApplicationLanguage(_mapper.Map<QuestionApplicationLanguageDTO>(questionApplicationLanguage), EnumHelper.PermissionsUser.Admin);

            Assert.Equal(questionApplicationLanguage.Id, response.Id);
            Assert.Equal(questionApplicationLanguage.ApplicationLanguageId, response.ApplicationLanguageId);
            Assert.Equal(questionApplicationLanguage.QuestionCompositionId, response.QuestionCompositionId);
        }

        [Fact]
        public void CanAnOtherUserThanAdminCreateAQuestionApplicationLanguage_TheAddMethodMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionApplicationLanguageRepo = factory.Create<IQuestionApplicationLanguageREPO>();

            QuestionApplicationLanguageDTO questionApplicationLanguageDto = new QuestionApplicationLanguageDTO { Id = 1, ApplicationLanguageId = 1, QuestionCompositionId = 1 };

            QuestionApplicationLanguageService questionApplicationLanguageService = new QuestionApplicationLanguageService(mockQuestionApplicationLanguageRepo.Object);

            questionApplicationLanguageService.AddQuestionApplicationLanguage(questionApplicationLanguageDto, EnumHelper.PermissionsUser.GDPR);
            questionApplicationLanguageService.AddQuestionApplicationLanguage(questionApplicationLanguageDto, EnumHelper.PermissionsUser.Read);
            questionApplicationLanguageService.AddQuestionApplicationLanguage(questionApplicationLanguageDto, EnumHelper.PermissionsUser.Write);
            questionApplicationLanguageService.AddQuestionApplicationLanguage(questionApplicationLanguageDto, EnumHelper.PermissionsUser.Owner);

            mockQuestionApplicationLanguageRepo.Verify(x => x.Add(_mapper.Map<QuestionApplicationLanguage>(questionApplicationLanguageDto)), Times.Never);
        }

        [Fact]
        public void CanAnAdminCreateANewQuestionApplicationLanguageWithoutApplicationLanguageId_TheAddMethodMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionApplicationLanguageRepo = factory.Create<IQuestionApplicationLanguageREPO>();

            QuestionApplicationLanguageDTO questionApplicationLanguageDto = new QuestionApplicationLanguageDTO { Id = 1, QuestionCompositionId = 1 };

            QuestionApplicationLanguageService questionApplicationLanguageService = new QuestionApplicationLanguageService(mockQuestionApplicationLanguageRepo.Object);

            questionApplicationLanguageService.AddQuestionApplicationLanguage(questionApplicationLanguageDto, EnumHelper.PermissionsUser.Admin);

            mockQuestionApplicationLanguageRepo.Verify(x => x.Add(_mapper.Map<QuestionApplicationLanguage>(questionApplicationLanguageDto)), Times.Never);
        }

        [Fact]
        public void CanAnAdminCreateANewQuestionApplicationLanguageWithoutQuestionCompositionId_TheAddMethodMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionApplicationLanguageRepo = factory.Create<IQuestionApplicationLanguageREPO>();

            QuestionApplicationLanguageDTO questionApplicationLanguageDto = new QuestionApplicationLanguageDTO { Id = 1, ApplicationLanguageId = 1 };

            QuestionApplicationLanguageService questionApplicationLanguageService = new QuestionApplicationLanguageService(mockQuestionApplicationLanguageRepo.Object);

            questionApplicationLanguageService.AddQuestionApplicationLanguage(questionApplicationLanguageDto, EnumHelper.PermissionsUser.Admin);

            mockQuestionApplicationLanguageRepo.Verify(x => x.Add(_mapper.Map<QuestionApplicationLanguage>(questionApplicationLanguageDto)), Times.Never);
        }

        [Fact]
        public void CanAnAdminCreateANewQuestionApplicationLanguageWithoutApplicationId_IWouldLikeToHaveANullObject()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionApplicationLanguageRepo = factory.Create<IQuestionApplicationLanguageREPO>();

            QuestionApplicationLanguageDTO questionApplicationLanguageDto = new QuestionApplicationLanguageDTO { Id = 1, QuestionCompositionId = 1 };

            QuestionApplicationLanguageService questionApplicationLanguageService = new QuestionApplicationLanguageService(mockQuestionApplicationLanguageRepo.Object);

            var responseQuestionApplicationLanguage = questionApplicationLanguageService.AddQuestionApplicationLanguage(questionApplicationLanguageDto, EnumHelper.PermissionsUser.Admin);

            Assert.Null(responseQuestionApplicationLanguage);
        }

        [Fact]
        public void CanAnAdminCreateANewQuestionApplicationLanguageWithoutQuestionCompositionId_IWouldLikeToHaveANullObject()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionApplicationLanguageRepo = factory.Create<IQuestionApplicationLanguageREPO>();

            QuestionApplicationLanguageDTO questionApplicationLanguageDto = new QuestionApplicationLanguageDTO { Id = 1, ApplicationLanguageId = 1 };
            QuestionApplicationLanguageService questionApplicationLanguageService = new QuestionApplicationLanguageService(mockQuestionApplicationLanguageRepo.Object);

            var responseQuestionApplicationLanguage = questionApplicationLanguageService.AddQuestionApplicationLanguage(questionApplicationLanguageDto, EnumHelper.PermissionsUser.Admin);

            Assert.Null(responseQuestionApplicationLanguage);
        }

        [Fact]
        public void CanAnAdminGetAQuestionApplicationLanguageById_TheGetByIdMethodMustBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionApplicationLanguageRepo = factory.Create<IQuestionApplicationLanguageREPO>();

            QuestionApplicationLanguageService questionApplicationLanguageService = new QuestionApplicationLanguageService(mockQuestionApplicationLanguageRepo.Object);

            questionApplicationLanguageService.GetQuestionApplicationLanguageById(1, EnumHelper.PermissionsUser.Admin);

            mockQuestionApplicationLanguageRepo.Verify(x => x.GetById(1), Times.Once);
        }

        [Fact]
        public void CanAnAdminGetAQuestionApplicationLanguageById_IReceiveAQuestionApplicationLanguage()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionApplicationLanguageRepo = factory.Create<IQuestionApplicationLanguageREPO>();

            QuestionApplicationLanguage questionApplicationLanguage = new QuestionApplicationLanguage
            {
                Id = 1,
                ApplicationLanguageId = 1,
                QuestionCompositionId = 1
            };

            mockQuestionApplicationLanguageRepo.Setup(x => x.GetById(questionApplicationLanguage.Id)).Returns(questionApplicationLanguage);

            QuestionApplicationLanguageService questionApplicationLanguageService = new QuestionApplicationLanguageService(mockQuestionApplicationLanguageRepo.Object);

            var response = questionApplicationLanguageService.GetQuestionApplicationLanguageById(1, EnumHelper.PermissionsUser.Admin);

            Assert.Equal(questionApplicationLanguage.Id, response.Id);
            Assert.Equal(questionApplicationLanguage.ApplicationLanguageId, response.ApplicationLanguageId);
            Assert.Equal(questionApplicationLanguage.QuestionCompositionId, response.QuestionCompositionId);
        }

        [Fact]
        public void CanAnOtherUserThanAdminGetAQuestionApplicationLanguageById_TheGetByIdMethodMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionApplicationLanguageRepo = factory.Create<IQuestionApplicationLanguageREPO>();

            QuestionApplicationLanguageService questionApplicationLanguageService = new QuestionApplicationLanguageService(mockQuestionApplicationLanguageRepo.Object);

            questionApplicationLanguageService.GetQuestionApplicationLanguageById(1, EnumHelper.PermissionsUser.GDPR);
            questionApplicationLanguageService.GetQuestionApplicationLanguageById(1, EnumHelper.PermissionsUser.Read);
            questionApplicationLanguageService.GetQuestionApplicationLanguageById(1, EnumHelper.PermissionsUser.Write);
            questionApplicationLanguageService.GetQuestionApplicationLanguageById(1, EnumHelper.PermissionsUser.Owner);

            mockQuestionApplicationLanguageRepo.Verify(x => x.GetById(1), Times.Never);
        }

        [Fact]
        public void CanAnAdminGetAllQuestionApplicationLanguages_TheGetAllQuestionApplicationLanguagesMethodMustBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionApplicationLanguageRepo = factory.Create<IQuestionApplicationLanguageREPO>();

            QuestionApplicationLanguageService questionApplicationLanguageService = new QuestionApplicationLanguageService(mockQuestionApplicationLanguageRepo.Object);

            questionApplicationLanguageService.GetAllQuestionApplicationLanguages(EnumHelper.PermissionsUser.Admin);

            mockQuestionApplicationLanguageRepo.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public void CanAnAdminGetAllQuestionApplicationLanguages_IReceiveAListOfQuestionApplicationLanguages()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionApplicationLanguageRepo = factory.Create<IQuestionApplicationLanguageREPO>();

            var listOfQuestionApplicationLanguages = new List<QuestionApplicationLanguage>
            {
                new QuestionApplicationLanguage
                {
                    Id=1,
                    ApplicationLanguageId=1,
                    QuestionCompositionId=1
                },
                new QuestionApplicationLanguage
                {
                    Id=2,
                    ApplicationLanguageId=2,
                    QuestionCompositionId=2
                },

            };

            mockQuestionApplicationLanguageRepo.Setup(x => x.GetAll()).Returns(listOfQuestionApplicationLanguages.AsQueryable());

            QuestionApplicationLanguageService questionApplicationLanguageService = new QuestionApplicationLanguageService(mockQuestionApplicationLanguageRepo.Object);


            var response = questionApplicationLanguageService.GetAllQuestionApplicationLanguages(EnumHelper.PermissionsUser.Admin);

            Assert.Equal(listOfQuestionApplicationLanguages.FirstOrDefault().Id, response.FirstOrDefault().Id);
            Assert.Equal(listOfQuestionApplicationLanguages.FirstOrDefault().ApplicationLanguageId, response.FirstOrDefault().ApplicationLanguageId);
            Assert.Equal(listOfQuestionApplicationLanguages.FirstOrDefault().QuestionCompositionId, response.FirstOrDefault().QuestionCompositionId);
        }

        [Fact]
        public void CanAnOtherUserThanAdminGetAllQuestionApplicationLanguages_TheGetAllQuestionApplicationLanguagesMethodMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionApplicationLanguageRepo = factory.Create<IQuestionApplicationLanguageREPO>();

            QuestionApplicationLanguageService questionApplicationLanguageService = new QuestionApplicationLanguageService(mockQuestionApplicationLanguageRepo.Object);


            questionApplicationLanguageService.GetAllQuestionApplicationLanguages(EnumHelper.PermissionsUser.GDPR);
            questionApplicationLanguageService.GetAllQuestionApplicationLanguages(EnumHelper.PermissionsUser.Read);
            questionApplicationLanguageService.GetAllQuestionApplicationLanguages(EnumHelper.PermissionsUser.Write);
            questionApplicationLanguageService.GetAllQuestionApplicationLanguages(EnumHelper.PermissionsUser.Owner);

            mockQuestionApplicationLanguageRepo.Verify(x => x.GetAll(), Times.Never);
        }

        [Fact]
        public void CanAnAdminUpdateAQuestionApplicationLanguage_TheUpdateMethodMustBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionApplicationLanguageRepo = factory.Create<IQuestionApplicationLanguageREPO>();

            QuestionApplicationLanguageDTO questionApplicationLanguageDto = new QuestionApplicationLanguageDTO { Id = 1, ApplicationLanguageId = 1, QuestionCompositionId = 1 };

            QuestionApplicationLanguageService questionApplicationLanguageService = new QuestionApplicationLanguageService(mockQuestionApplicationLanguageRepo.Object);


            questionApplicationLanguageService.UpdateQuestionApplicationLanguage(questionApplicationLanguageDto, EnumHelper.PermissionsUser.Admin);

            mockQuestionApplicationLanguageRepo.Verify(x => x.Update(_mapper.Map<QuestionApplicationLanguage>(questionApplicationLanguageDto)), Times.Once);
        }

        [Fact]
        public void CanAnAdminUpdateAQuestionApplicationLanguage_IReceiveTheUpdatedQuestionApplicationLanguage()
        {
            QuestionApplicationLanguage questionApplicationLanguage = new QuestionApplicationLanguage
            {
                Id = 1,
                ApplicationLanguageId = 1,
                QuestionCompositionId = 1
            };

            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionApplicationLanguageRepo = factory.Create<IQuestionApplicationLanguageREPO>();

            questionApplicationLanguage.ApplicationLanguageId = 2;

            mockQuestionApplicationLanguageRepo.Setup(x => x.Update(questionApplicationLanguage)).Returns(questionApplicationLanguage);

            QuestionApplicationLanguageService questionApplicationLanguageService = new QuestionApplicationLanguageService(mockQuestionApplicationLanguageRepo.Object);

            var response = questionApplicationLanguageService.UpdateQuestionApplicationLanguage(_mapper.Map<QuestionApplicationLanguageDTO>(questionApplicationLanguage), EnumHelper.PermissionsUser.Admin);

            Assert.Equal(questionApplicationLanguage.Id, response.Id);
            Assert.Equal(questionApplicationLanguage.ApplicationLanguageId, response.ApplicationLanguageId);
            Assert.Equal(questionApplicationLanguage.QuestionCompositionId, response.QuestionCompositionId);

        }

        [Fact]
        public void CanAnOtherUserThanAdminUpdateQuestionApplicationLanguage_TheUpdateMethodMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockQuestionApplicationLanguageRepo = factory.Create<IQuestionApplicationLanguageREPO>();

            QuestionApplicationLanguageDTO questionApplicationLanguageDto = new QuestionApplicationLanguageDTO { Id = 1, ApplicationLanguageId = 1, QuestionCompositionId = 1 };
            QuestionApplicationLanguageService questionApplicationLanguageService = new QuestionApplicationLanguageService(mockQuestionApplicationLanguageRepo.Object);


            questionApplicationLanguageService.UpdateQuestionApplicationLanguage(questionApplicationLanguageDto, EnumHelper.PermissionsUser.GDPR);
            questionApplicationLanguageService.UpdateQuestionApplicationLanguage(questionApplicationLanguageDto, EnumHelper.PermissionsUser.Read);
            questionApplicationLanguageService.UpdateQuestionApplicationLanguage(questionApplicationLanguageDto, EnumHelper.PermissionsUser.Write);
            questionApplicationLanguageService.UpdateQuestionApplicationLanguage(questionApplicationLanguageDto, EnumHelper.PermissionsUser.Owner);

            mockQuestionApplicationLanguageRepo.Verify(x => x.Update(_mapper.Map<QuestionApplicationLanguage>(questionApplicationLanguageDto)), Times.Never);
        }
    }
}
