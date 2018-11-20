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
    public class QuestionApplicationDomainFrontEndService : IQuestionApplicationDomainFrontEndService
    {
        private IMapper _mapper;
        private IQuestionApplicationDomainFrontEndRepo _questionApplicationDomainFrontEndRepo;

        public QuestionApplicationDomainFrontEndService(IQuestionApplicationDomainFrontEndRepo questionApplicationDomainFrontEndRepo)
        {
            _questionApplicationDomainFrontEndRepo = questionApplicationDomainFrontEndRepo;
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });

            _mapper = config.CreateMapper();
        }

        public IQuestionApplicationDomainFrontEndRepo QuestionApplicationDomainFrontEndREPO
        {
            get
            {
                return _questionApplicationDomainFrontEndRepo;
            }
            set
            {
                _questionApplicationDomainFrontEndRepo = value;
            }
        }

        public QuestionApplicationDomainFrontEndDTO AddQuestionApplicationDomainFrontEnd(QuestionApplicationDomainFrontEndDTO questionApplicationDomainFrontEndDto, EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                if (questionApplicationDomainFrontEndDto != null 
                    && questionApplicationDomainFrontEndDto.ApplicationDomainFrontEndId != 0 
                    && questionApplicationDomainFrontEndDto.QuestionCompositionId != 0)
                {
                    var questionApplicationDomainFrontEnd = _mapper.Map<QuestionApplicationDomainFrontEnd>(questionApplicationDomainFrontEndDto);
                    var returnValue = QuestionApplicationDomainFrontEndREPO.Add(questionApplicationDomainFrontEnd);
                    return _mapper.Map<QuestionApplicationDomainFrontEndDTO>(returnValue);
                }
            }

            return null;
        }

        public QuestionApplicationDomainFrontEndDTO GetQuestionApplicationDomainFrontEndById(int questionApplicationDomainFrontEndId, EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                var returnValue = QuestionApplicationDomainFrontEndREPO.GetById(questionApplicationDomainFrontEndId);
                return _mapper.Map<QuestionApplicationDomainFrontEndDTO>(returnValue);
            }

            return null;
        }

        public List<QuestionApplicationDomainFrontEndDTO> GetAllQuestionApplicationDomainFrontEnds(EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                var list = QuestionApplicationDomainFrontEndREPO.GetAll().ToList();
                var returnList = new List<QuestionApplicationDomainFrontEndDTO>();
                foreach (var questionApplicationDomainFrontEnd in list)
                {
                    returnList.Add(_mapper.Map<QuestionApplicationDomainFrontEndDTO>(questionApplicationDomainFrontEnd));
                }

                return returnList;
            }

            return null;
        }

        public QuestionApplicationDomainFrontEndDTO UpdateQuestionApplicationDomainFrontEnd(QuestionApplicationDomainFrontEndDTO questionApplicationDomainFrontEndDto, EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                var questionary = _mapper.Map<QuestionApplicationDomainFrontEnd>(questionApplicationDomainFrontEndDto);
                var returnValue = QuestionApplicationDomainFrontEndREPO.Update(questionary);
                return _mapper.Map<QuestionApplicationDomainFrontEndDTO>(returnValue);
            }

            return null;
        }
    }
}
