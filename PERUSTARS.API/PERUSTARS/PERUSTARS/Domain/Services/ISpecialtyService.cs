using PERUSTARS.Domain.Models;
using PERUSTARS.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PERUSTARS.Domain.Services
{
    public interface ISpecialtyService
    {
        Task<IEnumerable<Specialty>> ListAsync();
        Task<IEnumerable<Specialty>> ListByHobbyistIdAsync(int hobbyistId);
        Task<SpecialtyResponse> GetByIdAsync(long id);       

    }
}
