using PERUSTARS.Domain.Persistence.Repositories;
using PERUSTARS.Domain.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PERUSTARS.Domain.Models;

namespace PERUSTARS.Persistence.Repositories
{
    public class HobbyistRepository : BaseRepository, IHobbyistRepository
    {
        public HobbyistRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Hobbyist hobbyist)
        {
            await _context.Hobbyists.AddAsync(hobbyist);
        }

        public async Task<Hobbyist> FindById(long id)
        {
            return await _context.Hobbyists.FindAsync(id);
        }

        public async Task<IEnumerable<Hobbyist>> ListAsync()
        {
            return await _context.Hobbyists.ToListAsync();
        }

        public void Remove(Hobbyist hobbyist)
        {
            _context.Hobbyists.Remove(hobbyist);
        }

        public void Update(Hobbyist hobbyist)
        {
            _context.Hobbyists.Update(hobbyist);
        }
    }
}
