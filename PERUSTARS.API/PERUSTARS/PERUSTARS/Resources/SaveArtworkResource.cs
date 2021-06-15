using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PERUSTARS.Resources
{

    public class SaveArtworkResource
    {
        [Required]
        [MaxLength(100)]
        public string ArtTitle { get; set; }

        [Required]
        [MaxLength(250)]
        public string ArtDescription { get; set; }

        public double ArtCost { get; set; }
        public string LinkInfo { get; set; }


    }
}
