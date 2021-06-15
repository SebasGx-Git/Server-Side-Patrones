using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PERUSTARS.Domain.Models
{
    public class Artist : Person
    {
        public string BrandName { get; set; }
        public string Description { get; set; }
        public string Phrase { get; set; }

        public List<string>SocialMediaLink { get; set; }

        public long SpecialtyId { get; set; }
        public Specialty SpecialtyArt { get; set; }

        public IList<Artwork> Artworks { get; set; } = new List<Artwork>();

        public List<Event> Events { get; set; }

        public List<Follower> Followers { get; set; }
              
    }
}
