using System.ComponentModel.DataAnnotations;

namespace Assessment.DAL.Entities
{
    public class AssessmentOfCandidate : BaseEnt
    {
        #region Public Properties
        [Required]
        public int AssessmentId { get; set; }

        [Required]
        public int CandidateId { get; set; }
        #endregion
    }
}
