using System.Collections.Generic;
using Assessment.BLL.DTOs;
using EnumHelper = Assessment.BLL.Helper.EnumHelper;

namespace Assessment.BLL.Services.Interfaces
{
    public interface IAnswerService
    {
        AnswerDTO AddAnswer(AnswerDTO answerDto, EnumHelper.PermissionsUser permission);
        AnswerDTO GetAnswerById(int answerId, EnumHelper.PermissionsUser permission);
        List<AnswerDTO> GetAllAnswersByQuestionId(int questionId, EnumHelper.PermissionsUser permission);
        AnswerDTO UpdateAnswer(AnswerDTO answerDto, EnumHelper.PermissionsUser permission);
    }
}
