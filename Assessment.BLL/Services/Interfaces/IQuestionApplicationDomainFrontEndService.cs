using System.Collections.Generic;
using Assessment.BLL.DTOs;
using EnumHelper = Assessment.BLL.Helper.EnumHelper;

namespace Assessment.BLL.Services.Interfaces
{
    public interface IQuestionApplicationDomainFrontEndService
    {
        QuestionApplicationDomainFrontEndDTO AddQuestionApplicationDomainFrontEnd(QuestionApplicationDomainFrontEndDTO questionApplicationDomainFrontEndDto, EnumHelper.PermissionsUser permission);
        QuestionApplicationDomainFrontEndDTO GetQuestionApplicationDomainFrontEndById(int questionApplicationDomainFrontEndId, EnumHelper.PermissionsUser permission);
        List<QuestionApplicationDomainFrontEndDTO> GetAllQuestionApplicationDomainFrontEnds(EnumHelper.PermissionsUser permission);
        QuestionApplicationDomainFrontEndDTO UpdateQuestionApplicationDomainFrontEnd(QuestionApplicationDomainFrontEndDTO questionApplicationDomainFrontEndDto, EnumHelper.PermissionsUser permission);

    }
}
