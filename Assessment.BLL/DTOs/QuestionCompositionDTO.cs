using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Assessment.BLL.DTOs
{
    public class QuestionCompositionDTO : BaseDTO
    {
        #region Public Properties

        [Required]
        public int ApplicationLanguageId { get; set; }
        [Required]
        public int ApplicationFrontEndId { get; set; }
        [Required]
        public int ApplicationBackEndId { get; set; }

        #endregion
    }
}
