using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PERUSTARS.Domain.Models
{
    public class FavoriteArtwork
    {
        public Hobbyist Hobbyist{ get; set; }
        public long HobyyistId { get; set; }

        public Artwork Artwork { get; set; }
        public long ArtworkId { get; set; }

    }
}
