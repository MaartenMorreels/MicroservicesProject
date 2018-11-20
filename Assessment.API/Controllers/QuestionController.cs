using Assessment.BLL.DTOs;
using Assessment.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Assessment.BLL.Helper;


namespace Assessment.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Question")]
    public class QuestionController : Controller
    {
        private IQuestionaryService _questionaryService;
        private IQuestionService _questionService;

        public QuestionController(IQuestionaryService questionaryService, IQuestionService questionService)
        {
            _questionaryService = questionaryService;
            _questionService = questionService;
        }

        [Route("~/api/Question/GetAllQuestionsByQuestionaryId")]
        [HttpGet]
        public IEnumerable<QuestionDTO> GetAllQuestionsByQuestionaryId(int questionaryId)
        {
            var items = _questionService.GetAllQuestionsByQuestionaryId(questionaryId, EnumHelper.PermissionsUser.Admin);
            return items;
        }
        
            [Route("~/api/Question/GetAllQuestions")]
        [HttpGet]
        public IEnumerable<QuestionDTO> GetAllQuestions()
        {
            var items = _questionService.GetAllQuestions(EnumHelper.PermissionsUser.Admin);
            return items;
        }

        [Route("~/api/Questionary/GetQuestionById")]
        [HttpGet]
        public QuestionDTO GetQuestionById(int questionId)
        {
            var item = _questionService.GetQuestionById(questionId, EnumHelper.PermissionsUser.Admin);
            return item;
        }

        [Route("~/api/Question/AddQuestion")]
        [HttpPost]
        public QuestionDTO AddQuestion(QuestionDTO question)
        {
            return _questionService.AddQuestion(question, EnumHelper.PermissionsUser.Admin);
        }

        [Route("~/api/Question/UpdateQuestion")]
        [HttpPost]
        public QuestionDTO UpdateQuestion(QuestionDTO question)
        {
            return _questionService.UpdateQuestion(question, EnumHelper.PermissionsUser.Admin);
        }
    }
}