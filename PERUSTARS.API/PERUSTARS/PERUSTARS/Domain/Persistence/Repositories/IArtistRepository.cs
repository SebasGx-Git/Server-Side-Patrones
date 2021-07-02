using PERUSTARS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PERUSTARS.Domain.Persistence.Repositories
{
    public interface IArtistRepository
    {
        Task<IEnumerable<Artist>> ListAsync();
        Task AddAsync(Artist artist);
        Task<Artist> FindById(long id);
        void Remove(Artist artist);
        void Update(Artist artist);

        Task<bool> isSameBrandingName(string brandingname);

    }
}
