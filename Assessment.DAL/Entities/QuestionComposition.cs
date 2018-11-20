using System.ComponentModel.DataAnnotations;

namespace Assessment.DAL.Entities
{
    public class QuestionComposition : BaseEnt
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
