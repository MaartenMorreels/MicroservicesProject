using Assessment.BLL.DTOs;
using Assessment.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Assessment.BLL.Helper;


namespace Assessment.API.Controllers
{
    [Produces("application/json")]
    [Route("api/QuestionComposition")]
    public class QuestionCompositionController : Controller
    {
        private IQuestionCompositionService _questionCompositionService;

        public QuestionCompositionController(IQuestionCompositionService questionCompositionService)
        {
            _questionCompositionService = questionCompositionService;
        }

        [Route("~/api/QuestionComposition/AddQuestionComposition")]
        [HttpPost]
        public QuestionCompositionDTO AddQuestionComposition(QuestionCompositionDTO questionComposition)
        {
            return _questionCompositionService.AddQuestionComposition(questionComposition, EnumHelper.PermissionsUser.Admin);
        }
        [Route("~/api/QuestionComposition/GetQuestionCompositionById")]
        [HttpGet]
        public QuestionCompositionDTO GetQuestionCompositionById(int questionCompositionId)
        {
            return _questionCompositionService.GetQuestionCompositionById(questionCompositionId, EnumHelper.PermissionsUser.Admin);
        }
        [Route("~/api/QuestionComposition/GetAllQuestionCompositionsByQuestionId")]
        [HttpGet]
        public List<QuestionCompositionDTO> GetAllQuestionCompositionsByQuestionId(int questionId)
        {
            return _questionCompositionService.GetAllQuestionCompositionsByQuestionId(questionId,EnumHelper.PermissionsUser.Admin);
        }
        [Route("~/api/QuestionComposition/GetAllQuestionCompositions")]
        [HttpGet]
        public List<QuestionCompositionDTO> GetAllQuestionCompositions()
        {
            return _questionCompositionService.GetAllQuestionCompositions(EnumHelper.PermissionsUser.Admin);
        }
        [Route("~/api/QuestionComposition/UpdateQuestionComposition")]
        [HttpPost]
        public QuestionCompositionDTO UpdateQuestionComposition(QuestionCompositionDTO questionComposition)
        {
            return _questionCompositionService.UpdateQuestionComposition(questionComposition,EnumHelper.PermissionsUser.Admin);
        }
    }
}