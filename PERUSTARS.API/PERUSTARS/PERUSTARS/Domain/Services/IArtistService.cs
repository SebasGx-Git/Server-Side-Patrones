using PERUSTARS.Domain.Models;
using PERUSTARS.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PERUSTARS.Domain.Services
{
    public interface IArtistService
    {
        Task<IEnumerable<Artist>> ListAsync();
        Task<IEnumerable<Artist>> ListByHobbyistIdAsync(int hobbyistId);
        Task<ArtistResponse> GetByIdAsync(long id);
        Task<ArtistResponse> SaveAsync(Artist artist);
        Task<ArtistResponse> UpdateAsync(long id, Artist artist);
        Task<ArtistResponse> DeleteAsync(long id);
        Task<bool> isSameBrandingName(string brandingname);

    }
}
