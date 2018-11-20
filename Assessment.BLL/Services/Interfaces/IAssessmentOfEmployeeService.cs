using System.Collections.Generic;
using Assessment.BLL.DTOs;
using EnumHelper = Assessment.BLL.Helper.EnumHelper;

namespace Assessment.BLL.Services.Interfaces
{
    public interface IAssessmentOfEmployeeService
    {
        AssessmentOfEmployeeDTO AddAssessmentOfEmployee(AssessmentOfEmployeeDTO assessmentOfEmployeeDto, EnumHelper.PermissionsUser permission);
        AssessmentOfEmployeeDTO GetAssessmentOfEmployeeById(int assessmentOfEmployeeId, EnumHelper.PermissionsUser permission);
        List<AssessmentOfEmployeeDTO> GetAllAssessmentOfEmployees(EnumHelper.PermissionsUser permission);
        AssessmentOfEmployeeDTO UpdateAssessmentOfEmployee(AssessmentOfEmployeeDTO assessmentOfEmployeeDto, EnumHelper.PermissionsUser permission);
    }
}
