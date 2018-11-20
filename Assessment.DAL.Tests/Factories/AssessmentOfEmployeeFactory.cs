using Assessment.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assessment.DAL.Tests.Factories
{
    public class AssessmentOfEmployeeFactory
    {
        private int _assessmentOfEmployeeId = 1;
        private int _assessmentId = 1;
        public int _employeeId = 1;

        public AssessmentOfEmployee Build()
        {
            return new AssessmentOfEmployee()
            {
                Id = _assessmentOfEmployeeId,
                AssessmentId = _assessmentId,
                EmployeeId = _employeeId
            };
        }

        public AssessmentOfEmployeeFactory WithAssessmentOfEmployeeId(int assessmentOfEmployeeId)
        {
            _assessmentOfEmployeeId = assessmentOfEmployeeId;
            return this;
        }

        public AssessmentOfEmployeeFactory WithAssessmentId(int assessmentId)
        {
            _assessmentId = assessmentId;
            return this;
        }

        public AssessmentOfEmployeeFactory WithEmployeeId(int employeeId)
        {
            _employeeId = employeeId;
            return this;
        }
    }
}
