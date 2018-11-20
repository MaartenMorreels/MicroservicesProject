using Assessment.BLL.DTOs;
using Assessment.BLL.Mapper;
using Assessment.BLL.Services.Interfaces;
using Assessment.DAL.Entities;
using Assessment.DAL.Repositories.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using EnumHelper = Assessment.BLL.Helper.EnumHelper;

namespace Assessment.BLL.Services
{
    public class QuestionApplicationLanguageService : IQuestionApplicationLanguageService
    {
        private IMapper _mapper;
        private IQuestionApplicationLanguageREPO _questionApplicationLanguageRepo;

        public QuestionApplicationLanguageService(IQuestionApplicationLanguageREPO questionApplicationLanguageRepo)
        {
            _questionApplicationLanguageRepo = questionApplicationLanguageRepo;
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });

            _mapper = config.CreateMapper();
        }

        public IQuestionApplicationLanguageREPO questionApplicationLanguageRepo
        {
            get
            {
                return _questionApplicationLanguageRepo;
            }
            set
            {
                _questionApplicationLanguageRepo = value;
            }
        }

        public QuestionApplicationLanguageDTO AddQuestionApplicationLanguage(QuestionApplicationLanguageDTO questionApplicationLanguageDto, EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                if (questionApplicationLanguageDto.ApplicationLanguageId != 0 && questionApplicationLanguageDto.QuestionCompositionId != 0)
                {
                    var questionApplicationLanguage = _mapper.Map<QuestionApplicationLanguage>(questionApplicationLanguageDto);
                    var returnValue = questionApplicationLanguageRepo.Add(questionApplicationLanguage);
                    return _mapper.Map<QuestionApplicationLanguageDTO>(returnValue);
                }
            }
            return null;
        }

        public QuestionApplicationLanguageDTO GetQuestionApplicationLanguageById(int questionApplicationLanguageId, EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                var returnValue = questionApplicationLanguageRepo.GetById(questionApplicationLanguageId);
                return _mapper.Map<QuestionApplicationLanguageDTO>(returnValue);
            }
            return null;
        }

        public List<QuestionApplicationLanguageDTO> GetAllQuestionApplicationLanguages(EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                var list = questionApplicationLanguageRepo.GetAll().ToList();
                var returnList = new List<QuestionApplicationLanguageDTO>();
                foreach (var questionApplicationLanguage in list)
                {
                    returnList.Add(_mapper.Map<QuestionApplicationLanguageDTO>(questionApplicationLanguage));
                }

                return returnList;
            }
            return null;
        }

        public QuestionApplicationLanguageDTO UpdateQuestionApplicationLanguage(QuestionApplicationLanguageDTO questionApplicationLanguageDto, EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                var questionApplicationLanguage = _mapper.Map<QuestionApplicationLanguage>(questionApplicationLanguageDto);
                var returnValue = _questionApplicationLanguageRepo.Update(questionApplicationLanguage);
                return _mapper.Map<QuestionApplicationLanguageDTO>(returnValue);
            }
            return null;
        }
    }
}
