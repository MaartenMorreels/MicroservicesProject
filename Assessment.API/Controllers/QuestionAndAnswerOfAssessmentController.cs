using System.Collections.Generic;
using Assessment.BLL.DTOs;
using Assessment.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Assessment.BLL.Helper;


namespace Assessment.API.Controllers
{
    [Produces("application/json")]
    [Route("api/QuestionAndAnswerOfAssessment")]
    public class QuestionAndAnswerOfAssessmentController : Controller
    {
        private IQuestionAndAnswerOfAssessmentService _questionAndAnswerOfAssessmentService;

        public QuestionAndAnswerOfAssessmentController(IQuestionAndAnswerOfAssessmentService questionAndAnswerOfAssessmentService)
        {
            _questionAndAnswerOfAssessmentService = questionAndAnswerOfAssessmentService;
        }

        [Route("~/api/QuestionAndAnswerOfAssessment/AddAnswerToAQuestionOfAnAssessment")]
        [HttpGet]
        public List<QuestionAndAnswerOfAssessmentDTO> AddAnswerToAQuestionOfAnAssessment()
        {
            var questionAnAnswerOfAssessment = _questionAndAnswerOfAssessmentService.AddAnAnswerOfAssessment(new QuestionAndAnswerOfAssessmentDTO(),EnumHelper.PermissionsUser.Owner);
            return questionAnAnswerOfAssessment;
        }
    }
}