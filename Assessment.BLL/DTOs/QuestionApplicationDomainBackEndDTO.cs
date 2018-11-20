using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Assessment.BLL.DTOs
{
    public class QuestionApplicationDomainBackEndDTO : BaseDTO
    {
        #region Public Properties
        [Required]
        public int QuestionCompositionId { get; set; }
        [Required]
        public int ApplicationDomainBackEndId { get; set; }
        #endregion
    }
}
