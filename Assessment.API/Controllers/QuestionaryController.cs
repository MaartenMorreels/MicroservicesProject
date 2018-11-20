using System.Collections.Generic;
using Assessment.BLL.DTOs;
using Assessment.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Assessment.BLL.Helper;


namespace Assessment.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Questionary")]
    public class QuestionaryController : Controller
    {
        private IQuestionaryService _questionaryService;

        public QuestionaryController(IQuestionaryService questionaryService)
        {
            _questionaryService = questionaryService;
        }

        /// <summary>
        /// zhihoehoehoeho
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("~/api/Questionary/GetAllQuestionaries")]
        [HttpGet]
        public IEnumerable<QuestionaryDTO> GetAllQuestionaries()
        {
            var items = _questionaryService.GetAllQuestionaries(EnumHelper.PermissionsUser.Admin);

            return items;
        }

        [Route("~/api/Questionary/GetQuestionaryById")]
        [HttpGet]
        public QuestionaryDTO GetQuestionaryById(int id)
        {
            var item = _questionaryService.GetQuestionaryById(id, EnumHelper.PermissionsUser.Admin);
            return item;
        }

        [Route("~/api/Questionary/AddQuestionary")]
        [HttpPost]
        public QuestionaryDTO AddQuestionary(QuestionaryDTO questionaryDto)
        {
            return _questionaryService.AddQuestionary(questionaryDto, EnumHelper.PermissionsUser.Admin);
        }

        [Route("~/api/Questionary/UpdateQuestionaries")]
        [HttpPost]
        public QuestionaryDTO UpdateQuestionary(QuestionaryDTO questionaryDto)
        {
            return _questionaryService.UpdateQuestionaries(questionaryDto, EnumHelper.PermissionsUser.Admin);
        }
    }

}