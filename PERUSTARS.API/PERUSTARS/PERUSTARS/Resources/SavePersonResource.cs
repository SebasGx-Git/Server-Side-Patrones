using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PERUSTARS.Resources
{
    public class SavePersonResource
    {
        [Required]
        [MaxLength(20)]
        public string Firstname { get; set; }

        [Required]
        [MaxLength(20)]
        public string Lastname { get; set; }
    }
}
