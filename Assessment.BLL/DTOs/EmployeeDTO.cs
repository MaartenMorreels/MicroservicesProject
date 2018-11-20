using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Assessment.BLL.DTOs
{
    public class CEmployeeDTO : GdprBaseDTO
    {
        [Required]
        public int PersonId { get; set; }
    }
}
