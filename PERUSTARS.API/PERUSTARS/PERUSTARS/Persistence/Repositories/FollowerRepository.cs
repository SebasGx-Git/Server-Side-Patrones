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
    public class FollowerRepository : BaseRepository, IFollowerRepository
    {
        public FollowerRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Follower follower)
        {
            await _context.Followers.AddAsync(follower);
        }

        public async Task AssignFollower(long hobbyistId, long artistId)
        {
            Follower follower = await FindByHobbyistIdAndArtistId(hobbyistId, artistId);
            if (follower == null)
            {
                follower = new Follower { HobbyistId = hobbyistId, ArtistId = artistId };
                await AddAsync(follower);
            }
        }

        public async Task<int> CountFollower(long artistId)
        {
            return await _context.Followers
               .Where(pt => pt.ArtistId == artistId)
               .Include(pt => pt.Artist)
               .Include(pt => pt.Hobbyist)
               .CountAsync();
               
        }

        public async Task<Follower> FindByHobbyistIdAndArtistId(long hobbyistId, long artistId)
        {
            return await _context.Followers
                .Where(f => f.HobbyistId == hobbyistId && f.ArtistId == artistId)
                .Include(f => f.Artist)
                .FirstOrDefaultAsync();

            //return await _context.Followers.FindAsync(HobbyistId, ArtistId);
        }

        public async Task<IEnumerable<Follower>> ListAsync()
        {
            return await _context.Followers.ToListAsync();
        }

        public async Task<IEnumerable<Follower>> ListByArtistIdAsync(long artistId)
        {
            return await _context.Followers
                .Where(pt => pt.ArtistId == artistId)
                .Include(pt => pt.Artist)
                .Include(pt => pt.Hobbyist)
                .ToListAsync();
        }

        public async Task<IEnumerable<Follower>> ListByHobbyistIdAsync(long hobbyistId)
        {
            return await _context.Followers
               .Where(pt => pt.HobbyistId == hobbyistId)
               .Include(pt => pt.Artist)
               .Include(pt => pt.Hobbyist)
               .ToListAsync();
        }

        public void Remove(Follower follower)
        {
            _context.Followers.Remove(follower);
        }

        public async Task UnassignFollower(long hobbyistId, long artistId)
        {
            Follower follower = await FindByHobbyistIdAndArtistId(hobbyistId, artistId);
            if (follower != null)
                Remove(follower);
        }
    }
}
