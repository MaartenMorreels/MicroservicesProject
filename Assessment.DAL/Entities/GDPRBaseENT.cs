using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Assessment.DAL.Entities
{
    public abstract class GdprBaseEnt : BaseEnt
    {
        public int OwnerId { get; set; }

        //Is Erased

        [Required]
        public bool Erased { get; set; }

        public int? ErasedBy { get; set; }

        public DateTime? ErasedOn { get; set; }
    }
}
