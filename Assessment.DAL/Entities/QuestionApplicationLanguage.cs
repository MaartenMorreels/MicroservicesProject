using System.ComponentModel.DataAnnotations;

namespace Assessment.DAL.Entities
{
    public class QuestionApplicationLanguage : BaseEnt
    {
        #region Public Properties

        [Required]
        public int QuestionCompositionId { get; set; }

        [Required]
        public int ApplicationLanguageId { get; set; }
        #endregion
    }
}
