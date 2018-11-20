using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Assessment.BLL.DTOs
{
    public class QuestionApplicationDomainFrontEndDTO : BaseDTO
    {
        #region Public Properties
        [Required]
        public int QuestionCompositionId { get; set; }
        [Required]
        public int ApplicationDomainFrontEndId { get; set; }
        #endregion
    }
}
