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
    public class AssessmentOfEmployeeTest
    {
        private IMapper _mapper;

        public AssessmentOfEmployeeTest()
        {
            MapperProfile map = new MapperProfile();
            _mapper = map.Mapper;
        }

        [Fact]
        public void CanAnAdminCreateANewAssessmentOfEmployee_TheAddMethodMustBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockAssessmentOfEmployeeRepo = factory.Create<IAssessmentOfEmployeeREPO>();

            AssessmentOfEmployeeDTO assessmentOfEmployeeDTO = new AssessmentOfEmployeeDTO { Id = 1, AssessmentId = 1, EmployeeId = 1 };

            AssessmentOfEmployeeService assessmentOfEmployeeService = new AssessmentOfEmployeeService(mockAssessmentOfEmployeeRepo.Object);
           
            assessmentOfEmployeeService.AddAssessmentOfEmployee(assessmentOfEmployeeDTO, EnumHelper.PermissionsUser.Admin);

            mockAssessmentOfEmployeeRepo.Verify(x => x.Add(_mapper.Map<AssessmentOfEmployee>(assessmentOfEmployeeDTO)), Times.Once);
        }

        [Fact]
        public void CanAndAdminCreateANewAssessmentOfEmployee_IReceiveTheAddedAssessmentOfEmployee()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockAssessmentOfEmployeeRepo = factory.Create<IAssessmentOfEmployeeREPO>();

            AssessmentOfEmployee assessmentOfEmployee = new AssessmentOfEmployee
            {
                Id = 1,
                AssessmentId = 1,
                EmployeeId = 1
            };
            mockAssessmentOfEmployeeRepo.Setup(x => x.Add(assessmentOfEmployee)).Returns(assessmentOfEmployee);

            AssessmentOfEmployeeService assessmentOfEmployeeService = new AssessmentOfEmployeeService(mockAssessmentOfEmployeeRepo.Object);

            var response = assessmentOfEmployeeService.AddAssessmentOfEmployee(_mapper.Map<AssessmentOfEmployeeDTO>(assessmentOfEmployee), EnumHelper.PermissionsUser.Admin);

            Assert.Equal(assessmentOfEmployee.Id, response.Id);
            Assert.Equal(assessmentOfEmployee.AssessmentId, response.AssessmentId);
            Assert.Equal(assessmentOfEmployee.EmployeeId, response.EmployeeId);
        }

        [Fact]
        public void CanAnotherUserThanAdminCreateAnAssessmentOfEmployee_TheAddMethodMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockAssessmentOfEmployeeRepo = factory.Create<IAssessmentOfEmployeeREPO>();

            AssessmentOfEmployeeDTO assessmentOfEmployeeDTO = new AssessmentOfEmployeeDTO { Id = 1, AssessmentId = 1, EmployeeId = 1 };

            AssessmentOfEmployeeService assessmentOfEmployeeService = new AssessmentOfEmployeeService(mockAssessmentOfEmployeeRepo.Object);

            assessmentOfEmployeeService.AddAssessmentOfEmployee(assessmentOfEmployeeDTO, EnumHelper.PermissionsUser.GDPR);
            assessmentOfEmployeeService.AddAssessmentOfEmployee(assessmentOfEmployeeDTO, EnumHelper.PermissionsUser.Read);
            assessmentOfEmployeeService.AddAssessmentOfEmployee(assessmentOfEmployeeDTO, EnumHelper.PermissionsUser.Write);
            assessmentOfEmployeeService.AddAssessmentOfEmployee(assessmentOfEmployeeDTO, EnumHelper.PermissionsUser.Owner);

            mockAssessmentOfEmployeeRepo.Verify(x => x.Add(_mapper.Map<AssessmentOfEmployee>(assessmentOfEmployeeDTO)), Times.Never);
        }

        [Fact]
        public void CanAnAdminCreateANewAssessmentOfEmployeeWithoutAssessmentId_TheAddMethodMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockAssessmentOfEmployeeRepo = factory.Create<IAssessmentOfEmployeeREPO>();

            AssessmentOfEmployeeDTO assessmentOfEmployeeDTO = new AssessmentOfEmployeeDTO { Id = 1, EmployeeId = 1 };

            AssessmentOfEmployeeService assessmentOfEmployeeService = new AssessmentOfEmployeeService(mockAssessmentOfEmployeeRepo.Object);

            assessmentOfEmployeeService.AddAssessmentOfEmployee(assessmentOfEmployeeDTO, EnumHelper.PermissionsUser.Admin);

            mockAssessmentOfEmployeeRepo.Verify(x => x.Add(_mapper.Map<AssessmentOfEmployee>(assessmentOfEmployeeDTO)), Times.Never);
        }

        [Fact]
        public void CanAnAdminCreateANewAssessmentOfEmployeeWithoutEmployeeId_TheAddMethodMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockAssessmentOfEmployeeRepo = factory.Create<IAssessmentOfEmployeeREPO>();

            AssessmentOfEmployeeDTO assessmentOfEmployeeDTO = new AssessmentOfEmployeeDTO { Id = 1, AssessmentId = 1 };

            AssessmentOfEmployeeService assessmentOfEmployeeService = new AssessmentOfEmployeeService(mockAssessmentOfEmployeeRepo.Object);

            assessmentOfEmployeeService.AddAssessmentOfEmployee(assessmentOfEmployeeDTO, EnumHelper.PermissionsUser.Admin);

            mockAssessmentOfEmployeeRepo.Verify(x => x.Add(_mapper.Map<AssessmentOfEmployee>(assessmentOfEmployeeDTO)), Times.Never);
        }

        [Fact]
        public void CanAnAdminCreateANewAssessmentOfEmployeeWithoutAssessmentId_IWouldLikeToHaveANullObject()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockAssessmentOfEmployeeRepo = factory.Create<IAssessmentOfEmployeeREPO>();

            AssessmentOfEmployeeDTO assessmentOfEmployeeDTO = new AssessmentOfEmployeeDTO { Id = 1, EmployeeId = 1 };

            AssessmentOfEmployeeService assessmentOfEmployeeService = new AssessmentOfEmployeeService(mockAssessmentOfEmployeeRepo.Object);

            var responseAssessmentOfEmployee = assessmentOfEmployeeService.AddAssessmentOfEmployee(assessmentOfEmployeeDTO, EnumHelper.PermissionsUser.Admin);

            Assert.Null(responseAssessmentOfEmployee);
        }

        [Fact]
        public void CanAnAdminCreateANewAssessmentOfEmployeeWithoutEmployeeId_IWouldLikeToHaveANullObject()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockAssessmentOfEmployeeRepo = factory.Create<IAssessmentOfEmployeeREPO>();

            AssessmentOfEmployeeDTO assessmentOfEmployeeDTO = new AssessmentOfEmployeeDTO { Id = 1, AssessmentId = 1 };

            AssessmentOfEmployeeService assessmentOfEmployeeService = new AssessmentOfEmployeeService(mockAssessmentOfEmployeeRepo.Object);

            var responseAssessmentOfEmployee = assessmentOfEmployeeService.AddAssessmentOfEmployee(assessmentOfEmployeeDTO, EnumHelper.PermissionsUser.Admin);

            Assert.Null(responseAssessmentOfEmployee);
        }

        [Fact]
        public void CanAnAdminGetAnAssessmentOfEmployeeById_TheGetByIdMetgodMustBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockAssessmentOfEmployeeRepo = factory.Create<IAssessmentOfEmployeeREPO>();

            AssessmentOfEmployeeService assessmentOfEmployeeService = new AssessmentOfEmployeeService(mockAssessmentOfEmployeeRepo.Object);

            assessmentOfEmployeeService.GetAssessmentOfEmployeeById(1, EnumHelper.PermissionsUser.Admin);

            mockAssessmentOfEmployeeRepo.Verify(x => x.GetById(1), Times.Once);
        }

        [Fact]
        public void CanAnAdminGetAnAssessmentById_IReceiveAnAssessmentOfEmployee()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockAssessmentOfEmployeeRepo = factory.Create<IAssessmentOfEmployeeREPO>();

            AssessmentOfEmployee assessmentOfEmployee = new AssessmentOfEmployee
            {
                Id = 1,
                AssessmentId = 1,
                EmployeeId = 1
            };
            mockAssessmentOfEmployeeRepo.Setup(x => x.GetById(assessmentOfEmployee.Id)).Returns(assessmentOfEmployee);

            AssessmentOfEmployeeService assessmentOfEmployeeService = new AssessmentOfEmployeeService(mockAssessmentOfEmployeeRepo.Object);

            var response = assessmentOfEmployeeService.GetAssessmentOfEmployeeById(1, EnumHelper.PermissionsUser.Admin);

            Assert.Equal(assessmentOfEmployee.Id, response.Id);
            Assert.Equal(assessmentOfEmployee.AssessmentId, response.AssessmentId);
            Assert.Equal(assessmentOfEmployee.EmployeeId, response.EmployeeId);
        }

        [Fact]
        public void CanAnOtherUserThanAdminGetAssessmentOfEmployeeById_TheGetByIdMethodMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockAssessmentOfEmployeeRepo = factory.Create<IAssessmentOfEmployeeREPO>();

            AssessmentOfEmployeeService assessmentOfEmployeeService = new AssessmentOfEmployeeService(mockAssessmentOfEmployeeRepo.Object);

            assessmentOfEmployeeService.GetAssessmentOfEmployeeById(1, EnumHelper.PermissionsUser.GDPR);
            assessmentOfEmployeeService.GetAssessmentOfEmployeeById(1, EnumHelper.PermissionsUser.Read);
            assessmentOfEmployeeService.GetAssessmentOfEmployeeById(1, EnumHelper.PermissionsUser.Write);
            assessmentOfEmployeeService.GetAssessmentOfEmployeeById(1, EnumHelper.PermissionsUser.Owner);

            mockAssessmentOfEmployeeRepo.Verify(x => x.GetById(1), Times.Never);
        }

        [Fact]
        public void CanAnAdminGetAllAssessmentOfEmployees_TheGetAllAssessmentOfEmployeesMethodMustBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockAssessmentOfEmployeeRepo = factory.Create<IAssessmentOfEmployeeREPO>();

            AssessmentOfEmployeeService assessmentOfEmployeeService = new AssessmentOfEmployeeService(mockAssessmentOfEmployeeRepo.Object);

            assessmentOfEmployeeService.GetAllAssessmentOfEmployees(EnumHelper.PermissionsUser.Admin);

            mockAssessmentOfEmployeeRepo.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public void CanAnAdminGetAllAssessmentOfEmployees_IReceiveAListOfAssessmentOfEmployees()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockAssessmentOfEmployeeRepo = factory.Create<IAssessmentOfEmployeeREPO>();

            var listOfAssessmentOfEmployees = new List<AssessmentOfEmployee>
            {
                new AssessmentOfEmployee
                {
                    Id=1,
                    AssessmentId=1,
                    EmployeeId=1
                },
                new AssessmentOfEmployee
                {
                    Id=2,
                    AssessmentId=2,
                    EmployeeId=2
                },
            };

            mockAssessmentOfEmployeeRepo.Setup(x => x.GetAll()).Returns(listOfAssessmentOfEmployees.AsQueryable());

            AssessmentOfEmployeeService assessmentOfEmployeeService = new AssessmentOfEmployeeService(mockAssessmentOfEmployeeRepo.Object);


            var response = assessmentOfEmployeeService.GetAllAssessmentOfEmployees(EnumHelper.PermissionsUser.Admin);

            Assert.Equal(listOfAssessmentOfEmployees.FirstOrDefault().Id, response.FirstOrDefault().Id);
            Assert.Equal(listOfAssessmentOfEmployees.FirstOrDefault().AssessmentId, response.FirstOrDefault().AssessmentId);
            Assert.Equal(listOfAssessmentOfEmployees.FirstOrDefault().EmployeeId, response.FirstOrDefault().EmployeeId);
        }

        [Fact]
        public void CanAnOtherUserThanAdminGetAllAssessmentOfEmployees_TheGetAllAssessmentOfEmployeesMethodMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockAssessmentOfEmployeeRepo = factory.Create<IAssessmentOfEmployeeREPO>();

            AssessmentOfEmployeeService assessmentOfEmployeeService = new AssessmentOfEmployeeService(mockAssessmentOfEmployeeRepo.Object);


            assessmentOfEmployeeService.GetAllAssessmentOfEmployees(EnumHelper.PermissionsUser.GDPR);
            assessmentOfEmployeeService.GetAllAssessmentOfEmployees(EnumHelper.PermissionsUser.Read);
            assessmentOfEmployeeService.GetAllAssessmentOfEmployees(EnumHelper.PermissionsUser.Write);
            assessmentOfEmployeeService.GetAllAssessmentOfEmployees(EnumHelper.PermissionsUser.Owner);

            mockAssessmentOfEmployeeRepo.Verify(x => x.GetAll(), Times.Never);
        }

        [Fact]
        public void CanAnAdminUpdateAnAssessmentOfEmployee_TheUpdateMethodMustBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockAssessmentOfEmployeeRepo = factory.Create<IAssessmentOfEmployeeREPO>();

            AssessmentOfEmployeeDTO assessmentOfEmployeeDTO = new AssessmentOfEmployeeDTO { Id = 1, AssessmentId = 1, EmployeeId = 1 };

            AssessmentOfEmployeeService assessmentOfEmployeeService = new AssessmentOfEmployeeService(mockAssessmentOfEmployeeRepo.Object);


            assessmentOfEmployeeService.UpdateAssessmentOfEmployee(assessmentOfEmployeeDTO, EnumHelper.PermissionsUser.Admin);
            mockAssessmentOfEmployeeRepo.Verify(x => x.Update(_mapper.Map<AssessmentOfEmployee>(assessmentOfEmployeeDTO)), Times.Once);
        }

        [Fact]
        public void CanAnAdminUpdateAnAssessmentOfEmployee_IReceiveTheUpdatedAssessmentOfEmployee()
        {
            AssessmentOfEmployee assessmentOfEmployee = new AssessmentOfEmployee
            {
                Id = 1,
                AssessmentId = 1,
                EmployeeId = 1
            };

            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockAssessmentOfEmployeeRepo = factory.Create<IAssessmentOfEmployeeREPO>();

            mockAssessmentOfEmployeeRepo.Setup(x => x.Update(assessmentOfEmployee)).Returns(assessmentOfEmployee);

            AssessmentOfEmployeeService assessmentOfEmployeeService = new AssessmentOfEmployeeService(mockAssessmentOfEmployeeRepo.Object);

            var response = assessmentOfEmployeeService.UpdateAssessmentOfEmployee(_mapper.Map<AssessmentOfEmployeeDTO>(assessmentOfEmployee), EnumHelper.PermissionsUser.Admin);

            Assert.Equal(assessmentOfEmployee.Id, response.Id);
            Assert.Equal(assessmentOfEmployee.AssessmentId, response.AssessmentId);
            Assert.Equal(assessmentOfEmployee.EmployeeId, response.EmployeeId);
        }

        [Fact]
        public void CanAnOtherUserThanAdminUpdateAnAssessmentOfEmployee_TheUpdateMethodMustNotBeCalled()
        {
            MockRepository factory = new MockRepository(MockBehavior.Loose);
            var mockAssessmentOfEmployeeRepo = factory.Create<IAssessmentOfEmployeeREPO>();

            AssessmentOfEmployeeDTO assessmentOfEmployeeDTO = new AssessmentOfEmployeeDTO { Id = 1, AssessmentId = 1, EmployeeId = 1 };
            AssessmentOfEmployeeService assessmentOfEmployeeService = new AssessmentOfEmployeeService(mockAssessmentOfEmployeeRepo.Object);


            assessmentOfEmployeeService.UpdateAssessmentOfEmployee(assessmentOfEmployeeDTO, EnumHelper.PermissionsUser.GDPR);
            assessmentOfEmployeeService.UpdateAssessmentOfEmployee(assessmentOfEmployeeDTO, EnumHelper.PermissionsUser.Read);
            assessmentOfEmployeeService.UpdateAssessmentOfEmployee(assessmentOfEmployeeDTO, EnumHelper.PermissionsUser.Write);
            assessmentOfEmployeeService.UpdateAssessmentOfEmployee(assessmentOfEmployeeDTO, EnumHelper.PermissionsUser.Owner);

            mockAssessmentOfEmployeeRepo.Verify(x => x.Update(_mapper.Map<AssessmentOfEmployee>(assessmentOfEmployeeDTO)), Times.Never);
        }
    }
}
