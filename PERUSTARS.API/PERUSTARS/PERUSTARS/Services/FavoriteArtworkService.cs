using PERUSTARS.Domain.Models;
using PERUSTARS.Domain.Persistence.Repositories;
using PERUSTARS.Domain.Services;
using PERUSTARS.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PERUSTARS.Services
{
    public class FavoriteArtworkService : IFavoriteArtworkService
    {
        private readonly IFavoriteArtworkRepository _favoriteArtworkRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FavoriteArtworkService(IFavoriteArtworkRepository favoriteArtworkRepository, IUnitOfWork unitOfWork)
        {
            _favoriteArtworkRepository = favoriteArtworkRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<FavoriteArtworkResponse> AssignFavoriteArtworkAsync(long hobbyistId, long artworkId)
        {
            try {
                await _favoriteArtworkRepository.AssignFavoriteArtwork(hobbyistId, artworkId);
                await _unitOfWork.CompleteAsync();
                FavoriteArtwork favoriteArtwork = await _favoriteArtworkRepository.FindByHobbyistIdAndArtworkId(hobbyistId, artworkId);
                return new FavoriteArtworkResponse(favoriteArtwork);
            }
            catch (Exception ex) { 
            return new FavoriteArtworkResponse($"An error ocurred while assign Artwork to Hobbyist: {ex.Message}");
            }
            
        }

        public async Task<IEnumerable<FavoriteArtwork>> ListAsync()
        {
            return await _favoriteArtworkRepository.ListAsync();
        }

        public async Task<IEnumerable<FavoriteArtwork>> ListByHobbyistIdAsync(long hobbyistId)
        {
            return await _favoriteArtworkRepository.ListByHobbyistIdAsync(hobbyistId);
        }

        public async Task<FavoriteArtworkResponse> UnassignFavoriteArtworkAsync(long hobbyistId, long artworkId)
        {
            try
            {
                FavoriteArtwork favoriteArtwork = await _favoriteArtworkRepository.FindByHobbyistIdAndArtworkId(hobbyistId, artworkId);
                if (favoriteArtwork == null) throw new Exception();

                await _favoriteArtworkRepository.UnassignFavoriteArtwork(hobbyistId,artworkId);
                await _unitOfWork.CompleteAsync();
                return new FavoriteArtworkResponse(favoriteArtwork);

            }
            catch (Exception ex)
            { 
                return new FavoriteArtworkResponse($"An error ocurred while unassign Artwork to Hobbyist: {ex.Message}");
            }
        }
    }
}
