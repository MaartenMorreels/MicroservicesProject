using System;

namespace Assessment.BLL.DTOs
{
    public class QuestionAndAnswerOfAssessmentDTO: GdprBaseDTO
    {
        #region Public Properties

        public int AssessmentId { get; set; }
        public virtual AssessmentDTO Assessment { get; set; }

        public int QuestionId { get; set; }
        public virtual QuestionDTO Question { get; set; }

        public int? AnswerId { get; set; }

        public int QuestionStateId { get; set; }

        public DateTime? ResponseTimeStart { get; set; }

        public DateTime? ResponseTimeEnd { get; set; }


        #endregion Public Properties
    }
}
