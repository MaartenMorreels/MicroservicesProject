using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Assessment.DAL.Helper;

namespace Assessment.DAL.Entities
{
    public class Question : BaseEnt
    {
        #region Public Properties

        public int AllowedTime { get; set; }

        public int QuestionDifficultyId { get; set; }
        
        public int QuestionTypeId { get; set; }

        [Required]
        public int QuestionaryId { get; set; }

        [Required]
        [StringLength(300)]
        public string QuestionPhrase { get; set; }

        public virtual IEnumerable<Answer> Answers { get; set; }

        public virtual IEnumerable<QuestionAndAnswerOfAssessment> QuestionAndAnswerOfAssessment { get; set; }

        public virtual  IEnumerable<QuestionComposition> QuestionComposition { get; set; }

        #endregion Public Properties

    }
}
