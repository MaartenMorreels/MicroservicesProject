using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Assessment.BLL.DTOs
{
    public class AssessmentOfCandidateDTO : BaseDTO
    {
        #region Public Properties
        [Required]
        public int AssessmentId { get; set; }

        [Required]
        public int CandidateId { get; set; }
        #endregion
    }
}
