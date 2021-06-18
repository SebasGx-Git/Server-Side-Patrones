using PERUSTARS.Domain.Models;
using PERUSTARS.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PERUSTARS.Domain.Services
{
    public interface IHobbyistService
    {
        Task<IEnumerable<Hobbyist>> ListAsync();
        Task<IEnumerable<Hobbyist>> ListByArtistIdAsync(long artistId);
        Task<HobbyistResponse> GetByIdAsync(long id);
        Task<HobbyistResponse> SaveAsync(Hobbyist hobbyist);
        Task<HobbyistResponse> UpdateAsync(long id, Hobbyist hobbyist);
        Task<HobbyistResponse> DeleteAsync(long id);
    }
}
