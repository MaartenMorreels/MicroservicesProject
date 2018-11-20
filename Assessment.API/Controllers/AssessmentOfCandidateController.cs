using System.Collections.Generic;
using Assessment.BLL.DTOs;
using Assessment.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Assessment.BLL.Helper;


namespace Assessment.API.Controllers
{
    [Produces("application/json")]
    [Route("api/AssessmentOfCandidate")]
    public class AssessmentOfCandidateController : Controller
    {
        private IAssessmentOfCandidateService _assessmentOfCandidateService;

        [Route("~/api/AssessmentOfCandidate/GetAssessmentOfCandidateById")]
        [HttpGet]
        public AssessmentOfCandidateDTO GetAssessmentOfCandidateById(int id)
        {
            var assessmentOfCandidate = _assessmentOfCandidateService.GetAssessmentOfCandidateById(id, EnumHelper.PermissionsUser.Admin);
            return assessmentOfCandidate;

        }

        [Route("~/api/AssessmentOfCandidate/GetAllAssessmentOfCandidates")]
        [HttpGet]
        public List<AssessmentOfCandidateDTO> GetAllAssessmentOfCandidates()
        {
            var assessmentOfCandidates = _assessmentOfCandidateService.GetAllAssessmentOfCandidates(EnumHelper.PermissionsUser.Admin);
            return assessmentOfCandidates;
        }

        [Route("~/api/AssessmentOfCandidate/AddAssessmentOfCandidate")]
        [HttpPost]
        public AssessmentOfCandidateDTO AddAssessmentOfCandidate(AssessmentOfCandidateDTO assessmentOfCandidateDto)
        {
            var assessmentOfCandidate = _assessmentOfCandidateService.AddAssessmentOfCandidate(assessmentOfCandidateDto, EnumHelper.PermissionsUser.Admin);
            return assessmentOfCandidate;
        }

        [Route("~/api/AssessmentOfCandidate/UpdateAssessmentOfCandidate")]
        [HttpPost]
        public AssessmentOfCandidateDTO UpdateAssessmentOfCandidate(AssessmentOfCandidateDTO assessmentOfCandidateDto)
        {
            var assessmentOfCandidate = _assessmentOfCandidateService.UpdateAssessmentOfCandidate(assessmentOfCandidateDto, EnumHelper.PermissionsUser.Admin);
            return assessmentOfCandidate;
        }
    }
}