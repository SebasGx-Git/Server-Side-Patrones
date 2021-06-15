using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; 


namespace PERUSTARS.Resources
{
    public class SaveArtistResource : SavePersonResource
    {

        [Required]
        [MaxLength(30)]
        public string BrandName { get; set; }


        [Required]
        [MaxLength(250)]
        public string Description { get; set; }


        [Required]
        [MaxLength(100)]
        public string Phrase { get; set; }


        [Required]
        public long SpecialtyId { get; set; }

    }
}
