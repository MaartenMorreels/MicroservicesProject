using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assessment.BLL.DTOs
{
    public class QuestionaryDTO : BaseDTO
    {
        #region Public Properties

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        public virtual IEnumerable<QuestionDTO> Questions { get; set; }

        #endregion Public Properties
    }
}
