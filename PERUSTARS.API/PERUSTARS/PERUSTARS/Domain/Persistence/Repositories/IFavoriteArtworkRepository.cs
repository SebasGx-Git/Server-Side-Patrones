using PERUSTARS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PERUSTARS.Domain.Persistence.Repositories
{
    public interface IFavoriteArtworkRepository
    {
        Task<IEnumerable<FavoriteArtwork>> ListAsync();
        Task<IEnumerable<FavoriteArtwork>> ListByHobbyistIdAsync(long HobbyistId);
        Task<IEnumerable<FavoriteArtwork>> ListByArtworkIdAsync(long ArtworkId);
        Task<FavoriteArtwork> FindByHobbyistIdAndArtworkId( long HobbyistId, long ArtworkId);
        Task AddAsync(FavoriteArtwork favoriteArtwork);
        void Remove(FavoriteArtwork favoriteArtwork);
        Task AssignFavoriteArtwork(long HobbyistId, long ArtworkId);
        Task UnassignFavoriteArtwork(long HobbyistId, long ArtworkId);

    }
}
