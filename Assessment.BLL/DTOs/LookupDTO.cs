using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Assessment.BLL.DTOs
{
    public class LookupDTO : GdprBaseDTO
    {
        public int DomainId { get; set; }

        [Required]
        public int DomainItemId { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(50)]
        public string NameEng { get; set; }

        [StringLength(1000)]
        public string DescriptionEng { get; set; }

        [StringLength(50)]
        public string NameNed { get; set; }

        [StringLength(1000)]
        public string DescriptionNed { get; set; }

        public int SortOrder { get; set; }

    }
}
