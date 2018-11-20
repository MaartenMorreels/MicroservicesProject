using System.ComponentModel.DataAnnotations;

namespace Assessment.BLL.DTOs
{
    public class AnswerDTO: BaseDTO
    {
        #region Public Properties

        [Required]
        public bool Correct { get; set; }

        [Required]
        public int QuestionId { get; set; }

        [Required]
        [StringLength(300)]
        public string Text { get; set; }

        #endregion Public Properties
    }
}
