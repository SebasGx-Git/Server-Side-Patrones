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
    public class InterestService : IInterestService
    {
        private readonly IInterestRepository _interestRepository;
        private readonly IUnitOfWork _unitOfWork;

        public InterestService(IInterestRepository interestRepository, IUnitOfWork unitOfWork)
        {
            _interestRepository = interestRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<InterestResponse> AssingInterestAsync(long HobbyistId, long SpecialtyId)
        {
            try
            {
                Interest existingInterest = await _interestRepository.FindByHobbyistIdAndSpecialtyId(HobbyistId, SpecialtyId);
                if (existingInterest != null)
                    return new InterestResponse("Hobbyist already has interest in Specialty with id: " + SpecialtyId);
                await _interestRepository.AssignInterest(HobbyistId, SpecialtyId);
                await _unitOfWork.CompleteAsync();
                Interest hobbyistSpecialty = await _interestRepository.FindByHobbyistIdAndSpecialtyId(HobbyistId, SpecialtyId);
                return new InterestResponse(hobbyistSpecialty);
            }
            catch (Exception ex)
            {
                return new InterestResponse($"An error ocurred while assignig Interest: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Interest>> ListAsync()
        {
            return await _interestRepository.ListAsync();
        }

        public async Task<IEnumerable<Interest>> ListByHobbyistsIdAsync(long HobbyistId)
        {
            return await _interestRepository.ListByHobbyistIdAsync(HobbyistId);
        }

        public async Task<InterestResponse> UnassignInterestAsync(long HobbyistId, long SpecialtyId)
        {
            try
            {
                Interest interest = await _interestRepository.FindByHobbyistIdAndSpecialtyId(HobbyistId, SpecialtyId);
                if (interest == null)
                    return new InterestResponse("Hobbyist has no interest in Specialty with id: "+ SpecialtyId);
                await _interestRepository.UnassignInterest(HobbyistId, SpecialtyId);
                await _unitOfWork.CompleteAsync();
                return new InterestResponse(interest);
            }
            catch (Exception ex)
            {
                return new InterestResponse($"An error ocurred while unassignig Interest: {ex.Message}");
            }
        }
    }
}
