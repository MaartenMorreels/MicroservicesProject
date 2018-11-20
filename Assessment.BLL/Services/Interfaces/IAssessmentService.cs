using Assessment.BLL.DTOs;
using Assessment.DAL.Entities;
using Assessment.DAL.Helper;
using System.Collections.Generic;
using EnumHelper = Assessment.BLL.Helper.EnumHelper;

namespace Assessment.BLL.Services.Interfaces
{
    public interface IAssessmentService
    {
        AssessmentDTO AddAssessment(int questionaryId, AssessmentDTO assessmentDto, EnumHelper.PermissionsUser permission);

        AssessmentDTO GetAssessmentById(int assessmentId, EnumHelper.PermissionsUser permission);

        List<AssessmentDTO> GetAllAssessments(EnumHelper.PermissionsUser permission);

        AssessmentDTO UpdateAssessment(AssessmentDTO assessmentDto, EnumHelper.PermissionsUser permission);//vragen en anwtoorden aanpassen

        AssessmentDTO GenerateResult(int assessmentId, int questionaryId, EnumHelper.PermissionsUser permission);


    }
}
