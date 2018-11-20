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
    public class QuestionApplicationDomainBackEndServiceTest
    {
        private IMapper _mapper;

        public QuestionApplicationDomainBackEndServiceTest()
        {
            MapperProfile map = new MapperProfile();
            _mapper = map.Mapper;
        }

        [Fact]
        public void CanAAdminCreateANewQuestionApplicationDomainBackEnd_TheAddMethodMustBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockRepo = factory.Create<IQuestionApplicationDomainBackEndRepo>();

            QuestionApplicationDomainBackEndDTO Dto = new QuestionApplicationDomainBackEndDTO
            {
                Id = 1,
                ApplicationDomainBackEndId = 1,
                QuestionCompositionId = 1
            };

            QuestionApplicationDomainBackEndService service = new QuestionApplicationDomainBackEndService(mockRepo.Object);

            service.AddQuestionApplicationDomainBackEnd(Dto, EnumHelper.PermissionsUser.Admin);

            mockRepo.Verify(x => x.Add(_mapper.Map<QuestionApplicationDomainBackEnd>(Dto)), Times.Once);
        }

        [Fact]
        public void CanAAdminCreateANewQuestionApplicationDomainBackEnd_IReceiveAQuestionApplicationDomainBackEnd()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockRepo = factory.Create<IQuestionApplicationDomainBackEndRepo>();

            QuestionApplicationDomainBackEnd ent = new QuestionApplicationDomainBackEnd
            {
                Id = 1,
                QuestionCompositionId = 1,
                ApplicationDomainBackEndId = 1
            };

            mockRepo.Setup(x => x.Add(ent)).Returns(ent);

            QuestionApplicationDomainBackEndService service = new QuestionApplicationDomainBackEndService(mockRepo.Object);

            var response = service.AddQuestionApplicationDomainBackEnd(_mapper.Map<QuestionApplicationDomainBackEndDTO>(ent), EnumHelper.PermissionsUser.Admin);

            Assert.Equal(ent.Id, response.Id);
            Assert.Equal(ent.QuestionCompositionId, response.QuestionCompositionId);
            Assert.Equal(ent.ApplicationDomainBackEndId, response.ApplicationDomainBackEndId);
        }

        [Fact]
        public void CanAnotherUserThanAdminCreateANewQuestionApplicationDomainBackEnd_TheAddMethodMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockRepo = factory.Create<IQuestionApplicationDomainBackEndRepo>();

            QuestionApplicationDomainBackEndDTO Dto = new QuestionApplicationDomainBackEndDTO
            {
                Id = 1,
                ApplicationDomainBackEndId = 1,
                QuestionCompositionId = 1
            };

            QuestionApplicationDomainBackEndService service = new QuestionApplicationDomainBackEndService(mockRepo.Object);

            service.AddQuestionApplicationDomainBackEnd(Dto, EnumHelper.PermissionsUser.GDPR);
            service.AddQuestionApplicationDomainBackEnd(Dto, EnumHelper.PermissionsUser.Read);
            service.AddQuestionApplicationDomainBackEnd(Dto, EnumHelper.PermissionsUser.Write);
            service.AddQuestionApplicationDomainBackEnd(Dto, EnumHelper.PermissionsUser.Owner);

            mockRepo.Verify(x => x.Add(_mapper.Map<QuestionApplicationDomainBackEnd>(Dto)), Times.Never);
        }

        [Fact]
        public void CanAnAdminCreateANewQuestionApplicationDomainBackEndWithoutApplicationDomainBackEndId_TheAddMethodMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockRepo = factory.Create<IQuestionApplicationDomainBackEndRepo>();

            QuestionApplicationDomainBackEndDTO Dto = new QuestionApplicationDomainBackEndDTO
            {
                Id = 1,
                QuestionCompositionId = 1
            };

            QuestionApplicationDomainBackEndService service = new QuestionApplicationDomainBackEndService(mockRepo.Object);

            service.AddQuestionApplicationDomainBackEnd(Dto, EnumHelper.PermissionsUser.Admin);

            mockRepo.Verify(x => x.Add(_mapper.Map<QuestionApplicationDomainBackEnd>(Dto)), Times.Never);
        }

        [Fact]
        public void CanAnAdminCreateANewQuestionApplicationDomainBackEndWithoutQuestionCompositionId_TheAddMethodMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockRepo = factory.Create<IQuestionApplicationDomainBackEndRepo>();

            QuestionApplicationDomainBackEndDTO Dto = new QuestionApplicationDomainBackEndDTO
            {
                Id = 1,
                ApplicationDomainBackEndId = 1
            };

            QuestionApplicationDomainBackEndService service = new QuestionApplicationDomainBackEndService(mockRepo.Object);

            service.AddQuestionApplicationDomainBackEnd(Dto, EnumHelper.PermissionsUser.Admin);

            mockRepo.Verify(x => x.Add(_mapper.Map<QuestionApplicationDomainBackEnd>(Dto)), Times.Never);
        }

        [Fact]
        public void CanAnAdminCreateANewQuestionApplicationDomainBackEndWithoutApplicationDomainBackEndId_IReceiveANullObject()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockRepo = factory.Create<IQuestionApplicationDomainBackEndRepo>();

            QuestionApplicationDomainBackEnd ent = new QuestionApplicationDomainBackEnd
            {
                Id = 1,
                QuestionCompositionId = 1,
            };

            mockRepo.Setup(x => x.Add(ent)).Returns(ent);

            QuestionApplicationDomainBackEndService service = new QuestionApplicationDomainBackEndService(mockRepo.Object);


            var response = service.AddQuestionApplicationDomainBackEnd(_mapper.Map<QuestionApplicationDomainBackEndDTO>(ent), EnumHelper.PermissionsUser.Admin);

            Assert.Null(response);
        }

        [Fact]
        public void CanAnAdminCreateANewQuestionApplicationDomainBackEndWithoutQuestionCompositionId_IReceiveANullObject()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockRepo = factory.Create<IQuestionApplicationDomainBackEndRepo>();

            QuestionApplicationDomainBackEnd ent = new QuestionApplicationDomainBackEnd
            {
                Id = 1,
                ApplicationDomainBackEndId = 1
            };

            mockRepo.Setup(x => x.Add(ent)).Returns(ent);
            QuestionApplicationDomainBackEndService service = new QuestionApplicationDomainBackEndService(mockRepo.Object);


            var response = service.AddQuestionApplicationDomainBackEnd(_mapper.Map<QuestionApplicationDomainBackEndDTO>(ent), EnumHelper.PermissionsUser.Admin);

            Assert.Null(response);
        }

        [Fact]
        public void CanAnAdminGetAQuestionApplicationDomainBackEndById_TheGetMethodMustBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockRepo = factory.Create<IQuestionApplicationDomainBackEndRepo>();

            QuestionApplicationDomainBackEndDTO Dto = new QuestionApplicationDomainBackEndDTO
            {
                Id = 1,
                ApplicationDomainBackEndId = 1,
                QuestionCompositionId = 1
            };

            QuestionApplicationDomainBackEndService service = new QuestionApplicationDomainBackEndService(mockRepo.Object);


            service.GetQuestionApplicationDomainBackEndById(1, EnumHelper.PermissionsUser.Admin);

            mockRepo.Verify(x => x.GetById(1), Times.Once);
        }

        [Fact]
        public void CanAnAdminGetAQuestionApplicationDomainBackEndById_IReceiveTheQuestionAQuestionApplicationDomainBackEnd()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockRepo = factory.Create<IQuestionApplicationDomainBackEndRepo>();

            QuestionApplicationDomainBackEnd Ent = new QuestionApplicationDomainBackEnd
            {
                Id = 1,
                ApplicationDomainBackEndId = 1,
                QuestionCompositionId = 1
            };

            mockRepo.Setup(x => x.GetById(Ent.Id)).Returns(Ent);

            QuestionApplicationDomainBackEndService service = new QuestionApplicationDomainBackEndService(mockRepo.Object);

            var response = service.GetQuestionApplicationDomainBackEndById(1, EnumHelper.PermissionsUser.Admin);

            Assert.Equal(Ent.Id, response.Id);
            Assert.Equal(Ent.QuestionCompositionId, response.QuestionCompositionId);
            Assert.Equal(Ent.ApplicationDomainBackEndId, response.ApplicationDomainBackEndId);
        }

        [Fact]
        public void CanAnotherUserThanAdminGetAQuestionApplicationDomainBackEndById_TheAddMethodMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockRepo = factory.Create<IQuestionApplicationDomainBackEndRepo>();

            QuestionApplicationDomainBackEndService service = new QuestionApplicationDomainBackEndService(mockRepo.Object);

            service.GetQuestionApplicationDomainBackEndById(1, EnumHelper.PermissionsUser.GDPR);
            service.GetQuestionApplicationDomainBackEndById(1, EnumHelper.PermissionsUser.Read);
            service.GetQuestionApplicationDomainBackEndById(1, EnumHelper.PermissionsUser.Write);
            service.GetQuestionApplicationDomainBackEndById(1, EnumHelper.PermissionsUser.Owner);

            mockRepo.Verify(x => x.GetById(1), Times.Never);
        }

        [Fact]
        public void CanAnAdminGetAllTheQuestionApplicationDomainBackEnds_TheGetAllMethodMustBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockRepo = factory.Create<IQuestionApplicationDomainBackEndRepo>();

            QuestionApplicationDomainBackEndService service = new QuestionApplicationDomainBackEndService(mockRepo.Object);

            service.GetAllQuestionApplicationDomainBackEnds(EnumHelper.PermissionsUser.Admin);

            mockRepo.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public void CanAnAdminGetAllTheQuestionApplicationDomainBackEnds_IReceiveAListOfAllQuestionApplicationDomainBackEnds()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockRepo = factory.Create<IQuestionApplicationDomainBackEndRepo>();

            var listOfEnts = new List<QuestionApplicationDomainBackEnd>
            {
                new QuestionApplicationDomainBackEnd { Id = 1, QuestionCompositionId = 1, ApplicationDomainBackEndId = 1},
                new QuestionApplicationDomainBackEnd { Id = 2, QuestionCompositionId = 2, ApplicationDomainBackEndId = 2},
                new QuestionApplicationDomainBackEnd { Id = 3, QuestionCompositionId = 3, ApplicationDomainBackEndId = 3},

            };

            mockRepo.Setup(x => x.GetAll()).Returns(listOfEnts.AsQueryable());

            QuestionApplicationDomainBackEndService service = new QuestionApplicationDomainBackEndService(mockRepo.Object);


            var responseList = service.GetAllQuestionApplicationDomainBackEnds(EnumHelper.PermissionsUser.Admin);

            Assert.Equal(service.GetAllQuestionApplicationDomainBackEnds(EnumHelper.PermissionsUser.Admin)
                .FirstOrDefault().Id, responseList.FirstOrDefault().Id);
            Assert.Equal(service.GetAllQuestionApplicationDomainBackEnds(EnumHelper.PermissionsUser.Admin)
                .LastOrDefault().Id, responseList.LastOrDefault().Id);
            Assert.Equal(service.GetAllQuestionApplicationDomainBackEnds(EnumHelper.PermissionsUser.Admin)
                .FirstOrDefault().QuestionCompositionId, responseList.FirstOrDefault().QuestionCompositionId);
            Assert.Equal(service.GetAllQuestionApplicationDomainBackEnds(EnumHelper.PermissionsUser.Admin)
                .LastOrDefault().QuestionCompositionId, responseList.LastOrDefault().QuestionCompositionId);
        }

        [Fact]
        public void CanAnotherUserThanAdminGetAllTheQuestionApplicationDomainBackEnds_TheGetAllMethodMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockRepo = factory.Create<IQuestionApplicationDomainBackEndRepo>();

            QuestionApplicationDomainBackEndService service = new QuestionApplicationDomainBackEndService(mockRepo.Object);

            service.GetQuestionApplicationDomainBackEndById(1, EnumHelper.PermissionsUser.GDPR);
            service.GetQuestionApplicationDomainBackEndById(1, EnumHelper.PermissionsUser.Read);
            service.GetQuestionApplicationDomainBackEndById(1, EnumHelper.PermissionsUser.Write);
            service.GetQuestionApplicationDomainBackEndById(1, EnumHelper.PermissionsUser.Owner);

            mockRepo.Verify(x => x.GetAll(), Times.Never);
        }

        [Fact]
        public void CanAnAdminUpdateAnApplicationDomainBackEnd_TheUpdateMethodMustBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockRepo = factory.Create<IQuestionApplicationDomainBackEndRepo>();

            QuestionApplicationDomainBackEndDTO Dto = new QuestionApplicationDomainBackEndDTO
            {
                Id = 1,
                ApplicationDomainBackEndId = 1,
                QuestionCompositionId = 1
            };

            QuestionApplicationDomainBackEndService service = new QuestionApplicationDomainBackEndService(mockRepo.Object);


            service.UpdateQuestionApplicationDomainBackEnd(Dto, EnumHelper.PermissionsUser.Admin);

            mockRepo.Verify(x => x.Update(_mapper.Map<QuestionApplicationDomainBackEnd>(Dto)), Times.Once);
        }

        [Fact]
        public void CanAnotherUserThanAdminUpdateAnApplicationDomainBackEnd_TheUpdateMethodMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockRepo = factory.Create<IQuestionApplicationDomainBackEndRepo>();

            QuestionApplicationDomainBackEndService service = new QuestionApplicationDomainBackEndService(mockRepo.Object);

            QuestionApplicationDomainBackEndDTO Dto = new QuestionApplicationDomainBackEndDTO
            {
                Id = 1,
                ApplicationDomainBackEndId = 1,
                QuestionCompositionId = 1
            };

            service.UpdateQuestionApplicationDomainBackEnd(Dto, EnumHelper.PermissionsUser.GDPR);
            service.UpdateQuestionApplicationDomainBackEnd(Dto, EnumHelper.PermissionsUser.Read);
            service.UpdateQuestionApplicationDomainBackEnd(Dto, EnumHelper.PermissionsUser.Write);
            service.UpdateQuestionApplicationDomainBackEnd(Dto, EnumHelper.PermissionsUser.Owner);

            mockRepo.Verify(x => x.Update(_mapper.Map<QuestionApplicationDomainBackEnd>(Dto)), Times.Never);
        }
    }
}
