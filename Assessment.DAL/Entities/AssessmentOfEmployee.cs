using System.ComponentModel.DataAnnotations;

namespace Assessment.DAL.Entities
{
    public class AssessmentOfEmployee : BaseEnt
    {
        #region Public Properties
        [Required]
        public int AssessmentId { get; set; }

        [Required]
        public int EmployeeId { get; set; }
        #endregion
    }
}
