using System.Collections.Generic;
using Assessment.BLL.DTOs;
using Assessment.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Assessment.BLL.Helper;


namespace Assessment.API.Controllers
{
    [Produces("application/json")]
    [Route("api/AssessmentOfEmployee")]
    public class AssessmentOfEmployeeController : Controller
    {
        private IAssessmentOfEmployeeService _assessmentOfEmployeeService;

        [Route("~/api/AssessmentOfEmployee/GetAssessmentOfEmployeeById")]
        [HttpGet]
        public AssessmentOfEmployeeDTO GetAssessmentOfEmployeeById(int id)
        {
            var assessmentOfEmployee = _assessmentOfEmployeeService.GetAssessmentOfEmployeeById(id, EnumHelper.PermissionsUser.Admin);
            return assessmentOfEmployee;

        }

        [Route("~/api/AssessmentOfEmployee/GetAllAssessmentOfEmployees")]
        [HttpGet]
        public List<AssessmentOfEmployeeDTO> GetAllAssessmentOfEmployees()
        {
            var assessmentOfEmployees = _assessmentOfEmployeeService.GetAllAssessmentOfEmployees(EnumHelper.PermissionsUser.Admin);
            return assessmentOfEmployees;
        }

        [Route("~/api/AssessmentOfEmployee/AddAssessmentOfEmployee")]
        [HttpPost]
        public AssessmentOfEmployeeDTO AddAssessmentOfEmployee(AssessmentOfEmployeeDTO assessmentOfEmployeeDto)
        {
            var assessmentOfEmployee = _assessmentOfEmployeeService.AddAssessmentOfEmployee(assessmentOfEmployeeDto, EnumHelper.PermissionsUser.Admin);
            return assessmentOfEmployee;
        }

        [Route("~/api/AssessmentOfEmployee/UpdateAssessmentOfEmployee")]
        [HttpPost]
        public AssessmentOfEmployeeDTO UpdateAssessmentOfEmployee(AssessmentOfEmployeeDTO assessmentOfEmployeeDto)
        {
            var assessmentOfEmployee = _assessmentOfEmployeeService.UpdateAssessmentOfEmployee(assessmentOfEmployeeDto, EnumHelper.PermissionsUser.Admin);
            return assessmentOfEmployee;
        }
    }
}