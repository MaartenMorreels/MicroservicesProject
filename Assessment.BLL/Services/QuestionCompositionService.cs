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
    public class QuestionCompositionService : IQuestionCompositionService
    {
        private IQuestionCompositionREPO _questionCompositionRepo;
        private IMapper _mapper;

        public QuestionCompositionService(IQuestionCompositionREPO questionCompositionRepo)
        {
            _questionCompositionRepo = questionCompositionRepo;

            var config = new AutoMapper.MapperConfiguration(cfg => { cfg.AddProfile<MapperProfile>(); });

            _mapper = config.CreateMapper();
        }

        public IQuestionCompositionREPO questionCompositionRepo
        {
            get { return _questionCompositionRepo; }
            set { _questionCompositionRepo = value; }
        }


        public QuestionCompositionDTO AddQuestionComposition(QuestionCompositionDTO questionCompositionDTO, EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                var questionComposition = _mapper.Map<QuestionComposition>(questionCompositionDTO);
                var returnValue = questionCompositionRepo.Add(questionComposition);
                return _mapper.Map<QuestionCompositionDTO>(returnValue);
            }

            return null;
        }

        public QuestionCompositionDTO GetQuestionCompositionById(int questionCompositionId, EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                var returnValue = questionCompositionRepo.GetById(questionCompositionId);
                return _mapper.Map<QuestionCompositionDTO>(returnValue);
            }

            return null;
        }

        public List<QuestionCompositionDTO> GetAllQuestionCompositions(EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                var list = questionCompositionRepo.GetAll().ToList();
                var returnList = new List<QuestionCompositionDTO>();
                foreach (var questionary in list)
                {
                    returnList.Add(_mapper.Map<QuestionCompositionDTO>(questionary));
                }

                return returnList;
            }

            return null;
        }

        public List<QuestionCompositionDTO> GetAllQuestionCompositionsByQuestionId(int questionId,EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                var list = questionCompositionRepo.GetAllQuestionCompositionByQuestionId(questionId).ToList();
                var returnList = new List<QuestionCompositionDTO>();
                foreach (var questionary in list)
                {
                    returnList.Add(_mapper.Map<QuestionCompositionDTO>(questionary));
                }

                return returnList;
            }

            return null;
        }

        public QuestionCompositionDTO UpdateQuestionComposition(QuestionCompositionDTO questionCompositionDto, EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                var questionary = _mapper.Map<QuestionComposition>(questionCompositionDto);
                var returnValue = questionCompositionRepo.Update(questionary);
                return _mapper.Map<QuestionCompositionDTO>(returnValue);
            }

            return null;
        }
        
    }
}
