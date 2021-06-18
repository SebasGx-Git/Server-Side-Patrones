using Microsoft.AspNetCore.Mvc;
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
    public class ArtworkService : IArtworkService
    {
        private readonly IArtworkRepository _artworkRepository;
        private readonly IArtistRepository _artistRepository;
        private readonly IFavoriteArtworkRepository _favoriteArtworkRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ArtworkService(IArtworkRepository artworkRepository, IUnitOfWork unitOfWork, IFavoriteArtworkRepository favoriteArtworkRepository, IArtistRepository artistRepository)
        {
            _artworkRepository = artworkRepository;
            _unitOfWork = unitOfWork;
            _favoriteArtworkRepository = favoriteArtworkRepository;
            _artistRepository = artistRepository;
        }

        public async Task<ArtworkResponse> DeleteAsync(long artworkId, long artistId)
        {
            var existingArtist = await _artistRepository.FindById(artistId);
            if (existingArtist == null)
                return new ArtworkResponse("Artist not found");

            var existingArtwork = await _artworkRepository.FindById(artworkId);
            if (existingArtwork == null)
                return new ArtworkResponse("Artwork not found");

            if (!existingArtist.Artworks.Contains(existingArtwork))
                return new ArtworkResponse("Artwork not found by Artist with Id: " + artistId);

            try
            {
                _artworkRepository.Remove(existingArtwork);
                await _unitOfWork.CompleteAsync();

                return new ArtworkResponse(existingArtwork);
            }
            catch (Exception ex)
            {
                return new ArtworkResponse($"An error ocurred while deleting the artwork: {ex.Message}");
            }
        }

        public async Task<ArtworkResponse> GetByIdAndArtistIdAsync(long artworkId, long artistId)
        {
            var existingArtist = await _artistRepository.FindById(artistId);
            if (existingArtist == null)
                return new ArtworkResponse("Artist not found");

            var existingArtwork = await _artworkRepository.FindById(artworkId);
            if (existingArtwork == null)
                return new ArtworkResponse("Artwork not found");

            if (!existingArtist.Artworks.Contains(existingArtwork))
                return new ArtworkResponse("Artwork not found by Artist with Id: " + artistId);

            return new ArtworkResponse(existingArtwork);
        }

        public async Task<bool> isSameTitle(string title, long ArtistId)
        {
            return await _artworkRepository.isSameTitle(title, ArtistId);
        }

        public async Task<IEnumerable<Artwork>> ListAsync()
        {
            return await _artworkRepository.ListAsync();
        }

        public async Task<IEnumerable<Artwork>> ListByArtistIdAsync(long id)
        {
            return await _artworkRepository.ListByArtistIdAsync(id);
        }

        public async Task<IEnumerable<Artwork>> ListByHobbyistAsync(long hobbyistId)
        {
            var favoriteArtwork = await _favoriteArtworkRepository.ListByHobbyistIdAsync(hobbyistId);
            var artworks = favoriteArtwork.Select(pt => pt.Artwork).ToList();
            return artworks;
        }

        public async Task<ArtworkResponse> SaveAsync(long artistId, Artwork artwork)
        {
            var existingArtist = await _artistRepository.FindById(artistId);
            if (existingArtist == null)
                return new ArtworkResponse("Artist not found");

            artwork.ArtistId = artistId;

            if (_artworkRepository.isSameTitle(artwork.ArtTitle, artwork.ArtistId).Result == true)
            {
                return new ArtworkResponse($"You already created an artwork with the same title");
            }

            try
            {
                await _artworkRepository.AddAsync(artwork);
                await _unitOfWork.CompleteAsync();

                return new ArtworkResponse(artwork);
            }
            catch (Exception ex)
            {
                return new ArtworkResponse($"An error ocurred while saving the artwork: {ex.Message}");
            }
        }

        public async Task<ArtworkResponse> UpdateAsync(long artworkId, long artistId, Artwork artwork)
        {
            var existingArtist = await _artistRepository.FindById(artistId);
            if (existingArtist == null)
                return new ArtworkResponse("Artist not found");

            var existingArtwork = await _artworkRepository.FindById(artworkId);
            if (existingArtwork == null)
                return new ArtworkResponse("Artist not found");

            if (!existingArtist.Artworks.Contains(existingArtwork))
                return new ArtworkResponse("Artwork not found by Artist with Id: " + artistId);

            if (existingArtwork.ArtTitle != artwork.ArtTitle)
            { // si el titulo nuevo es diferente al titulo existente
                if (_artworkRepository.isSameTitle(artwork.ArtTitle, artistId).Result == true) // se verifica si el titulo nuevo es igual a cualquier titulo de obras del artista
                {
                    return new ArtworkResponse($"You already created an artwork with the same title");
                }
            }

            existingArtwork.ArtTitle = artwork.ArtTitle;
            existingArtwork.ArtDescription = artwork.ArtDescription;
            existingArtwork.ArtCost = artwork.ArtCost;
            existingArtwork.LinkInfo = artwork.LinkInfo;
            existingArtwork.ArtistId = artistId;

            try
            {
                _artworkRepository.Update(existingArtwork);
                await _unitOfWork.CompleteAsync();

                return new ArtworkResponse(existingArtwork);
            }
            catch (Exception ex)
            {
                return new ArtworkResponse($"An error ocurred while updating the artwork: {ex.Message}");
            }
        }

    }
}
