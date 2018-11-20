using System.Collections.Generic;
using Assessment.BLL.DTOs;
using EnumHelper = Assessment.BLL.Helper.EnumHelper;

namespace Assessment.BLL.Services.Interfaces
{
    public interface IQuestionApplicationLanguageService
    {
        QuestionApplicationLanguageDTO AddQuestionApplicationLanguage(QuestionApplicationLanguageDTO questionApplicationLanguageDTO, EnumHelper.PermissionsUser permission);
        QuestionApplicationLanguageDTO GetQuestionApplicationLanguageById(int questionApplicationLanguageId, EnumHelper.PermissionsUser permission);
        List<QuestionApplicationLanguageDTO> GetAllQuestionApplicationLanguages(EnumHelper.PermissionsUser permission);
        QuestionApplicationLanguageDTO UpdateQuestionApplicationLanguage(QuestionApplicationLanguageDTO questionApplicationLanguageDto, EnumHelper.PermissionsUser permission);
    }
}
