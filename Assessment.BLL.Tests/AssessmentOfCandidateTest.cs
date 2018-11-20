using Assessment.BLL.DTOs;
using Assessment.BLL.Helper;
using Assessment.BLL.Services;
using Assessment.DAL.Entities;
using Assessment.DAL.Repositories.Interfaces;
using AutoMapper;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Assessment.BLL.Tests
{
    public class AssessmentOfCandidateTest
    {
        private IMapper _mapper;

        public AssessmentOfCandidateTest()
        {
            MapperProfile map = new MapperProfile();
            _mapper = map.Mapper;
        }

        [Fact]
        public void CanAnAdminCreateANewAssessmentOfCandidate_TheAddMethodMustBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockAssessmentOfCandidateRepo = factory.Create<IAssessmentOfCandidateRepo>();

            AssessmentOfCandidateDTO assessmentOfCandidateDTO = new AssessmentOfCandidateDTO { Id = 1, AssessmentId = 1, CandidateId = 1 };

            AssessmentOfCandidateService assessmentOfCandidateService = new AssessmentOfCandidateService(mockAssessmentOfCandidateRepo.Object);

            assessmentOfCandidateService.AddAssessmentOfCandidate(assessmentOfCandidateDTO, EnumHelper.PermissionsUser.Admin);

            mockAssessmentOfCandidateRepo.Verify(x => x.Add(_mapper.Map<AssessmentOfCandidate>(assessmentOfCandidateDTO)), Times.Once);
        }

        [Fact]
        public void CanAndAdminCreateANewAssessmentOfCandidate_IReceiveTheAddedAssessmentOfCandidate()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockAssessmentOfCandidateRepo = factory.Create<IAssessmentOfCandidateRepo>();

            AssessmentOfCandidate assessmentOfCandidate = new AssessmentOfCandidate
            {
                Id = 1,
                AssessmentId = 1,
                CandidateId = 1
            };
            mockAssessmentOfCandidateRepo.Setup(x => x.Add(assessmentOfCandidate)).Returns(assessmentOfCandidate);

            AssessmentOfCandidateService assessmentOfCandidateService = new AssessmentOfCandidateService(mockAssessmentOfCandidateRepo.Object);

            var response = assessmentOfCandidateService.AddAssessmentOfCandidate(_mapper.Map<AssessmentOfCandidateDTO>(assessmentOfCandidate), EnumHelper.PermissionsUser.Admin);

            Assert.Equal(assessmentOfCandidate.Id, response.Id);
            Assert.Equal(assessmentOfCandidate.AssessmentId, response.AssessmentId);
            Assert.Equal(assessmentOfCandidate.CandidateId, response.CandidateId);
        }

        [Fact]
        public void CanAnotherUserThanAdminCreateAnAssessmentOfCandidate_TheAddMethodMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockAssessmentOfCandidateRepo = factory.Create<IAssessmentOfCandidateRepo>();

            AssessmentOfCandidateDTO assessmentOfCandidateDTO = new AssessmentOfCandidateDTO { Id = 1, AssessmentId = 1, CandidateId = 1 };

            AssessmentOfCandidateService assessmentOfCandidateService = new AssessmentOfCandidateService(mockAssessmentOfCandidateRepo.Object);

            assessmentOfCandidateService.AddAssessmentOfCandidate(assessmentOfCandidateDTO, EnumHelper.PermissionsUser.GDPR);
            assessmentOfCandidateService.AddAssessmentOfCandidate(assessmentOfCandidateDTO, EnumHelper.PermissionsUser.Read);
            assessmentOfCandidateService.AddAssessmentOfCandidate(assessmentOfCandidateDTO, EnumHelper.PermissionsUser.Write);
            assessmentOfCandidateService.AddAssessmentOfCandidate(assessmentOfCandidateDTO, EnumHelper.PermissionsUser.Owner);

            mockAssessmentOfCandidateRepo.Verify(x => x.Add(_mapper.Map<AssessmentOfCandidate>(assessmentOfCandidateDTO)), Times.Never);
        }

        [Fact]
        public void CanAnAdminCreateANewAssessmentOfCandidateWithoutAssessmentId_TheAddMethodMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockAssessmentOfCandidateRepo = factory.Create<IAssessmentOfCandidateRepo>();

            AssessmentOfCandidateDTO assessmentOfCandidateDTO = new AssessmentOfCandidateDTO { Id = 1, CandidateId = 1 };

            AssessmentOfCandidateService assessmentOfCandidateService =new AssessmentOfCandidateService(mockAssessmentOfCandidateRepo.Object);
           
            assessmentOfCandidateService.AddAssessmentOfCandidate(assessmentOfCandidateDTO, EnumHelper.PermissionsUser.Admin);

            mockAssessmentOfCandidateRepo.Verify(x => x.Add(_mapper.Map<AssessmentOfCandidate>(assessmentOfCandidateDTO)), Times.Never);
        }

        [Fact]
        public void CanAnAdminCreateANewAssessmentOfCandidateWithoutCandidateId_TheAddMethodMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockAssessmentOfCandidateRepo = factory.Create<IAssessmentOfCandidateRepo>();

            AssessmentOfCandidateDTO assessmentOfCandidateDTO = new AssessmentOfCandidateDTO { Id = 1, AssessmentId = 1 };

            AssessmentOfCandidateService assessmentOfCandidateService = new AssessmentOfCandidateService(mockAssessmentOfCandidateRepo.Object);

            assessmentOfCandidateService.AddAssessmentOfCandidate(assessmentOfCandidateDTO, EnumHelper.PermissionsUser.Admin);

            mockAssessmentOfCandidateRepo.Verify(x => x.Add(_mapper.Map<AssessmentOfCandidate>(assessmentOfCandidateDTO)), Times.Never);
        }

        [Fact]
        public void CanAnAdminCreateANewAssessmentOfCandidateWithoutAssessmentId_IWouldLikeToHaveANullObject()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockAssessmentOfCandidateRepo = factory.Create<IAssessmentOfCandidateRepo>();

            AssessmentOfCandidateDTO assessmentOfCandidateDTO = new AssessmentOfCandidateDTO { Id = 1, CandidateId = 1 };

            AssessmentOfCandidateService assessmentOfCandidateService = new AssessmentOfCandidateService(mockAssessmentOfCandidateRepo.Object);

            var responseAssessmentOfCandidate = assessmentOfCandidateService.AddAssessmentOfCandidate(assessmentOfCandidateDTO, EnumHelper.PermissionsUser.Admin);

            Assert.Null(responseAssessmentOfCandidate);
        }

        [Fact]
        public void CanAnAdminCreateANewAssessmentOfCandidateWithoutCandidateId_IWouldLikeToHaveANullObject()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockAssessmentOfCandidateRepo = factory.Create<IAssessmentOfCandidateRepo>();

            AssessmentOfCandidateDTO assessmentOfCandidateDTO = new AssessmentOfCandidateDTO { Id = 1, AssessmentId = 1 };

            AssessmentOfCandidateService assessmentOfCandidateService = new AssessmentOfCandidateService(mockAssessmentOfCandidateRepo.Object);

            var responseAssessmentOfCandidate = assessmentOfCandidateService.AddAssessmentOfCandidate(assessmentOfCandidateDTO, EnumHelper.PermissionsUser.Admin);

            Assert.Null(responseAssessmentOfCandidate);
        }

        [Fact]
        public void CanAnAdminGetAnAssessmentOfCandidateById_TheGetByIdMetgodMustBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockAssessmentOfCandidateRepo = factory.Create<IAssessmentOfCandidateRepo>();

            AssessmentOfCandidateService assessmentOfCandidateService = new AssessmentOfCandidateService(mockAssessmentOfCandidateRepo.Object);

            assessmentOfCandidateService.GetAssessmentOfCandidateById(1, EnumHelper.PermissionsUser.Admin);

            mockAssessmentOfCandidateRepo.Verify(x => x.GetById(1), Times.Once);
        }

        [Fact]
        public void CanAnAdminGetAnAssessmentById_IReceiveAnAssessmentOfCandidate()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockAssessmentOfCandidateRepo = factory.Create<IAssessmentOfCandidateRepo>();

            AssessmentOfCandidate assessmentOfCandidate = new AssessmentOfCandidate
            {
                Id = 1,
                AssessmentId = 1,
                CandidateId = 1
            };
            mockAssessmentOfCandidateRepo.Setup(x => x.GetById(assessmentOfCandidate.Id)).Returns(assessmentOfCandidate);

            AssessmentOfCandidateService assessmentOfCandidateService = new AssessmentOfCandidateService(mockAssessmentOfCandidateRepo.Object);

            var response = assessmentOfCandidateService.GetAssessmentOfCandidateById(1, EnumHelper.PermissionsUser.Admin);

            Assert.Equal(assessmentOfCandidate.Id, response.Id);
            Assert.Equal(assessmentOfCandidate.AssessmentId, response.AssessmentId);
            Assert.Equal(assessmentOfCandidate.CandidateId, response.CandidateId);
        }

        [Fact]
        public void CanAnOtherUserThanAdminGetAssessmentOfCandidateById_TheGetByIdMethodMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockAssessmentOfCandidateRepo = factory.Create<IAssessmentOfCandidateRepo>();

            AssessmentOfCandidateService assessmentOfCandidateService = new AssessmentOfCandidateService(mockAssessmentOfCandidateRepo.Object);

            assessmentOfCandidateService.GetAssessmentOfCandidateById(1, EnumHelper.PermissionsUser.GDPR);
            assessmentOfCandidateService.GetAssessmentOfCandidateById(1, EnumHelper.PermissionsUser.Read);
            assessmentOfCandidateService.GetAssessmentOfCandidateById(1, EnumHelper.PermissionsUser.Write);
            assessmentOfCandidateService.GetAssessmentOfCandidateById(1, EnumHelper.PermissionsUser.Owner);

            mockAssessmentOfCandidateRepo.Verify(x => x.GetById(1), Times.Never);
        }

        [Fact]
        public void CanAnAdminGetAllAssessmentOfCandidates_TheGetAllAssessmentOfCandidatesMethodMustBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockAssessmentOfCandidateRepo = factory.Create<IAssessmentOfCandidateRepo>();

            AssessmentOfCandidateService assessmentOfCandidateService = new AssessmentOfCandidateService(mockAssessmentOfCandidateRepo.Object);

            assessmentOfCandidateService.GetAllAssessmentOfCandidates(EnumHelper.PermissionsUser.Admin);

            mockAssessmentOfCandidateRepo.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public void CanAnAdminGetAllAssessmentOfCandidates_IReceiveAListOfAssessmentOfCandidates()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockAssessmentOfCandidateRepo = factory.Create<IAssessmentOfCandidateRepo>();

            var listOfAssessmentOfCandidates = new List<AssessmentOfCandidate>
            {
                new AssessmentOfCandidate
                {
                    Id=1,
                    AssessmentId=1,
                    CandidateId=1
                },
                new AssessmentOfCandidate
                {
                    Id=2,
                    AssessmentId=2,
                    CandidateId=2
                },
            };

            mockAssessmentOfCandidateRepo.Setup(x => x.GetAll()).Returns(listOfAssessmentOfCandidates.AsQueryable());

            AssessmentOfCandidateService assessmentOfCandidateService = new AssessmentOfCandidateService(mockAssessmentOfCandidateRepo.Object);

            var response = assessmentOfCandidateService.GetAllAssessmentOfCandidates(EnumHelper.PermissionsUser.Admin);

            Assert.Equal(listOfAssessmentOfCandidates.FirstOrDefault().Id, response.FirstOrDefault().Id);
            Assert.Equal(listOfAssessmentOfCandidates.FirstOrDefault().AssessmentId, response.FirstOrDefault().AssessmentId);
            Assert.Equal(listOfAssessmentOfCandidates.FirstOrDefault().CandidateId, response.FirstOrDefault().CandidateId);
        }

        [Fact]
        public void CanAnOtherUserThanAdminGetAllAssessmentOfCandidates_TheGetAllAssessmentOfCandidatesMethodMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockAssessmentOfCandidateRepo = factory.Create<IAssessmentOfCandidateRepo>();

            AssessmentOfCandidateService assessmentOfCandidateService = new AssessmentOfCandidateService(mockAssessmentOfCandidateRepo.Object);


            assessmentOfCandidateService.GetAllAssessmentOfCandidates(EnumHelper.PermissionsUser.GDPR);
            assessmentOfCandidateService.GetAllAssessmentOfCandidates(EnumHelper.PermissionsUser.Read);
            assessmentOfCandidateService.GetAllAssessmentOfCandidates(EnumHelper.PermissionsUser.Write);
            assessmentOfCandidateService.GetAllAssessmentOfCandidates(EnumHelper.PermissionsUser.Owner);

            mockAssessmentOfCandidateRepo.Verify(x => x.GetAll(), Times.Never);
        }

        [Fact]
        public void CanAnAdminUpdateAnAssessmentOfCandidate_TheUpdateMethodMustBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockAssessmentOfCandidateRepo = factory.Create<IAssessmentOfCandidateRepo>();

            AssessmentOfCandidateDTO assessmentOfCandidateDTO = new AssessmentOfCandidateDTO { Id = 1, AssessmentId = 1, CandidateId = 1 };

            AssessmentOfCandidateService assessmentOfCandidateService = new AssessmentOfCandidateService(mockAssessmentOfCandidateRepo.Object);


            assessmentOfCandidateService.UpdateAssessmentOfCandidate(assessmentOfCandidateDTO, EnumHelper.PermissionsUser.Admin);
            mockAssessmentOfCandidateRepo.Verify(x => x.Update(_mapper.Map<AssessmentOfCandidate>(assessmentOfCandidateDTO)), Times.Once);
        }

        [Fact]
        public void CanAnAdminUpdateAnAssessmentOfCandidate_IReceiveTheUpdatedAssessmentOfCandidate()
        {
            AssessmentOfCandidate assessmentOfCandidate = new AssessmentOfCandidate
            {
                Id = 1,
                AssessmentId = 1,
                CandidateId = 1
            };

            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockAssessmentOfCandidateRepo = factory.Create<IAssessmentOfCandidateRepo>();

            mockAssessmentOfCandidateRepo.Setup(x => x.Update(assessmentOfCandidate)).Returns(assessmentOfCandidate);

            AssessmentOfCandidateService assessmentOfCandidateService = new AssessmentOfCandidateService(mockAssessmentOfCandidateRepo.Object);

            var response = assessmentOfCandidateService.UpdateAssessmentOfCandidate(_mapper.Map<AssessmentOfCandidateDTO>(assessmentOfCandidate), EnumHelper.PermissionsUser.Admin);

            Assert.Equal(assessmentOfCandidate.Id, response.Id);
            Assert.Equal(assessmentOfCandidate.AssessmentId, response.AssessmentId);
            Assert.Equal(assessmentOfCandidate.CandidateId, response.CandidateId);
        }

        [Fact]
        public void CanAnOtherUserThanAdminUpdateAnAssessmentOfCandidate_TheUpdateMethodMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockAssessmentOfCandidateRepo = factory.Create<IAssessmentOfCandidateRepo>();

            AssessmentOfCandidateDTO assessmentOfCandidateDTO = new AssessmentOfCandidateDTO { Id = 1, AssessmentId = 1, CandidateId = 1 };

            AssessmentOfCandidateService assessmentOfCandidateService = new AssessmentOfCandidateService(mockAssessmentOfCandidateRepo.Object);


            assessmentOfCandidateService.UpdateAssessmentOfCandidate(assessmentOfCandidateDTO,EnumHelper.PermissionsUser.GDPR);
            assessmentOfCandidateService.UpdateAssessmentOfCandidate(assessmentOfCandidateDTO,EnumHelper.PermissionsUser.Read);
            assessmentOfCandidateService.UpdateAssessmentOfCandidate(assessmentOfCandidateDTO,EnumHelper.PermissionsUser.Write);
            assessmentOfCandidateService.UpdateAssessmentOfCandidate(assessmentOfCandidateDTO,EnumHelper.PermissionsUser.Owner);

            mockAssessmentOfCandidateRepo.Verify(x => x.Update(_mapper.Map<AssessmentOfCandidate>(assessmentOfCandidateDTO)), Times.Never);
        }
    }
}
