using System.ComponentModel.DataAnnotations;

namespace Assessment.DAL.Entities
{
    public class QuestionApplicationDomainFrontEnd : BaseEnt
    {
        #region Public Properties
        [Required]
        public int QuestionCompositionId { get; set; }
        [Required]
        public int ApplicationDomainFrontEndId { get; set; }
        #endregion
    }
}
