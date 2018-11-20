using System.Collections.Generic;
using Assessment.BLL.DTOs;
using Assessment.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Assessment.BLL.Helper;


namespace Assessment.API.Controllers
{
    [Produces("application/json")]
    [Route("api/QuestionApplicationDomainBackEnd")]
    public class QuestionApplicationDomainBackEndController : Controller
    {
        private IQuestionApplicationDomainBackEndService _questionApplicationDomainBackEndService;

        public QuestionApplicationDomainBackEndController(IQuestionApplicationDomainBackEndService questionApplicationDomainBackEndService)
        {
            _questionApplicationDomainBackEndService = questionApplicationDomainBackEndService;
        }

        [Route("~/api/QuestionApplicationDomainBackEnd/GetQuestionApplicationDomainBackEndById")]
        [HttpGet]
        public QuestionApplicationDomainBackEndDTO GetQuestionApplicationLanguageById(int id)
        {
            var questionApplicationDomainBackEnd = _questionApplicationDomainBackEndService.GetQuestionApplicationDomainBackEndById(id, EnumHelper.PermissionsUser.Admin);
            return questionApplicationDomainBackEnd;

        }

        [Route("~/api/QuestionApplicationDomainBackEnd/GetAllQuestionApplicationDomainBackEnds")]
        [HttpGet]
        public List<QuestionApplicationDomainBackEndDTO> GetAllQuestionApplicationDomainBackEnds()
        {
            var questionApplicationDomainBackEnds = _questionApplicationDomainBackEndService.GetAllQuestionApplicationDomainBackEnds(EnumHelper.PermissionsUser.Admin);
            return questionApplicationDomainBackEnds;
        }

        [Route("~/api/QuestionApplicationDomainBackEnd/AddQuestionApplicationDomainBackEnd")]
        [HttpPost]
        public QuestionApplicationDomainBackEndDTO AddQuestionApplicationDomainBackEnd(QuestionApplicationDomainBackEndDTO questionApplicationDomainBackEndDto)
        {
            var questionApplicationDomainBackEnd = _questionApplicationDomainBackEndService.AddQuestionApplicationDomainBackEnd(questionApplicationDomainBackEndDto, EnumHelper.PermissionsUser.Admin);
            return questionApplicationDomainBackEnd;
        }

        [Route("~/api/QuestionApplicationDomainBackEnd/UpdateQuestionApplicationDomainBackEnd")]
        [HttpPost]
        public QuestionApplicationDomainBackEndDTO UpdateQuestionApplicationDomainBackEn(QuestionApplicationDomainBackEndDTO questionApplicationDomainBackEndDto)
        {
            var questionApplicationDomainBackEnd = _questionApplicationDomainBackEndService.UpdateQuestionApplicationDomainBackEnd(questionApplicationDomainBackEndDto, EnumHelper.PermissionsUser.Admin);
            return questionApplicationDomainBackEnd;
        }
    }
}