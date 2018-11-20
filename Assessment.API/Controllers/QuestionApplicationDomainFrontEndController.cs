using System.Collections.Generic;
using Assessment.BLL.DTOs;
using Assessment.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Assessment.BLL.Helper;


namespace Assessment.API.Controllers
{
    [Produces("application/json")]
    [Route("api/QuestionApplicationDomainFrontEnd")]
    public class QuestionApplicationDomainFrontEndController : Controller
    {
        private IQuestionApplicationDomainFrontEndService _questionApplicationDomainFrontEndService;

        public QuestionApplicationDomainFrontEndController(IQuestionApplicationDomainFrontEndService questionApplicationDomainFrontEndService)
        {
            _questionApplicationDomainFrontEndService = questionApplicationDomainFrontEndService;
        }

        [Route("~/api/QuestionApplicationDomainFrontEnd/GetQuestionApplicationDomainFrontEndById")]
        [HttpGet]
        public QuestionApplicationDomainFrontEndDTO GetQuestionApplicationLanguageById(int id)
        {
            var questionApplicationDomainFrontEnd = _questionApplicationDomainFrontEndService.GetQuestionApplicationDomainFrontEndById(id, EnumHelper.PermissionsUser.Admin);
            return questionApplicationDomainFrontEnd;

        }

        [Route("~/api/QuestionApplicationDomainFrontEnd/GetAllQuestionApplicationDomainFrontEnds")]
        [HttpGet]
        public List<QuestionApplicationDomainFrontEndDTO> GetAllQuestionApplicationDomainFrontEnds()
        {
            var questionApplicationDomainFrontEnds = _questionApplicationDomainFrontEndService.GetAllQuestionApplicationDomainFrontEnds(EnumHelper.PermissionsUser.Admin);
            return questionApplicationDomainFrontEnds;
        }

        [Route("~/api/QuestionApplicationDomainFrontEnd/AddQuestionApplicationDomainFrontEnd")]
        [HttpPost]
        public QuestionApplicationDomainFrontEndDTO AddQuestionApplicationDomainFrontEnd(QuestionApplicationDomainFrontEndDTO questionApplicationDomainFrontEndDto)
        {
            var questionApplicationDomainFrontEnd = _questionApplicationDomainFrontEndService.AddQuestionApplicationDomainFrontEnd(questionApplicationDomainFrontEndDto, EnumHelper.PermissionsUser.Admin);
            return questionApplicationDomainFrontEnd;
        }

        [Route("~/api/QuestionApplicationDomainFrontEnd/UpdateQuestionApplicationDomainFrontEnd")]
        [HttpPost]
        public QuestionApplicationDomainFrontEndDTO UpdateQuestionApplicationDomainFrontEn(QuestionApplicationDomainFrontEndDTO questionApplicationDomainFrontEndDto)
        {
            var questionApplicationDomainFrontEnd = _questionApplicationDomainFrontEndService.UpdateQuestionApplicationDomainFrontEnd(questionApplicationDomainFrontEndDto, EnumHelper.PermissionsUser.Admin);
            return questionApplicationDomainFrontEnd;
        }
    }
}