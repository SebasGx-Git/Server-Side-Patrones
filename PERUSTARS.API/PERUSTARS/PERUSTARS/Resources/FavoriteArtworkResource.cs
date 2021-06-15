using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PERUSTARS.Resources
{
    public class FavoriteArtworkResource
    {
        public HobbyistResource Hobbyist { get; set; }
        public long HobyyistId { get; set; }

        public ArtworkResource Artwork { get; set; }
        public long ArtworkId { get; set; }
    }
}
