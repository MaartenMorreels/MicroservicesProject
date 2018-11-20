using System.Collections.Generic;
using Assessment.BLL.DTOs;
using EnumHelper = Assessment.BLL.Helper.EnumHelper;

namespace Assessment.BLL.Services.Interfaces
{
    public interface IQuestionaryService
    {
        QuestionaryDTO AddQuestionary(QuestionaryDTO questionaryDTO, EnumHelper.PermissionsUser permission);
        QuestionaryDTO GetQuestionaryById(int questionaryId, EnumHelper.PermissionsUser permission);
        List<QuestionaryDTO> GetAllQuestionaries(EnumHelper.PermissionsUser permission);
        QuestionaryDTO UpdateQuestionaries(QuestionaryDTO questionaryDTO, EnumHelper.PermissionsUser permission);
    }
}
