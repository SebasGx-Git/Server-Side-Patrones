using PERUSTARS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PERUSTARS.Domain.Persistence.Repositories
{
    public interface IHobbyistRepository
    {
        Task<IEnumerable<Hobbyist>> ListAsync();
        Task AddAsync(Hobbyist hobbyist);
        Task<Hobbyist> FindById(long id);
        void Update(Hobbyist hobbyist);
        void Remove(Hobbyist hobbyist);
    }
}
