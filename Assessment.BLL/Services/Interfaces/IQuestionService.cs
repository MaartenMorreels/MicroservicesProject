using System.Collections.Generic;
using Assessment.BLL.DTOs;
using EnumHelper = Assessment.BLL.Helper.EnumHelper;

namespace Assessment.BLL.Services.Interfaces
{
    public interface IQuestionService
    {
        QuestionDTO AddQuestion(QuestionDTO questionDto, EnumHelper.PermissionsUser permission);
        QuestionDTO GetQuestionById(int questionId, EnumHelper.PermissionsUser permission);
        List<QuestionDTO> GetAllQuestionsByQuestionaryId(int questionaryId, EnumHelper.PermissionsUser permission);
        List<QuestionDTO> GetAllQuestions(EnumHelper.PermissionsUser permission);
        QuestionDTO UpdateQuestion(QuestionDTO questionDto, EnumHelper.PermissionsUser permission);
    }
}
