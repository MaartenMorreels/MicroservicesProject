using System.Collections.Generic;
using Assessment.BLL.DTOs;
using EnumHelper = Assessment.BLL.Helper.EnumHelper;

namespace Assessment.BLL.Services.Interfaces
{
    public interface IQuestionAndAnswerOfAssessmentService
    {
        List<QuestionAndAnswerOfAssessmentDTO> AddAnAnswerOfAssessment(QuestionAndAnswerOfAssessmentDTO questionAndAnswerOfAssessmentDto,EnumHelper.PermissionsUser permission);
    }
}
