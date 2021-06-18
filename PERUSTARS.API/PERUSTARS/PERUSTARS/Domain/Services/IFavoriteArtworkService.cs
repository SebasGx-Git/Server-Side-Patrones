using PERUSTARS.Domain.Models;
using PERUSTARS.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PERUSTARS.Domain.Services
{
    public interface IFavoriteArtworkService
    {
        Task<IEnumerable<FavoriteArtwork>> ListAsync();
        Task<IEnumerable<FavoriteArtwork>> ListByHobbyistIdAsync(long Id); 
        Task<FavoriteArtworkResponse> AssignFavoriteArtworkAsync(long HobbyistId, long ArtworkId);
        Task<FavoriteArtworkResponse> UnassignFavoriteArtworkAsync(long HobbyistId, long ArtworkId);
    }
}
