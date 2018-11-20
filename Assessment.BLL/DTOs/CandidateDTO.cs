using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Assessment.BLL.DTOs
{
    public class CandidateDTO : GdprBaseDTO
    {
        [Required]
        public int PersonId { get; set; }
        [Required]
        public int AssignedOfficeId { get; set; }
        [StringLength(300)]
        public string ActualPlaceOfEmployment { get; set; }
        [StringLength(300)]
        public string Residence { get; set; }
        [Required]
        public int ProfessionalProfileId { get; set; }
        [Required]
        public int ProfessionalValueId { get; set; }
        [Required]
        public int ReasonOfRejectionId { get; set; }
        public DateTime ReasonOfRejectionDate { get; set; }
        [Required]
        public int ReasonOfEmploymentRefusalId { get; set; }
        [Required]
        public int ReasonOfSalaryPackageRefusalId { get; set; }
        [Required]
        [StringLength(300)]
        public string ReasonOfSalaryPackageRefusalRemark { get; set; }
        [Required]
        public int LastRecruitmentActionId { get; set; }
        [Required]
        public int AssessmentStatusId { get; set; }
        [Required]
        public int NumberOfVoiceMails { get; set; }
        [Required]
        public int NumberOfInterviews { get; set; }
        [Required]
        public int NumberOfEmails { get; set; }
        [Required]
        public int NumberOfTextMessages { get; set; }
        [Required]
        public int NumberOfFailedAppointments { get; set; }
    }
}
