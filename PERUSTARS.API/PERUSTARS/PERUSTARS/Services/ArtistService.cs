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
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IFollowerRepository _followerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ArtistService(IArtistRepository artistRepository, IUnitOfWork unitOfWork, IFollowerRepository followerRepository)
        {
            _artistRepository = artistRepository;
            _unitOfWork = unitOfWork;
            _followerRepository = followerRepository;
        }

        public async Task<ArtistResponse> DeleteAsync(long id)
        {
            var existingArtist = await _artistRepository.FindById(id);

            if (existingArtist == null)
                return new ArtistResponse("Artist not found");

            try
            {
                _artistRepository.Remove(existingArtist);
                await _unitOfWork.CompleteAsync();

                return new ArtistResponse(existingArtist);
            }
            catch (Exception ex)
            {
                return new ArtistResponse($"An error ocurred while deleting the artist: {ex.Message}");
            }
        }

        public async Task<ArtistResponse> GetByIdAsync(long id)
        {
            var existingArtist = await _artistRepository.FindById(id);

            if (existingArtist == null)
                return new ArtistResponse("Artist not found");
            return new ArtistResponse(existingArtist);
        }

        public async Task<bool> isSameBrandingName(string brandingname)
        {
            return await _artistRepository.isSameBrandingName(brandingname);

        }

        public async Task<IEnumerable<Artist>> ListAsync()
        {
            return await _artistRepository.ListAsync();
        }

        public async Task<IEnumerable<Artist>> ListByHobbyistIdAsync(int hobbyistId)
        {
            var follows = await _followerRepository.ListByHobbyistIdAsync(hobbyistId);
            var artists = follows.Select(f => f.Artist).ToList();
            return artists;
        }

        public async Task<ArtistResponse> SaveAsync(Artist artist)
        {
            if (_artistRepository.isSameBrandingName(artist.BrandName).Result == true)
            {
                return new ArtistResponse($"Your Brand Name is already in use");
            }

            try
            {
                await _artistRepository.AddAsync(artist);
                await _unitOfWork.CompleteAsync();

                return new ArtistResponse(artist);
            }
            catch (Exception ex)
            {
                return new ArtistResponse($"An error ocurred while saving the artist: {ex.Message}");
            }
        }

        public async Task<ArtistResponse> UpdateAsync(long id, Artist artist)
        {
            var existingArtist = await _artistRepository.FindById(id);

            if (existingArtist == null)
                return new ArtistResponse("Artist not found");

            if (existingArtist.BrandName != artist.BrandName)
            {
                if (_artistRepository.isSameBrandingName(artist.BrandName).Result == true)
                {
                    return new ArtistResponse($"Your NEW Brand Name is already in use");
                }
            }

            existingArtist.Firstname = artist.Firstname;
            existingArtist.Lastname = artist.Lastname;
            existingArtist.BrandName = artist.BrandName;
            existingArtist.Description = artist.Description;
            existingArtist.Phrase = artist.Phrase;
            existingArtist.SpecialtyId = artist.SpecialtyId;

            try
            {
                _artistRepository.Update(existingArtist);
                await _unitOfWork.CompleteAsync();

                return new ArtistResponse(existingArtist);
            }
            catch (Exception ex)
            {
                return new ArtistResponse($"An error ocurred while updating the artist: {ex.Message}");
            }
        }
    }
}
