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
    public class QuestionApplicationDomainFrontEndServiceTest
    {
        private IMapper _mapper;

        public QuestionApplicationDomainFrontEndServiceTest()
        {
            MapperProfile map = new MapperProfile();
            _mapper = map.Mapper;
        }

        [Fact]
        public void CanAAdminCreateANewQuestionApplicationDomainFrontEnd_TheAddMethodMustBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockRepo = factory.Create<IQuestionApplicationDomainFrontEndRepo>();

            QuestionApplicationDomainFrontEndDTO Dto = new QuestionApplicationDomainFrontEndDTO
            {
                Id = 1,
                ApplicationDomainFrontEndId = 1,
                QuestionCompositionId = 1
            };

            QuestionApplicationDomainFrontEndService service = new QuestionApplicationDomainFrontEndService(mockRepo.Object);

            service.AddQuestionApplicationDomainFrontEnd(Dto, EnumHelper.PermissionsUser.Admin);

            mockRepo.Verify(x => x.Add(_mapper.Map<QuestionApplicationDomainFrontEnd>(Dto)), Times.Once);
        }

        [Fact]
        public void CanAAdminCreateANewQuestionApplicationDomainFrontEnd_IReceiveAQuestionApplicationDomainFrontEnd()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockRepo = factory.Create<IQuestionApplicationDomainFrontEndRepo>();

            QuestionApplicationDomainFrontEnd ent = new QuestionApplicationDomainFrontEnd
            {
                Id = 1,
                QuestionCompositionId = 1,
                ApplicationDomainFrontEndId = 1
            };

            mockRepo.Setup(x => x.Add(ent)).Returns(ent);

            QuestionApplicationDomainFrontEndService service = new QuestionApplicationDomainFrontEndService(mockRepo.Object);


            var response = service.AddQuestionApplicationDomainFrontEnd(_mapper.Map<QuestionApplicationDomainFrontEndDTO>(ent), EnumHelper.PermissionsUser.Admin);

            Assert.Equal(ent.Id, response.Id);
            Assert.Equal(ent.QuestionCompositionId, response.QuestionCompositionId);
            Assert.Equal(ent.ApplicationDomainFrontEndId, response.ApplicationDomainFrontEndId);
        }

        [Fact]
        public void CanAnotherUserThanAdminCreateANewQuestionApplicationDomainFrontEnd_TheAddMethodMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockRepo = factory.Create<IQuestionApplicationDomainFrontEndRepo>();

            QuestionApplicationDomainFrontEndDTO Dto = new QuestionApplicationDomainFrontEndDTO
            {
                Id = 1,
                ApplicationDomainFrontEndId = 1,
                QuestionCompositionId = 1
            };

            QuestionApplicationDomainFrontEndService service = new QuestionApplicationDomainFrontEndService(mockRepo.Object);

            service.AddQuestionApplicationDomainFrontEnd(Dto, EnumHelper.PermissionsUser.GDPR);
            service.AddQuestionApplicationDomainFrontEnd(Dto, EnumHelper.PermissionsUser.Read);
            service.AddQuestionApplicationDomainFrontEnd(Dto, EnumHelper.PermissionsUser.Write);
            service.AddQuestionApplicationDomainFrontEnd(Dto, EnumHelper.PermissionsUser.Owner);

            mockRepo.Verify(x => x.Add(_mapper.Map<QuestionApplicationDomainFrontEnd>(Dto)), Times.Never);
        }

        [Fact]
        public void CanAnAdminCreateANewQuestionApplicationDomainFrontEndWithoutApplicationDomainFrontEndId_TheAddMethodMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockRepo = factory.Create<IQuestionApplicationDomainFrontEndRepo>();

            QuestionApplicationDomainFrontEndDTO Dto = new QuestionApplicationDomainFrontEndDTO
            {
                Id = 1,
                QuestionCompositionId = 1
            };

            QuestionApplicationDomainFrontEndService service = new QuestionApplicationDomainFrontEndService(mockRepo.Object);

            service.AddQuestionApplicationDomainFrontEnd(Dto, EnumHelper.PermissionsUser.Admin);

            mockRepo.Verify(x => x.Add(_mapper.Map<QuestionApplicationDomainFrontEnd>(Dto)), Times.Never);
        }

        [Fact]
        public void CanAnAdminCreateANewQuestionApplicationDomainFrontEndWithoutQuestionCompositionId_TheAddMethodMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockRepo = factory.Create<IQuestionApplicationDomainFrontEndRepo>();

            QuestionApplicationDomainFrontEndDTO Dto = new QuestionApplicationDomainFrontEndDTO
            {
                Id = 1,
                ApplicationDomainFrontEndId = 1
            };

            QuestionApplicationDomainFrontEndService service = new QuestionApplicationDomainFrontEndService(mockRepo.Object);

            service.AddQuestionApplicationDomainFrontEnd(Dto, EnumHelper.PermissionsUser.Admin);

            mockRepo.Verify(x => x.Add(_mapper.Map<QuestionApplicationDomainFrontEnd>(Dto)), Times.Never);
        }

        [Fact]
        public void CanAnAdminCreateANewQuestionApplicationDomainFrontEndWithoutApplicationDomainFrontEndId_IReceiveANullObject()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockRepo = factory.Create<IQuestionApplicationDomainFrontEndRepo>();

            QuestionApplicationDomainFrontEndDTO Dto = new QuestionApplicationDomainFrontEndDTO
            {
                Id = 1,
                QuestionCompositionId = 1,
            };

            QuestionApplicationDomainFrontEndService service = new QuestionApplicationDomainFrontEndService(mockRepo.Object);

            var response = service.AddQuestionApplicationDomainFrontEnd(Dto, EnumHelper.PermissionsUser.Admin);

            Assert.Null(response);
        }

        [Fact]
        public void CanAnAdminCreateANewQuestionApplicationDomainFrontEndWithoutQuestionCompositionId_IReceiveANullObject()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockRepo = factory.Create<IQuestionApplicationDomainFrontEndRepo>();

            QuestionApplicationDomainFrontEndDTO Dto = new QuestionApplicationDomainFrontEndDTO
            {
                Id = 1,
                ApplicationDomainFrontEndId = 1
            };

            QuestionApplicationDomainFrontEndService service = new QuestionApplicationDomainFrontEndService(mockRepo.Object);

            var response = service.AddQuestionApplicationDomainFrontEnd(Dto, EnumHelper.PermissionsUser.Admin);

            Assert.Null(response);
        }

        [Fact]
        public void CanAnAdminGetAQuestionApplicationDomainFrontEndById_TheGetByIdMethodMustBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockRepo = factory.Create<IQuestionApplicationDomainFrontEndRepo>();

            QuestionApplicationDomainFrontEndService service = new QuestionApplicationDomainFrontEndService(mockRepo.Object);

            service.GetQuestionApplicationDomainFrontEndById(1, EnumHelper.PermissionsUser.Admin);

            mockRepo.Verify(x => x.GetById(1), Times.Once);
        }

        [Fact]
        public void CanAnAdminGetAQuestionApplicationDomainFrontEndById_IReceiveAQuestionApplicationDomainFrontEnd()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockRepo = factory.Create<IQuestionApplicationDomainFrontEndRepo>();

            QuestionApplicationDomainFrontEnd Ent = new QuestionApplicationDomainFrontEnd
            {
                Id = 1,
                ApplicationDomainFrontEndId = 1,
                QuestionCompositionId = 1
            };

            mockRepo.Setup(x => x.GetById(Ent.Id)).Returns(Ent);

            QuestionApplicationDomainFrontEndService service = new QuestionApplicationDomainFrontEndService(mockRepo.Object);

            var response = service.GetQuestionApplicationDomainFrontEndById(1, EnumHelper.PermissionsUser.Admin);

            Assert.Equal(Ent.Id, response.Id);
            Assert.Equal(Ent.QuestionCompositionId, response.QuestionCompositionId);
            Assert.Equal(Ent.ApplicationDomainFrontEndId, response.ApplicationDomainFrontEndId);
        }

        [Fact]
        public void CanAnotherUserThanAdminGetAQuestionApplicationDomainFrontEndById_TheAddMethodMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockRepo = factory.Create<IQuestionApplicationDomainFrontEndRepo>();

            QuestionApplicationDomainFrontEndService service = new QuestionApplicationDomainFrontEndService(mockRepo.Object);

            service.GetQuestionApplicationDomainFrontEndById(1, EnumHelper.PermissionsUser.GDPR);
            service.GetQuestionApplicationDomainFrontEndById(1, EnumHelper.PermissionsUser.Read);
            service.GetQuestionApplicationDomainFrontEndById(1, EnumHelper.PermissionsUser.Write);
            service.GetQuestionApplicationDomainFrontEndById(1, EnumHelper.PermissionsUser.Owner);

            mockRepo.Verify(x => x.GetById(1), Times.Never);
        }

        [Fact]
        public void CanAnAdminGetAllTheQuestionApplicationDomainFrontEnds_TheGetAllMethodMustBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockRepo = factory.Create<IQuestionApplicationDomainFrontEndRepo>();

            QuestionApplicationDomainFrontEndService service = new QuestionApplicationDomainFrontEndService(mockRepo.Object);

            service.GetAllQuestionApplicationDomainFrontEnds(EnumHelper.PermissionsUser.Admin);

            mockRepo.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public void CanAnAdminGetAllTheQuestionApplicationDomainFrontEnds_IReceiveAListOfAllQuestionApplicationDomainFrontEnds()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockRepo = factory.Create<IQuestionApplicationDomainFrontEndRepo>();

            var listOfEnts = new List<QuestionApplicationDomainFrontEnd>
            {
                new QuestionApplicationDomainFrontEnd { Id = 1, QuestionCompositionId = 1, ApplicationDomainFrontEndId = 1},
                new QuestionApplicationDomainFrontEnd { Id = 2, QuestionCompositionId = 2, ApplicationDomainFrontEndId = 2},
                new QuestionApplicationDomainFrontEnd { Id = 3, QuestionCompositionId = 3, ApplicationDomainFrontEndId = 3},

            };

            mockRepo.Setup(x => x.GetAll()).Returns(listOfEnts.AsQueryable());

            QuestionApplicationDomainFrontEndService service = new QuestionApplicationDomainFrontEndService(mockRepo.Object);


            var responseList = service.GetAllQuestionApplicationDomainFrontEnds(EnumHelper.PermissionsUser.Admin);

            Assert.Equal(service.GetAllQuestionApplicationDomainFrontEnds(EnumHelper.PermissionsUser.Admin)
                .FirstOrDefault().Id, responseList.FirstOrDefault().Id);
            Assert.Equal(service.GetAllQuestionApplicationDomainFrontEnds(EnumHelper.PermissionsUser.Admin)
                .LastOrDefault().Id, responseList.LastOrDefault().Id);
            Assert.Equal(service.GetAllQuestionApplicationDomainFrontEnds(EnumHelper.PermissionsUser.Admin)
                .FirstOrDefault().QuestionCompositionId, responseList.FirstOrDefault().QuestionCompositionId);
            Assert.Equal(service.GetAllQuestionApplicationDomainFrontEnds(EnumHelper.PermissionsUser.Admin)
                .LastOrDefault().QuestionCompositionId, responseList.LastOrDefault().QuestionCompositionId);
        }

        [Fact]
        public void CanAnotherUserThanAdminGetAllTheQuestionApplicationDomainFrontEnds_TheGetAllMethodMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockRepo = factory.Create<IQuestionApplicationDomainFrontEndRepo>();

            QuestionApplicationDomainFrontEndService service = new QuestionApplicationDomainFrontEndService(mockRepo.Object);

            service.GetQuestionApplicationDomainFrontEndById(1, EnumHelper.PermissionsUser.GDPR);
            service.GetQuestionApplicationDomainFrontEndById(1, EnumHelper.PermissionsUser.Read);
            service.GetQuestionApplicationDomainFrontEndById(1, EnumHelper.PermissionsUser.Write);
            service.GetQuestionApplicationDomainFrontEndById(1, EnumHelper.PermissionsUser.Owner);

            mockRepo.Verify(x => x.GetAll(), Times.Never);
        }

        [Fact]
        public void CanAnAdminUpdateAnApplicationDomainFrontEnd_TheUpdateMethodMustBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockRepo = factory.Create<IQuestionApplicationDomainFrontEndRepo>();

            QuestionApplicationDomainFrontEndDTO Dto = new QuestionApplicationDomainFrontEndDTO
            {
                Id = 1,
                ApplicationDomainFrontEndId = 1,
                QuestionCompositionId = 1
            };

            QuestionApplicationDomainFrontEndService service = new QuestionApplicationDomainFrontEndService(mockRepo.Object);

            service.UpdateQuestionApplicationDomainFrontEnd(Dto, EnumHelper.PermissionsUser.Admin);

            mockRepo.Verify(x => x.Update(_mapper.Map<QuestionApplicationDomainFrontEnd>(Dto)), Times.Once);
        }

        [Fact]
        public void CanAnotherUserThanAdminUpdateAnApplicationDomainFrontEnd_TheUpdateMethodMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockRepo = factory.Create<IQuestionApplicationDomainFrontEndRepo>();

            QuestionApplicationDomainFrontEndService service = new QuestionApplicationDomainFrontEndService(mockRepo.Object);

            QuestionApplicationDomainFrontEndDTO Dto = new QuestionApplicationDomainFrontEndDTO
            {
                Id = 1,
                ApplicationDomainFrontEndId = 1,
                QuestionCompositionId = 1
            };

            service.UpdateQuestionApplicationDomainFrontEnd(Dto, EnumHelper.PermissionsUser.GDPR);
            service.UpdateQuestionApplicationDomainFrontEnd(Dto, EnumHelper.PermissionsUser.Read);
            service.UpdateQuestionApplicationDomainFrontEnd(Dto, EnumHelper.PermissionsUser.Write);
            service.UpdateQuestionApplicationDomainFrontEnd(Dto, EnumHelper.PermissionsUser.Owner);

            mockRepo.Verify(x => x.Update(_mapper.Map<QuestionApplicationDomainFrontEnd>(Dto)), Times.Never);
        }
    }
}
