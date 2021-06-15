using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PERUSTARS.Domain.Models
{
    public class Specialty
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Interest> Interests { get; set; }
        public List<Artist> Artists { get; set; } 
    }
}
