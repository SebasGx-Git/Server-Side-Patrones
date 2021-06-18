using PERUSTARS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PERUSTARS.Domain.Services.Communications
{
    public class ArtistResponse : BaseResponse<Artist>
    {
        public ArtistResponse(Artist resource) : base(resource)
        {
        }

        public ArtistResponse(string message) : base(message)
        {
        }
    }
}
