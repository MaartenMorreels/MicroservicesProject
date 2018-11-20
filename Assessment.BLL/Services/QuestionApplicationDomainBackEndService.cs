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
    public class QuestionApplicationDomainBackEndService : IQuestionApplicationDomainBackEndService
    {
        private IMapper _mapper;
        private IQuestionApplicationDomainBackEndRepo _questionApplicationDomainBackEndRepo;

        public QuestionApplicationDomainBackEndService(IQuestionApplicationDomainBackEndRepo questionApplicationDomainBackEndRepo)
        {
            _questionApplicationDomainBackEndRepo = questionApplicationDomainBackEndRepo;
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });

            _mapper = config.CreateMapper();
        }

        public IQuestionApplicationDomainBackEndRepo QuestionApplicationDomainBackEndREPO
        {
            get
            {
                return _questionApplicationDomainBackEndRepo;
            }
            set
            {
                _questionApplicationDomainBackEndRepo = value;
            }
        }

        public QuestionApplicationDomainBackEndDTO AddQuestionApplicationDomainBackEnd(QuestionApplicationDomainBackEndDTO questionApplicationDomainBackEndDto, EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                if (questionApplicationDomainBackEndDto != null
                    && questionApplicationDomainBackEndDto.ApplicationDomainBackEndId != 0
                    && questionApplicationDomainBackEndDto.QuestionCompositionId != 0)
                {
                    var questionApplicationDomainBackEnd = _mapper.Map<QuestionApplicationDomainBackEnd>(questionApplicationDomainBackEndDto);
                    var returnValue = QuestionApplicationDomainBackEndREPO.Add(questionApplicationDomainBackEnd);
                    return _mapper.Map<QuestionApplicationDomainBackEndDTO>(returnValue);
                }
            }

            return null;
        }

        public QuestionApplicationDomainBackEndDTO GetQuestionApplicationDomainBackEndById(int questionApplicationDomainBackEndId, EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                var returnValue = QuestionApplicationDomainBackEndREPO.GetById(questionApplicationDomainBackEndId);
                return _mapper.Map<QuestionApplicationDomainBackEndDTO>(returnValue);
            }

            return null;
        }

        public List<QuestionApplicationDomainBackEndDTO> GetAllQuestionApplicationDomainBackEnds(EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                var list = QuestionApplicationDomainBackEndREPO.GetAll().ToList();
                var returnList = new List<QuestionApplicationDomainBackEndDTO>();
                foreach (var questionApplicationDomainBackEnd in list)
                {
                    returnList.Add(_mapper.Map<QuestionApplicationDomainBackEndDTO>(questionApplicationDomainBackEnd));
                }

                return returnList;
            }

            return null;
        }

        public QuestionApplicationDomainBackEndDTO UpdateQuestionApplicationDomainBackEnd(QuestionApplicationDomainBackEndDTO questionApplicationDomainBackEndDto, EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                var questionary = _mapper.Map<QuestionApplicationDomainBackEnd>(questionApplicationDomainBackEndDto);
                var returnValue = QuestionApplicationDomainBackEndREPO.Update(questionary);
                return _mapper.Map<QuestionApplicationDomainBackEndDTO>(returnValue);
            }

            return null;
        }
    }
}
