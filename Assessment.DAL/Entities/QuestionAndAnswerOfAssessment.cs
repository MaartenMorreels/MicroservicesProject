using System;
using System.ComponentModel.DataAnnotations;

namespace Assessment.DAL.Entities
{
    public class QuestionAndAnswerOfAssessment : GdprBaseEnt
    {
        #region Public Properties

        [Required]
        public int AssessmentId { get; set; }
        public virtual Assessment Assessment { get; set; }

        [Required]
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }

        public int? AnswerId { get; set; }

        public int QuestionStateId { get; set; }

        public DateTime? ResponseTimeStart { get; set; }

        public DateTime? ResponseTimeEnd { get; set; }


        #endregion Public Properties
    }
}
