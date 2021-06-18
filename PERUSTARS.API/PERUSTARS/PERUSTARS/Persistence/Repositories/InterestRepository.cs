using Microsoft.EntityFrameworkCore;
using PERUSTARS.Domain.Models;
using PERUSTARS.Domain.Persistence.Contexts;
using PERUSTARS.Domain.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PERUSTARS.Persistence.Repositories
{
    public class InterestRepository : BaseRepository, IInterestRepository
    {
        public InterestRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Interest interest)
        {
            await _context.Interests.AddAsync(interest);
        }

        public async Task AssignInterest(long hobbyistId, long specialtyId)
        {
            Interest hobbyistSpecialty = await FindByHobbyistIdAndSpecialtyId(hobbyistId, specialtyId);
            if (hobbyistSpecialty == null)
            {
                hobbyistSpecialty = new Interest { HobbyistId = hobbyistId, SpecialtyId = specialtyId };
                await AddAsync(hobbyistSpecialty);
            }
        }

        public async Task<Interest> FindByHobbyistIdAndSpecialtyId(long hobbyistId, long specialtyId)
        {
            return await _context.Interests
                .Where(i => i.HobbyistId == hobbyistId && i.SpecialtyId == specialtyId)
                .Include(i => i.Specialty)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Interest>> ListAsync()
        {
            return await _context.Interests
                .Include(i => i.Hobbyist)
                .Include(i => i.Specialty)
                .ToListAsync();
        }

        public async Task<IEnumerable<Interest>> ListByHobbyistIdAsync(long hobbyistId)
        {
            return await _context.Interests
                .Where(i => i.HobbyistId == hobbyistId)
                .Include(i => i.Hobbyist)
                .Include(i => i.Specialty)
                .ToListAsync();
        }

        public void Remove(Interest interest)
        {
            _context.Interests.Remove(interest);
        }

        public async Task UnassignInterest(long hobbyistId, long specialtyId)
        {
            Interest interest = await FindByHobbyistIdAndSpecialtyId(hobbyistId, specialtyId);
            if (interest != null)
                Remove(interest);

        }
    }
}
