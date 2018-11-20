using System.Collections.Generic;
using Assessment.BLL.DTOs;
using EnumHelper = Assessment.BLL.Helper.EnumHelper;

namespace Assessment.BLL.Services.Interfaces
{
    public interface IQuestionCompositionService
    {
        QuestionCompositionDTO AddQuestionComposition(QuestionCompositionDTO questionCompositionDto, EnumHelper.PermissionsUser permission);
        QuestionCompositionDTO GetQuestionCompositionById(int questionCompositionId, EnumHelper.PermissionsUser permission);
        List<QuestionCompositionDTO> GetAllQuestionCompositionsByQuestionId(int questionId, EnumHelper.PermissionsUser permission);
        List<QuestionCompositionDTO> GetAllQuestionCompositions(EnumHelper.PermissionsUser permission);
        QuestionCompositionDTO UpdateQuestionComposition(QuestionCompositionDTO questionCompositionDto, EnumHelper.PermissionsUser permission);
    }
}
