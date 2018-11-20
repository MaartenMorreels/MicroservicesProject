using System.Collections.Generic;
using Assessment.BLL.DTOs;
using Assessment.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Assessment.BLL.Helper;


namespace Assessment.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Assessment")]
    public class AssessmentController : Controller
    {
        private IAssessmentService _assessmentService;

        public AssessmentController(IAssessmentService assessmentService)
        {
            _assessmentService = assessmentService;
        }

        [Route("~/api/Assessment/GetAssessmentById")]
        [HttpGet]
        public AssessmentDTO GetAssessmentById(int id)
        {
            var assessment = _assessmentService.GetAssessmentById(id, EnumHelper.PermissionsUser.Admin);
            return assessment;
        }

        [Route("~/api/Assessment/GetAllAssessments")]
        [HttpGet]
        public List<AssessmentDTO> GetAllAssessments()
        {
            var assessments = _assessmentService.GetAllAssessments(EnumHelper.PermissionsUser.Admin);
            return assessments;
        }

        [Route("~/api/Assessment/AddAssessment")]
        [HttpPost]
        public AssessmentDTO AddAssessment(int questionaryId, AssessmentDTO assessmentDto, DAL.Helper.EnumHelper.Difficulty difficulty)
        {
            var assessment = _assessmentService.AddAssessment(questionaryId, assessmentDto, EnumHelper.PermissionsUser.Admin);
            return assessment;
        }

        [Route("~/api/Assessment/UpdateAssessment")]
        [HttpPost]
        public AssessmentDTO UpdateAssessment(AssessmentDTO assessmentDto)
        {
            var assessment = _assessmentService.UpdateAssessment(assessmentDto, EnumHelper.PermissionsUser.Admin);
            return assessment;
        }
    }
}