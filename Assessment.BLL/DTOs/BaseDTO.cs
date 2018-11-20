using System;
using System.ComponentModel.DataAnnotations;

namespace Assessment.BLL.DTOs
{
    public class BaseDTO
    {
        #region Public Properties

        public int Id { get; set; }

        //Created
        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        //Updated
        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        //Is Archived
        [Required]
        public bool Archived { get; set; }

        public int? ArchivedBy { get; set; }

        public DateTime? ArchivedOn { get; set; }

        ////Is Deleted
        [Required]
        public bool Deleted { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedOn { get; set; }

        #endregion Public Properties
    }
}
