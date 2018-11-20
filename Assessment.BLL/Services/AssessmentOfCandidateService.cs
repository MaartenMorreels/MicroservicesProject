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
    public class AssessmentOfCandidateService : IAssessmentOfCandidateService
    {
        private IMapper _mapper;
        private IAssessmentOfCandidateRepo _assessmentOfCandidateREPO;

        public AssessmentOfCandidateService(IAssessmentOfCandidateRepo assessmentOfCandidateREP)
        {
            _assessmentOfCandidateREPO = assessmentOfCandidateREP;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });

            _mapper = config.CreateMapper();
        }

        public IAssessmentOfCandidateRepo assessmentOfCandidateRepo
        {
            get
            {
                return _assessmentOfCandidateREPO;
            }
            set
            {
                _assessmentOfCandidateREPO = value;
            }
        }

        public AssessmentOfCandidateDTO AddAssessmentOfCandidate(AssessmentOfCandidateDTO assessmentOfCandidateDto, EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                if (assessmentOfCandidateDto.AssessmentId != 0 && assessmentOfCandidateDto.CandidateId != 0)
                {
                    var assessmentOfCandidate = _mapper.Map<AssessmentOfCandidate>(assessmentOfCandidateDto);
                    var returnValue = assessmentOfCandidateRepo.Add(assessmentOfCandidate);
                    return _mapper.Map<AssessmentOfCandidateDTO>(returnValue);
                }
            }
            return null;
        }

        public List<AssessmentOfCandidateDTO> GetAllAssessmentOfCandidates(EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                var list = assessmentOfCandidateRepo.GetAll().ToList();
                var returnList = new List<AssessmentOfCandidateDTO>();
                foreach (var assessmentOfCandidate in list)
                {
                    returnList.Add(_mapper.Map<AssessmentOfCandidateDTO>(assessmentOfCandidate));
                }
                return returnList;
            }
            return null;
        }

        public AssessmentOfCandidateDTO GetAssessmentOfCandidateById(int assessmentOfCandidateId, EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                var returnValue = assessmentOfCandidateRepo.GetById(assessmentOfCandidateId);
                return _mapper.Map<AssessmentOfCandidateDTO>(returnValue);
            }
            return null;
        }

        public AssessmentOfCandidateDTO UpdateAssessmentOfCandidate(AssessmentOfCandidateDTO assessmentOfCandidateDto, EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                var assessmentOfCandidate = _mapper.Map<AssessmentOfCandidate>(assessmentOfCandidateDto);
                var returnValue = assessmentOfCandidateRepo.Update(assessmentOfCandidate);
                return _mapper.Map<AssessmentOfCandidateDTO>(returnValue);
            }
            return null;
        }
    }
}
