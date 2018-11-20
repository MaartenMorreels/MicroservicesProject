using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assessment.DAL.Entities
{
    public class Assessment : GdprBaseEnt
    {
        #region Public Properties

        [Required]
        public Guid AssessmentIdentifier { get; set; }

        [StringLength(300)]
        public string Feedback { get; set; }

        public DateTime? AssessmentStart { get; set; }

        public DateTime? AssessmentEnd { get; set; }
        
        public int AssessmentStateId { get; set; }

        public int QuestionComplexityGrade { get; set; }//(dit is voor een curve te maken van hoeveel percentge van de vragen hoger ligt dan de andere)

        public int QuestionDifficultyId { get; set; }

        public int NumberOfQuestions { get; set; }

        public double? AssessmentPercentage { get; set; }

        public virtual IEnumerable<QuestionAndAnswerOfAssessment> ListOfQuestionAndAnswerOfAssessment { get; set; }
        #endregion
    }
}
