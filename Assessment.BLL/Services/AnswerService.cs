using System.Collections.Generic;
using System.Linq;
using Assessment.BLL.DTOs;
using Assessment.BLL.Mapper;
using Assessment.BLL.Services.Interfaces;
using Assessment.DAL.Entities;
using Assessment.DAL.Repositories.Interfaces;
using AutoMapper;
using EnumHelper = Assessment.BLL.Helper.EnumHelper;

namespace Assessment.BLL.Services
{
    public class AnswerService : IAnswerService
    {
        private IMapper _mapper;
        private IQuestionaryRepo _questionaryRepo;
        private IQuestionRepo _questionRepo;
        private IAnswerRepo _answerRepo;

        public AnswerService(IAnswerRepo answerRepo, IQuestionRepo questionRepo, IQuestionaryRepo questionaryRepo)
        {
            _answerRepo = answerRepo;
            _questionRepo = questionRepo;
            _questionaryRepo = questionaryRepo;
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });

            _mapper = config.CreateMapper();
        }
        public IQuestionaryRepo questionaryRepo
        {
            get
            {
                return _questionaryRepo;
            }
            set
            {
                _questionaryRepo = value;
            }
        }
        public IQuestionRepo questionRepo
        {
            get
            {
                return _questionRepo;
            }
            set
            {
                _questionRepo = value;
            }
        }
        public IAnswerRepo answerRepo
        {
            get
            {
                return _answerRepo;
            }
            set
            {
                _answerRepo = value;
            }
        }

        public AnswerDTO AddAnswer(AnswerDTO answerDto, EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                var Question = _questionRepo.GetById(answerDto.QuestionId);
                if (Question != null)
                {
                    var answer = _mapper.Map<Answer>(answerDto);
                    var returnValue = _answerRepo.Add(answer);
                    return _mapper.Map<AnswerDTO>(returnValue);
                }
            }

            return null;
        }

        public AnswerDTO GetAnswerById(int answerId, EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                var returnValue = _answerRepo.GetById(answerId);
                return _mapper.Map<AnswerDTO>(returnValue);
            }

            return null;
        }

        public List<AnswerDTO> GetAllAnswersByQuestionId(int questionId, EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                var resultGetAll =  _answerRepo.GetAll(); 
                var answersForQuestion = resultGetAll.Where(x => x.QuestionId == questionId).ToList();
                var answerDtos = new List<AnswerDTO>();
                foreach (var answer in answersForQuestion)
                {
                    answerDtos.Add(_mapper.Map<AnswerDTO>(answer));
                }
                return answerDtos; 
            }

            return null;
        }

        public AnswerDTO UpdateAnswer(AnswerDTO answerDto, EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                var answer = _mapper.Map<Answer>(answerDto);
                var returnValue = _answerRepo.Update(answer);
                return _mapper.Map<AnswerDTO>(returnValue);
            }

            return null;
        }
    }
}
