
using PERUSTARS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PERUSTARS.Resources
{
    public class ArtistResource : PersonResource
    {
        public string BrandName{ get; set; }

        public string Description { get; set; }

        public string Phrase { get; set; }

        public long SpecialtyId { get; set; }
    }
}
