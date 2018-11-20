using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Assessment.BLL.Helper;

namespace Assessment.BLL.DTOs
{
    public class QuestionDTO: BaseDTO
    {
        #region Public Properties

        public int AllowedTime { get; set; }

        public int QuestionDifficultyId { get; set; }

        public int QuestionTypeId { get; set; }

        public int QuestionCompositionId { get; set; }

        [Required]
        public int QuestionaryId { get; set; }

        [Required]
        [StringLength(300)]
        public string QuestionPhrase { get; set; }

        public virtual IEnumerable<AnswerDTO> Answers { get; set; }

        public virtual IEnumerable<QuestionAndAnswerOfAssessmentDTO> QuestionAndAnswerOfAssessment { get; set; }

        #endregion Public Properties
    }
}
