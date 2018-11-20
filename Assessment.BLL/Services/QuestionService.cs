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
    public class QuestionService : IQuestionService
    {
        private IQuestionaryRepo _questionaryRepo;
        private IQuestionRepo _questionRepo;
        private IMapper _mapper;

        public QuestionService(IQuestionaryRepo questionaryRepo, IQuestionRepo questionRepo)
        {
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

        public QuestionDTO AddQuestion(QuestionDTO questionDto,EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                var questionary = _questionaryRepo.GetById(questionDto.QuestionaryId);
                if (questionary != null)
                {
                    var question = _mapper.Map<Question>(questionDto);
                    var returnValue = _questionRepo.Add(question);
                    return _mapper.Map<QuestionDTO>(returnValue);
                }
            }

            return null;
        }

        public QuestionDTO GetQuestionById( int questionId, EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                var returnValue = _questionRepo.GetById(questionId);
                return _mapper.Map<QuestionDTO>(returnValue);
            }
            return null;
        }

        public List<QuestionDTO> GetAllQuestionsByQuestionaryId( int questionaryId, EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                var list = _questionRepo.GetAllQuestionsByQuestionaryId(questionaryId).ToList();
                var returnList = new List<QuestionDTO>();
                foreach(var question in list)
                {
                    returnList.Add(_mapper.Map<QuestionDTO>(question));
                }
                return returnList;

            }

            return null;
        }

        public QuestionDTO UpdateQuestion(QuestionDTO questionDto, EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                var question = _mapper.Map<Question>(questionDto);
                var returnValue = _questionRepo.Update(question);
                return _mapper.Map<QuestionDTO>(returnValue);
            }

            return null;
        }

        public List<QuestionDTO> GetAllQuestions(EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                var list = _questionRepo.GetAll().ToList();
                var returnList = new List<QuestionDTO>();
                foreach (var question in list)
                {
                    returnList.Add(_mapper.Map<QuestionDTO>(question));
                }
                return returnList;

            }
            return null;
        }
    }
}
