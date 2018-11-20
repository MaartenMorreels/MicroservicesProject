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
    public class QuestionaryService : IQuestionaryService
    {
        private IQuestionaryRepo _questionaryRepo;
        private IMapper _mapper;

        public QuestionaryService(IQuestionaryRepo questionaryRepo)
        {
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

        public QuestionaryDTO AddQuestionary(QuestionaryDTO questionaryDto, EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                if (questionaryDto.Description != null)
                {
                    var questionary = _mapper.Map<Questionary>(questionaryDto);
                    var returnValue = _questionaryRepo.Add(questionary);
                    return _mapper.Map<QuestionaryDTO>(returnValue);
                }
            }

            return null;
        }

        public QuestionaryDTO GetQuestionaryById(int questionaryId, EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                var returnValue = _questionaryRepo.GetById(questionaryId);
                return _mapper.Map<QuestionaryDTO>(returnValue);
            }

            return null;
        }

        public List<QuestionaryDTO> GetAllQuestionaries(EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                var list = _questionaryRepo.GetAll().ToList();
                var returnList = new List<QuestionaryDTO>();
                foreach (var questionary in list)
                {
                    returnList.Add(_mapper.Map<QuestionaryDTO>(questionary));
                }

                return returnList;
            }

            return null;
        }

        public QuestionaryDTO UpdateQuestionaries(QuestionaryDTO questionaryDto, EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                var questionary = _mapper.Map<Questionary>(questionaryDto);
                var returnValue = _questionaryRepo.Update(questionary);
                return _mapper.Map<QuestionaryDTO>(returnValue);
            }

            return null;
        }
    }
}
