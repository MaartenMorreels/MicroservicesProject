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
    public class AssessmentOfEmployeeService : IAssessmentOfEmployeeService
    {
        private IMapper _mapper;
        private IAssessmentOfEmployeeREPO _assessmentOfEmployeeREPO;

        public AssessmentOfEmployeeService(IAssessmentOfEmployeeREPO assessmentOfEmployeeREPO)
        {
            _assessmentOfEmployeeREPO = assessmentOfEmployeeREPO;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });

            _mapper = config.CreateMapper();
        }

        public IAssessmentOfEmployeeREPO assessmentOfEmployeeRepo
        {
            get
            {
                return _assessmentOfEmployeeREPO;
            }
            set
            {
                _assessmentOfEmployeeREPO = value;
            }
        }

        public AssessmentOfEmployeeDTO AddAssessmentOfEmployee(AssessmentOfEmployeeDTO assessmentOfEmployeeDto, EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                if (assessmentOfEmployeeDto.AssessmentId != 0 && assessmentOfEmployeeDto.EmployeeId != 0)
                {
                    var assessmentOfEmployee = _mapper.Map<AssessmentOfEmployee>(assessmentOfEmployeeDto);
                    var returnValue = assessmentOfEmployeeRepo.Add(assessmentOfEmployee);
                    return _mapper.Map<AssessmentOfEmployeeDTO>(returnValue);
                }
            }
            return null;
        }

        public List<AssessmentOfEmployeeDTO> GetAllAssessmentOfEmployees(EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                var list = assessmentOfEmployeeRepo.GetAll().ToList();
                var returnList = new List<AssessmentOfEmployeeDTO>();
                foreach (var assessmentOfEmployee in list)
                {
                    returnList.Add(_mapper.Map<AssessmentOfEmployeeDTO>(assessmentOfEmployee));
                }
                return returnList;
            }
            return null;
        }

        public AssessmentOfEmployeeDTO GetAssessmentOfEmployeeById(int assessmentOfEmployeeId, EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                var returnValue = assessmentOfEmployeeRepo.GetById(assessmentOfEmployeeId);
                return _mapper.Map<AssessmentOfEmployeeDTO>(returnValue);
            }
            return null;
        }

        public AssessmentOfEmployeeDTO UpdateAssessmentOfEmployee(AssessmentOfEmployeeDTO assessmentOfEmployeeDto, EnumHelper.PermissionsUser permission)
        {
            if (permission == EnumHelper.PermissionsUser.Admin)
            {
                var assessmentOfEmployee = _mapper.Map<AssessmentOfEmployee>(assessmentOfEmployeeDto);
                var returnValue = assessmentOfEmployeeRepo.Update(assessmentOfEmployee);
                return _mapper.Map<AssessmentOfEmployeeDTO>(returnValue);
            }
            return null;
        }
    }
}
