using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Assessment.DAL.Entities
{
    public class QuestionApplicationDomainBackEnd: BaseEnt
    {
        #region Public Properties
        [Required]
        public int QuestionCompositionId { get; set; }
        [Required]
        public int ApplicationDomainBackEndId { get; set; }
        #endregion
    }
}
