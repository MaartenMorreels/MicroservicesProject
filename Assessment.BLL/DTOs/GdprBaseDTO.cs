using System;
using System.ComponentModel.DataAnnotations;

namespace Assessment.BLL.DTOs
{
    public class GdprBaseDTO : BaseDTO
    {
        #region Public Properties

        public int OwnerId { get; set; }

        //Is Erased
        [Required]
        public bool Erased { get; set; }

        public int? ErasedBy { get; set; }

        public DateTimeOffset? ErasedOn { get; set; }

        #endregion Public Properties
    }
}
