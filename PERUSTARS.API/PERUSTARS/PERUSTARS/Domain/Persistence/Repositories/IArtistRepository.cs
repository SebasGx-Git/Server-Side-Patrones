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
        void Update(Artist artist);
        void Remove(Artist artist);

        Task<bool> isSameBrandingName(string brandingname);

    }
}
