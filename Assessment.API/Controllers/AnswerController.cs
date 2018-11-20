using System.Collections.Generic;
using Assessment.BLL.DTOs;
using Assessment.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Assessment.BLL.Helper;

namespace Assessment.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Answer")]
    public class AnswerController : Controller
    {
        private IAnswerService _answerService;

        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        [Route("~/api/Answer/GetAnswerById")]
        [HttpGet]
        public AnswerDTO GetAnswerById(int id)
        {
            var answer = _answerService.GetAnswerById(id, EnumHelper.PermissionsUser.Admin);
            return answer;
        }

        [Route("~/api/Answer/GetAllAnswersByQuestionId")]
        [HttpGet]
        public List<AnswerDTO> GetAllAnswersByQuestionId(int id)
        {
            var answers = _answerService.GetAllAnswersByQuestionId(id, EnumHelper.PermissionsUser.Admin);
            return answers;
        }

        [Route("~/api/Answer/AddAnswer")]
        [HttpPost]
        public AnswerDTO AddAnswer(AnswerDTO answerDto)
        {
            return _answerService.AddAnswer(answerDto, EnumHelper.PermissionsUser.Admin);
        }

        [Route("~/api/Answer/UpdateAnswer")]
        [HttpPost]
        public AnswerDTO UpdateAnswer(AnswerDTO answerDto)
        {
            return _answerService.UpdateAnswer(answerDto, EnumHelper.PermissionsUser.Admin);
        }


    }
}