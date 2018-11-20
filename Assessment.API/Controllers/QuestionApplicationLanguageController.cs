using System.Collections.Generic;
using Assessment.BLL.DTOs;
using Assessment.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Assessment.BLL.Helper;


namespace Assessment.API.Controllers
{
    [Produces("application/json")]
    [Route("api/QuestionApplicationLanguage")]
    public class QuestionApplicationLanguageController : Controller
    {
        private IQuestionApplicationLanguageService _questionApplicationLanguageService;

        public QuestionApplicationLanguageController(IQuestionApplicationLanguageService questionApplicationLanguageService)
        {
            _questionApplicationLanguageService = questionApplicationLanguageService;
        }

        [Route("~/api/QuestionApplicationLanguage/GetQuestionApplicationLanguageById")]
        [HttpGet]
        public QuestionApplicationLanguageDTO GetQuestionApplicationLanguageById(int id)
        {
            var questionApplicationLanguage = _questionApplicationLanguageService.GetQuestionApplicationLanguageById(id, EnumHelper.PermissionsUser.Admin);
            return questionApplicationLanguage;

        }

        [Route("~/api/QuestionApplicationLanguage/GetAllQuestionApplicationLanguages")]
        [HttpGet]
        public List<QuestionApplicationLanguageDTO> GetAllQuestionApplicationLanguages()
        {
            var questionApplicationLanguages = _questionApplicationLanguageService.GetAllQuestionApplicationLanguages(EnumHelper.PermissionsUser.Admin);
            return questionApplicationLanguages;
        }

        [Route("~/api/QuestionApplicationLanguage/AddQuestionApplicationLanguage")]
        [HttpPost]
        public QuestionApplicationLanguageDTO AddQuestionApplicationLanguage(QuestionApplicationLanguageDTO questionApplicationLanguageDto)
        {
            var questionApplicationLanguage = _questionApplicationLanguageService.AddQuestionApplicationLanguage(questionApplicationLanguageDto, EnumHelper.PermissionsUser.Admin);
            return questionApplicationLanguage;
        }

        [Route("~/api/QuestionApplicationLanguage/UpdateQuestionApplicationLanguage")]
        [HttpPost]
        public QuestionApplicationLanguageDTO UpdateQuestionApplicationLanguage(QuestionApplicationLanguageDTO questionApplicationLanguageDto)
        {
            var questionApplicationLanguage = _questionApplicationLanguageService.UpdateQuestionApplicationLanguage(questionApplicationLanguageDto, EnumHelper.PermissionsUser.Admin);
            return questionApplicationLanguage;
        }
    }
}