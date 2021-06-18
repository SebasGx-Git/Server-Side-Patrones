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
    public class HobbyistService : IHobbyistService
    {
        private readonly IHobbyistRepository _hobbyistRepository;
        private readonly IFollowerRepository _followerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public HobbyistService(IHobbyistRepository hobbyistRepository, IUnitOfWork unitOfWork, IFollowerRepository followerRepository)
        {
            _hobbyistRepository = hobbyistRepository;
            _unitOfWork = unitOfWork;
            _followerRepository = followerRepository;
        }

        public async Task<HobbyistResponse> DeleteAsync(long id)
        {
            var existingHobbyist = await _hobbyistRepository.FindById(id);

            if (existingHobbyist == null)
                return new HobbyistResponse("Hobbyist not found");

            try
            {
                _hobbyistRepository.Remove(existingHobbyist);
                await _unitOfWork.CompleteAsync();

                return new HobbyistResponse(existingHobbyist);
            }
            catch (Exception ex)
            {
                return new HobbyistResponse($"An error ocurred while deleting the hobbyist: {ex.Message}");
            }
        }

        public async Task<HobbyistResponse> GetByIdAsync(long id)
        {
            var existingHobbyist = await _hobbyistRepository.FindById(id);

            if (existingHobbyist == null)
                return new HobbyistResponse("Hobbyist not found");
            return new HobbyistResponse(existingHobbyist);
        }

        public async Task<IEnumerable<Hobbyist>> ListAsync()
        {
            return await _hobbyistRepository.ListAsync();
        }

        public async Task<IEnumerable<Hobbyist>> ListByArtistIdAsync(long artistId)
        {
            var follows = await _followerRepository.ListByArtistIdAsync(artistId);
            var hobbyists = follows.Select(f => f.Hobbyist).ToList();
            return hobbyists;
        }

        public async Task<HobbyistResponse> SaveAsync(Hobbyist hobbyist)
        {
            try
            {
                await _hobbyistRepository.AddAsync(hobbyist);
                await _unitOfWork.CompleteAsync();

                return new HobbyistResponse(hobbyist);
            }
            catch (Exception ex)
            {
                return new HobbyistResponse($"An error ocurred while saving the hobbyist: {ex.Message}");
            }
        }

        public async Task<HobbyistResponse> UpdateAsync(long id, Hobbyist hobbyist)
        {
            var existingHobbyist = await _hobbyistRepository.FindById(id);

            if (existingHobbyist == null)
                return new HobbyistResponse("Artist not found");

            existingHobbyist.Firstname = hobbyist.Firstname;
            existingHobbyist.Lastname = hobbyist.Lastname;

            try
            {
                _hobbyistRepository.Update(existingHobbyist);
                await _unitOfWork.CompleteAsync();

                return new HobbyistResponse(existingHobbyist);
            }
            catch (Exception ex)
            {
                return new HobbyistResponse($"An error ocurred while updating the hobbyist: {ex.Message}");
            }
        }
    }
}
