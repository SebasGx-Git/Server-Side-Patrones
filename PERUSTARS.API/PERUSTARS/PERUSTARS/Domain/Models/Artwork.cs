using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PERUSTARS.Domain.Models
{
    public class Artwork
    {
        public long ArtworkId { get; set; }
        public string ArtTitle { get; set; }
        public string ArtDescription { get; set; }
        public double ArtCost { get; set; }
        public string LinkInfo { get; set; } 

        //Una obra tiene muchos artistas
        public long ArtistId { get; set; }

        public Artist Artist { get; set; }

        public List<FavoriteArtwork> FavoriteArtworks { get; set; }
    }
}
