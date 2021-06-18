using PERUSTARS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PERUSTARS.Domain.Persistence.Repositories
{
    public interface IArtworkRepository
    {
        Task<IEnumerable<Artwork>> ListAsync();
        Task AddAsync(Artwork artwork);
        Task<Artwork> FindById(long artworkId);
        Task<IEnumerable<Artwork>> ListByArtistIdAsync(long artistId);
        void Update(Artwork artwork);
        void Remove(Artwork artwork);

        Task<bool> isSameTitle(string title, long ArtistId);

    }
}
