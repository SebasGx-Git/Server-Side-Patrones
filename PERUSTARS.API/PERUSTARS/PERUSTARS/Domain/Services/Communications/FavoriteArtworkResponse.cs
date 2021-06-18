using PERUSTARS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PERUSTARS.Domain.Services.Communications
{
    public class FavoriteArtworkResponse : BaseResponse<FavoriteArtwork>
    {
        public FavoriteArtworkResponse(FavoriteArtwork resource) : base(resource)
        {
        }

        public FavoriteArtworkResponse(string message) : base(message)
        {
        }
    }
}
