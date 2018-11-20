using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Assessment.BLL.DTOs
{
    public class QuestionApplicationLanguageDTO : BaseDTO
    {
        #region Public Properties

        [Required]
        public int QuestionCompositionId { get; set; }

        [Required]
        public int ApplicationLanguageId { get; set; }
        #endregion
    }
}
