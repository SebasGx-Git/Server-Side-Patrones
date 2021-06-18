using PERUSTARS.Domain.Models;
using PERUSTARS.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PERUSTARS.Domain.Services
{
    public interface IArtworkService
    {
        Task<IEnumerable<Artwork>> ListAsync();
        Task<IEnumerable<Artwork>> ListByArtistIdAsync(long Id);
        Task<ArtworkResponse> GetByIdAndArtistIdAsync(long id, long artistId);
        Task<ArtworkResponse> SaveAsync(long artistId, Artwork artwork);
        Task<ArtworkResponse> UpdateAsync(long id, long artistId, Artwork artwork);
        Task<ArtworkResponse> DeleteAsync(long id, long artistId);
        Task<bool> isSameTitle(string title, long ArtistId);
        Task<IEnumerable<Artwork>> ListByHobbyistAsync(long hobbyistId);
    }
}
