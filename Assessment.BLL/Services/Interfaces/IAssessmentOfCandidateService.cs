using System.Collections.Generic;
using Assessment.BLL.DTOs;
using EnumHelper = Assessment.BLL.Helper.EnumHelper;

namespace Assessment.BLL.Services.Interfaces
{
    public interface IAssessmentOfCandidateService
    {
        AssessmentOfCandidateDTO AddAssessmentOfCandidate(AssessmentOfCandidateDTO assessmentOfCandidateDto, EnumHelper.PermissionsUser permission);
        AssessmentOfCandidateDTO GetAssessmentOfCandidateById(int assessmentOfCandidateId, EnumHelper.PermissionsUser permission);
        List<AssessmentOfCandidateDTO> GetAllAssessmentOfCandidates(EnumHelper.PermissionsUser permission);
        AssessmentOfCandidateDTO UpdateAssessmentOfCandidate(AssessmentOfCandidateDTO assessmentOfCandidateDto, EnumHelper.PermissionsUser permission);
    }
}
