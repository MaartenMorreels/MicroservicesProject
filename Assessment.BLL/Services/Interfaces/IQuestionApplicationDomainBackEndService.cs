using System.Collections.Generic;
using Assessment.BLL.DTOs;
using EnumHelper = Assessment.BLL.Helper.EnumHelper;

namespace Assessment.BLL.Services.Interfaces
{
    public interface IQuestionApplicationDomainBackEndService
    {
        QuestionApplicationDomainBackEndDTO AddQuestionApplicationDomainBackEnd(QuestionApplicationDomainBackEndDTO questionApplicationDomainBackEndDto, EnumHelper.PermissionsUser permission);
        QuestionApplicationDomainBackEndDTO GetQuestionApplicationDomainBackEndById(int questionApplicationDomainBackEndId, EnumHelper.PermissionsUser permission);
        List<QuestionApplicationDomainBackEndDTO> GetAllQuestionApplicationDomainBackEnds(EnumHelper.PermissionsUser permission);
        QuestionApplicationDomainBackEndDTO UpdateQuestionApplicationDomainBackEnd(QuestionApplicationDomainBackEndDTO questionApplicationDomainBackEndDto, EnumHelper.PermissionsUser permission);
    }
}
