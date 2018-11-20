using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assessment.DAL.Entities
{
    public class Questionary : BaseEnt
    {

        #region Public Properties

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        public virtual IEnumerable<Question> Questions { get; set; }

        #endregion Public Properties
    }  
}
