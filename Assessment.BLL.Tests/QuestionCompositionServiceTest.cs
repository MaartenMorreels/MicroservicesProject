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
    public class QuestionCompositionServiceTest
    {
        private IMapper _mapper;
        private Mock<IQuestionCompositionREPO> _mockQuestionCompositionRepo;
        private QuestionCompositionDTO _questionCompositionDto;
        private Question _question;
        private Question _question2;
        private Questionary _questionary;
        private List<QuestionComposition> _questionCompositions;
        private int _questionId = 20;
        private int _questionId2 = 21;
        private string _questionPhrase = "Eerste vraag";

        private int _questionCompositionId1 = 1;
        private int _questionCompositionId2 = 2;
        private int _questionCompositionId3 = 3;

        private int _questionaryId = 1;
        private string _questionaryPhrase = "Eerste Questionary";

        private int _applicationLanguageId1 = 1;
        private int _applicationLanguageId2 = 2;
        private int _applicationBackEndId1 = 2;
        private int _applicationBackEndId2 = 2;
        private int _applicationFrontEndId1 = 2;
        private int _applicationFrontEndId2 = 2;

        private int _updateApplicationLanguageId = 6;

        public QuestionCompositionServiceTest()
        {
            MapperProfile map = new MapperProfile();
            _mapper = map.Mapper;
        }

        public void Init()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            _mockQuestionCompositionRepo = factory.Create<IQuestionCompositionREPO>();
            _questionCompositionDto = new QuestionCompositionDTO { Id = _questionCompositionId1, ApplicationLanguageId = _applicationLanguageId1, ApplicationBackEndId = _applicationBackEndId1, ApplicationFrontEndId = _applicationFrontEndId1 };

            _question = new Question
            {
                Id = _questionId,
                QuestionPhrase = _questionPhrase,
                QuestionComposition = new List<QuestionComposition>
                {
                    new QuestionComposition { Id = _questionCompositionId1, ApplicationLanguageId = _applicationLanguageId1, ApplicationBackEndId = _applicationBackEndId1, ApplicationFrontEndId = _applicationFrontEndId1 },
                    new QuestionComposition { Id = _questionCompositionId2, ApplicationLanguageId = _applicationLanguageId2, ApplicationBackEndId = _applicationBackEndId2, ApplicationFrontEndId = _applicationFrontEndId2 }
                }
            };
            _question2 = new Question
            {
                Id = _questionId2,
                QuestionPhrase = _questionPhrase,
                QuestionComposition = new List<QuestionComposition>
                {
                    new QuestionComposition { Id = _questionCompositionId3, ApplicationLanguageId = _applicationLanguageId1, ApplicationBackEndId = _applicationBackEndId1, ApplicationFrontEndId = _applicationFrontEndId1 }
                }
            };

           _questionary  = new Questionary
           {
               Id = _questionaryId,
               Description = _questionaryPhrase,
               Questions = new List<Question>
               {
                   _question,
                   _question2
               }
           };

            _questionCompositions = new List<QuestionComposition>
            {
                new QuestionComposition { Id = _questionCompositionId1, ApplicationLanguageId = _applicationLanguageId1, ApplicationBackEndId = _applicationBackEndId1, ApplicationFrontEndId = _applicationFrontEndId1 },
                new QuestionComposition { Id = _questionCompositionId2, ApplicationLanguageId = _applicationLanguageId2, ApplicationBackEndId = _applicationBackEndId2, ApplicationFrontEndId = _applicationFrontEndId2 },
                new QuestionComposition { Id = _questionCompositionId3, ApplicationLanguageId = _applicationLanguageId1, ApplicationBackEndId = _applicationBackEndId1, ApplicationFrontEndId = _applicationFrontEndId1 }

            };
        }

        [Fact]
        public void CanAAdminCreateANewQuestionComposition_TheAddMethodMustBeCalled()
        {
            Init();
            QuestionCompositionService questionCompositionService = new QuestionCompositionService(_mockQuestionCompositionRepo.Object);

            questionCompositionService.AddQuestionComposition(_questionCompositionDto, EnumHelper.PermissionsUser.Admin);
            _mockQuestionCompositionRepo.Verify(x => x.Add(_mapper.Map<QuestionComposition>(_questionCompositionDto)), Times.Once);
        }

        [Fact]
        public void CanAnOtherUserThanAdminCreateANewQuestionComposition_TheAddMethodMustNotBeCalled()
        {
            Init();
            QuestionCompositionService questionCompositionService = new QuestionCompositionService(_mockQuestionCompositionRepo.Object);

            questionCompositionService.AddQuestionComposition(_questionCompositionDto, EnumHelper.PermissionsUser.GDPR);
            questionCompositionService.AddQuestionComposition(_questionCompositionDto, EnumHelper.PermissionsUser.Write);
            questionCompositionService.AddQuestionComposition(_questionCompositionDto, EnumHelper.PermissionsUser.Read);
            questionCompositionService.AddQuestionComposition(_questionCompositionDto, EnumHelper.PermissionsUser.Owner);

            _mockQuestionCompositionRepo.Verify(x => x.Add(_mapper.Map<QuestionComposition>(_questionCompositionDto)), Times.Never);
        }

        [Fact]
        public void CanAAdminCreateANewQuestionComposition_IRecieveAQuestionComposition()
        {
            Init();
            QuestionCompositionService questionCompositionService = new QuestionCompositionService(_mockQuestionCompositionRepo.Object);

            _mockQuestionCompositionRepo.Setup(x => x.Add(_mapper.Map<QuestionComposition>(_questionCompositionDto))).Returns(_mapper.Map<QuestionComposition>(_questionCompositionDto));

            var response = questionCompositionService.AddQuestionComposition(_questionCompositionDto, EnumHelper.PermissionsUser.Admin);
            Assert.Equal(_questionCompositionDto.Id, response.Id);
            Assert.Equal(_questionCompositionDto.ApplicationBackEndId, response.ApplicationBackEndId);
            Assert.Equal(_questionCompositionDto.ApplicationLanguageId, response.ApplicationLanguageId);
            Assert.Equal(_questionCompositionDto.ApplicationFrontEndId, response.ApplicationFrontEndId);
        }

        [Fact]
        public void CanAnAdminGetAnQuestionCompostionById_TheGetByIdMethodMustBeCalled()
        {
            Init();
            QuestionCompositionService questionCompositionService = new QuestionCompositionService(_mockQuestionCompositionRepo.Object);

            questionCompositionService.GetQuestionCompositionById(_questionCompositionDto.Id, EnumHelper.PermissionsUser.Admin);
            _mockQuestionCompositionRepo.Verify(x => x.GetById(_questionCompositionDto.Id), Times.Once);
        }

        [Fact]
        public void CanAnOthderUserThqnAdminGetAnQuestionCompostionById_TheGetByIdMethodMustNotBeCalled()
        {
            Init();
            QuestionCompositionService questionCompositionService = new QuestionCompositionService(_mockQuestionCompositionRepo.Object);

            questionCompositionService.GetQuestionCompositionById(_questionCompositionDto.Id, EnumHelper.PermissionsUser.GDPR);
            questionCompositionService.GetQuestionCompositionById(_questionCompositionDto.Id, EnumHelper.PermissionsUser.Read);
            questionCompositionService.GetQuestionCompositionById(_questionCompositionDto.Id, EnumHelper.PermissionsUser.Write);
            questionCompositionService.GetQuestionCompositionById(_questionCompositionDto.Id, EnumHelper.PermissionsUser.Owner);

            _mockQuestionCompositionRepo.Verify(x => x.GetById(_questionCompositionDto.Id), Times.Never);
        }


        [Fact]
        public void CanAnAdminGetAnQuestionCompostionById_IRecieveAnQuestionComposition()
        {
            Init();
            QuestionCompositionService questionCompositionService = new QuestionCompositionService(_mockQuestionCompositionRepo.Object);

            _mockQuestionCompositionRepo.Setup(x => x.GetById(_questionCompositionDto.Id)).Returns(_mapper.Map<QuestionComposition>(_questionCompositionDto));

            var response = questionCompositionService.GetQuestionCompositionById(_questionCompositionDto.Id, EnumHelper.PermissionsUser.Admin);

            Assert.Equal(_questionCompositionDto.Id, response.Id);
            Assert.Equal(_questionCompositionDto.ApplicationBackEndId, response.ApplicationBackEndId);
            Assert.Equal(_questionCompositionDto.ApplicationLanguageId, response.ApplicationLanguageId);
            Assert.Equal(_questionCompositionDto.ApplicationFrontEndId, response.ApplicationFrontEndId);
        }

        [Fact]
        public void CanAnAdminGetAnQuestionCompostionByQuestionId_TheGetByIdMethodMustBeCalled()
        {
            Init();

            QuestionCompositionService questionCompositionService = new QuestionCompositionService(_mockQuestionCompositionRepo.Object);

            questionCompositionService.GetAllQuestionCompositionsByQuestionId(_question.Id, EnumHelper.PermissionsUser.Admin);
            _mockQuestionCompositionRepo.Verify(x => x.GetAllQuestionCompositionByQuestionId(_question.Id), Times.Once);
        }

        [Fact]
        public void CanAnOtherUserThanAdminGetAnQuestionCompostionByQuestionId_TheGetByIdMethodMustNotBeCalled()
        {
            Init();

            QuestionCompositionService questionCompositionService = new QuestionCompositionService(_mockQuestionCompositionRepo.Object);

            questionCompositionService.GetAllQuestionCompositionsByQuestionId(_question.Id, EnumHelper.PermissionsUser.GDPR);
            questionCompositionService.GetAllQuestionCompositionsByQuestionId(_question.Id, EnumHelper.PermissionsUser.Read);
            questionCompositionService.GetAllQuestionCompositionsByQuestionId(_question.Id, EnumHelper.PermissionsUser.Write);
            questionCompositionService.GetAllQuestionCompositionsByQuestionId(_question.Id, EnumHelper.PermissionsUser.Owner);
            _mockQuestionCompositionRepo.Verify(x => x.GetAllQuestionCompositionByQuestionId(_question.Id), Times.Never);
        }


        [Fact]
        public void CanAnAdminGetAnQuestionCompostionByQuestionId_IRecieveQuestionCompositions()
        {
            Init();
            QuestionCompositionService questionCompositionService = new QuestionCompositionService(_mockQuestionCompositionRepo.Object);

            _mockQuestionCompositionRepo.Setup(x => x.GetAllQuestionCompositionByQuestionId(_question.Id)).Returns(_question.QuestionComposition);

            var response = questionCompositionService.GetAllQuestionCompositionsByQuestionId(_question.Id, EnumHelper.PermissionsUser.Admin);

            Assert.Equal(_question.QuestionComposition.FirstOrDefault().Id, response.FirstOrDefault().Id);
            Assert.Equal(_question.QuestionComposition.FirstOrDefault().ApplicationBackEndId, response.FirstOrDefault().ApplicationBackEndId);
            Assert.Equal(_question.QuestionComposition.FirstOrDefault().ApplicationFrontEndId, response.FirstOrDefault().ApplicationFrontEndId);
            Assert.Equal(_question.QuestionComposition.FirstOrDefault().ApplicationLanguageId, response.FirstOrDefault().ApplicationLanguageId);

            Assert.Equal(_question.QuestionComposition.LastOrDefault().Id, response.LastOrDefault().Id);
            Assert.Equal(_question.QuestionComposition.LastOrDefault().ApplicationBackEndId, response.LastOrDefault().ApplicationBackEndId);
            Assert.Equal(_question.QuestionComposition.LastOrDefault().ApplicationFrontEndId, response.LastOrDefault().ApplicationFrontEndId);
            Assert.Equal(_question.QuestionComposition.LastOrDefault().ApplicationLanguageId, response.LastOrDefault().ApplicationLanguageId);
        }

        [Fact]
        public void CanAnAdminGetAllQuestionCompostion_TheGetAllQuestionCompositionMethodMustBeCalled()
        {
            Init();
            QuestionCompositionService questionCompositionService = new QuestionCompositionService(_mockQuestionCompositionRepo.Object);

            questionCompositionService.GetAllQuestionCompositions(EnumHelper.PermissionsUser.Admin);
            _mockQuestionCompositionRepo.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public void CanAnOtherUserThanAdminGetAllQuestionCompostion_TheGetAllQuestionCompositionMethodMustNotBeCalled()
        {
            Init();
            QuestionCompositionService questionCompositionService = new QuestionCompositionService(_mockQuestionCompositionRepo.Object);

            questionCompositionService.GetAllQuestionCompositions(EnumHelper.PermissionsUser.GDPR);
            questionCompositionService.GetAllQuestionCompositions(EnumHelper.PermissionsUser.Read);
            questionCompositionService.GetAllQuestionCompositions(EnumHelper.PermissionsUser.Write);
            questionCompositionService.GetAllQuestionCompositions(EnumHelper.PermissionsUser.Owner);
            _mockQuestionCompositionRepo.Verify(x => x.GetAll(), Times.Never);
        }

        [Fact]
        public void CanAnAdminGetAllQuestionCompostion_IRecieveAnListOfQuestionCompositions()
        {
            Init();
            QuestionCompositionService questionCompositionService = new QuestionCompositionService(_mockQuestionCompositionRepo.Object);

            _mockQuestionCompositionRepo.Setup(x => x.GetAll()).Returns(_questionCompositions.AsQueryable());
            
            var response = questionCompositionService.GetAllQuestionCompositions(EnumHelper.PermissionsUser.Admin);

            Assert.Equal(_questionCompositions.Count, response.Count);
            Assert.Equal(_questionCompositions.FirstOrDefault().Id, response.FirstOrDefault().Id);
            Assert.Equal(_questionCompositions.FirstOrDefault().ApplicationBackEndId, response.FirstOrDefault().ApplicationBackEndId);
            Assert.Equal(_questionCompositions.FirstOrDefault().ApplicationFrontEndId, response.FirstOrDefault().ApplicationFrontEndId);
            Assert.Equal(_questionCompositions.FirstOrDefault().ApplicationLanguageId, response.FirstOrDefault().ApplicationLanguageId);

            Assert.Equal(_questionCompositions.LastOrDefault().Id, response.LastOrDefault().Id);
            Assert.Equal(_questionCompositions.LastOrDefault().ApplicationBackEndId, response.LastOrDefault().ApplicationBackEndId);
            Assert.Equal(_questionCompositions.LastOrDefault().ApplicationFrontEndId, response.LastOrDefault().ApplicationFrontEndId);
            Assert.Equal(_questionCompositions.LastOrDefault().ApplicationLanguageId, response.LastOrDefault().ApplicationLanguageId);
        }

        [Fact]
        public void CanAnAdminUpdateAnQuestionCompostion_TheUpdateQuestionCompositionMethodMustBeCalled()
        {
            Init();
            QuestionCompositionService questionCompositionService = new QuestionCompositionService(_mockQuestionCompositionRepo.Object);
            _mockQuestionCompositionRepo.Setup(x => x.Update(_mapper.Map<QuestionComposition>(_questionCompositionDto))).Returns(_mapper.Map<QuestionComposition>(_questionCompositionDto));

            _questionCompositionDto.ApplicationLanguageId = _updateApplicationLanguageId;

            questionCompositionService.UpdateQuestionComposition(_questionCompositionDto,EnumHelper.PermissionsUser.Admin);
            _mockQuestionCompositionRepo.Verify(x => x.Update(_mapper.Map<QuestionComposition>(_questionCompositionDto)), Times.Once);
        }
        [Fact]
        public void CanAnOtherUserThanAdminUpdateAnQuestionCompostion_TheUpdateQuestionCompositionMethodMustNotBeCalled()
        {
            Init();
            QuestionCompositionService questionCompositionService = new QuestionCompositionService(_mockQuestionCompositionRepo.Object);
            _mockQuestionCompositionRepo.Setup(x => x.Update(_mapper.Map<QuestionComposition>(_questionCompositionDto))).Returns(_mapper.Map<QuestionComposition>(_questionCompositionDto));

            _questionCompositionDto.ApplicationLanguageId = _updateApplicationLanguageId;

            questionCompositionService.UpdateQuestionComposition(_questionCompositionDto, EnumHelper.PermissionsUser.GDPR);
            questionCompositionService.UpdateQuestionComposition(_questionCompositionDto, EnumHelper.PermissionsUser.Read);
            questionCompositionService.UpdateQuestionComposition(_questionCompositionDto, EnumHelper.PermissionsUser.Write);
            questionCompositionService.UpdateQuestionComposition(_questionCompositionDto, EnumHelper.PermissionsUser.Owner);
            _mockQuestionCompositionRepo.Verify(x => x.Update(_mapper.Map<QuestionComposition>(_questionCompositionDto)), Times.Never);
        }

        [Fact]
        public void CanAnAdminUpdateAnQuestionCompostion_IRecieveTheUpdatedQuestionComposition()
        {
            Init();
            QuestionCompositionService questionCompositionService = new QuestionCompositionService(_mockQuestionCompositionRepo.Object);
           
            _questionCompositionDto.ApplicationLanguageId = _updateApplicationLanguageId;
            _mockQuestionCompositionRepo.Setup(x => x.Update(_mapper.Map<QuestionComposition>(_questionCompositionDto))).Returns(_mapper.Map<QuestionComposition>(_questionCompositionDto));


            var response  = questionCompositionService.UpdateQuestionComposition(_questionCompositionDto, EnumHelper.PermissionsUser.Admin);
            Assert.Equal(response.ApplicationLanguageId , _updateApplicationLanguageId);
        }
    }
}
