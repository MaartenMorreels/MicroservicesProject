using Assessment.DAL.Entities;
using Assessment.DAL.Repositories;
using Assessment.DAL.Tests.Core;
using Assessment.DAL.Tests.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Assessment.DAL.Tests
{
    public class AssessmentOfEmployeeREPOTests : AssessmentCoreTest
    {
        #region Private Fields

        private AssessmentOfEmployeeREPO _assessmentOfEmployeeREPO;

        private AssessmentOfEmployee _assessmentOfEmployee;
        private AssessmentOfEmployee _assessmentOfEmployeeAdded;
        private AssessmentOfEmployee _assessmentOfEmployeeDB;
        private AssessmentOfEmployee _assessmentOfEmployeeUpdate;
        //Lists
        private List<AssessmentOfEmployee> _assessmentOfEmployees;
        private List<AssessmentOfEmployee> _assessmentOfEmployeesDB;

        private int _assessmentOfEmployeeId1 = 1;
        private int _assessmentOfEmployeeId2 = 2;
        private int _newAssessmentOfEmployeeId = 3;

        private int _assessmentId1 = 1;
        private int _employeeId1 = 1;
        private int _assessmentId2 = 2;
        private int _employeeId2 = 2;

        private int _newAssessmentId = 3;
        private int _newEmployeeId = 3;

        #endregion

        public override void Init()
        {
            _assessmentOfEmployee = new AssessmentOfEmployeeFactory()
                .WithAssessmentOfEmployeeId(_newAssessmentOfEmployeeId)
                .WithAssessmentId(_newAssessmentId)
                .WithEmployeeId(_newEmployeeId)
                .Build();

            _assessmentOfEmployees = new List<AssessmentOfEmployee>
            {
                new AssessmentOfEmployeeFactory().WithAssessmentOfEmployeeId(_assessmentOfEmployeeId1).WithAssessmentId(_assessmentId1).WithAssessmentId(_employeeId1).Build(),
                new AssessmentOfEmployeeFactory().WithAssessmentOfEmployeeId(_assessmentOfEmployeeId2).WithAssessmentId(_assessmentId2).WithAssessmentId(_employeeId2).Build()
            };

            InitializeDbSet(_assessmentOfEmployees);
        }

        [Fact]
        public void ANewAssessmentOfEmployeeIsAddedCorrectlyToTheDB_AndCanBeRetrieved()
        {
            _assessmentOfEmployeeREPO = new AssessmentOfEmployeeREPO(Context);
            _assessmentOfEmployeeAdded = _assessmentOfEmployeeREPO.Add(_assessmentOfEmployee);
            _assessmentOfEmployeeDB = Context.AssessmentOfEmployees.First(x => x.Id == _newAssessmentOfEmployeeId);

            #region Asserts
            Assert.Equal(_newAssessmentOfEmployeeId, _assessmentOfEmployeeDB.Id);
            #endregion
        }

        [Fact]
        public void AnAssessmentOfEmployeeCanBeRetrievedById()
        {
            _assessmentOfEmployeeREPO = new AssessmentOfEmployeeREPO(Context);
            var assessmentOfEmployee = _assessmentOfEmployeeREPO.GetById(_assessmentOfEmployeeId1);
            _assessmentOfEmployeeDB = Context.AssessmentOfEmployees.First(x => x.Id == _assessmentOfEmployeeId1);

            #region Asserts
            Assert.NotNull(_assessmentOfEmployeeDB);
            Assert.Equal(assessmentOfEmployee.AssessmentId, _assessmentOfEmployeeDB.AssessmentId);
            Assert.Equal(assessmentOfEmployee.EmployeeId, _assessmentOfEmployeeDB.EmployeeId);
            #endregion

        }

        [Fact]
        public void WhenIUpdateAnAssessmentOfEmployee_TheChangesAreCorrectlyChangedInTheDB()
        {
            _assessmentOfEmployeeREPO = new AssessmentOfEmployeeREPO(Context);
            _assessmentOfEmployeeUpdate = _assessmentOfEmployeeREPO.GetById(_assessmentOfEmployeeId1);

            _assessmentOfEmployeeUpdate.AssessmentId = _assessmentId2;
            _assessmentOfEmployeeREPO.Update(_assessmentOfEmployeeUpdate);
            _assessmentOfEmployeeDB = Context.AssessmentOfEmployees.FirstOrDefault(x => x.Id == _assessmentOfEmployeeId1);

            #region Asserts
            Assert.Equal(_assessmentOfEmployeeUpdate.AssessmentId, _assessmentOfEmployeeDB.AssessmentId);
            #endregion
        }

        [Fact]
        public void WhenIAskForAListOfAssessmentOfEmployees_IReceiveAListOfAssessmentOfEmployees()
        {
            _assessmentOfEmployeeREPO = new AssessmentOfEmployeeREPO(Context);
            _assessmentOfEmployeesDB = _assessmentOfEmployeeREPO.GetAll().ToList();

            AssessmentOfEmployee first = _assessmentOfEmployees.FirstOrDefault();
            AssessmentOfEmployee last = _assessmentOfEmployees.LastOrDefault();

            AssessmentOfEmployee firstDB = _assessmentOfEmployeesDB.FirstOrDefault();
            AssessmentOfEmployee lastDB = _assessmentOfEmployeesDB.LastOrDefault();

            Assert.Equal(first.Id, firstDB.Id);
        }

    }
}
