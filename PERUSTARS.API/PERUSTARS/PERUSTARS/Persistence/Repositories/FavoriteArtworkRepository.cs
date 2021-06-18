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
    public class FavoriteArtworkRepository : BaseRepository, IFavoriteArtworkRepository
    {
        public FavoriteArtworkRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(FavoriteArtwork favoriteArtwork)
        {
            await _context.FavoriteArtworks.AddAsync(favoriteArtwork);
        }

        public async Task AssignFavoriteArtwork(long hobbyistId, long artworkId)
        {
            FavoriteArtwork favoriteArtwork = await FindByHobbyistIdAndArtworkId(hobbyistId, artworkId);
            if (favoriteArtwork == null)
            {
                favoriteArtwork = new FavoriteArtwork { HobyyistId = hobbyistId , ArtworkId = artworkId };
                await AddAsync(favoriteArtwork);
            }
        }

        public async Task<FavoriteArtwork>FindByHobbyistIdAndArtworkId(long hobbyistId, long artworkId)
        {
            return await _context.FavoriteArtworks
                .Where(art => art.HobyyistId == hobbyistId && art.ArtworkId == artworkId)
                .Include(art => art.Artwork)
                .FirstOrDefaultAsync();
            //return await _context.FavoriteArtworks.FindAsync(HobbyistId, ArtworkId);
        }

        public async Task<IEnumerable<FavoriteArtwork>> ListAsync()
        {
            return await _context.FavoriteArtworks.ToListAsync();
        }

        public async Task<IEnumerable<FavoriteArtwork>> ListByArtworkIdAsync(long artworkId)
        {
            return await _context.FavoriteArtworks
                 .Where(pt => pt.ArtworkId == artworkId)
                 .Include(pt => pt.Artwork)
                 .Include(pt => pt.Hobbyist)
                 .ToListAsync();
        }

        public async Task<IEnumerable<FavoriteArtwork>> ListByHobbyistIdAsync(long hobbyistId)
        {
            return await _context.FavoriteArtworks
                 .Where(pt => pt.HobyyistId == hobbyistId)
                 .Include(pt => pt.Artwork)
                 .Include(pt => pt.Hobbyist)
                 .ToListAsync();
        }

        public void Remove(FavoriteArtwork favoriteArtwork)
        {
            _context.FavoriteArtworks.Remove(favoriteArtwork);
        }

        public async Task UnassignFavoriteArtwork(long hobbyistId, long artworkId)
        {
            FavoriteArtwork favoriteArtwork = await FindByHobbyistIdAndArtworkId(hobbyistId, artworkId);
            if (favoriteArtwork != null)
                Remove(favoriteArtwork);
        }
    }
}
