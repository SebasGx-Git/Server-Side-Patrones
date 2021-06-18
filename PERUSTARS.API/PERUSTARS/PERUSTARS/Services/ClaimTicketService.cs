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
    public class ClaimTicketService : IClaimTicketService
    {
        private readonly IClaimTicketRepository _claimTicketRepository;
        private readonly IArtistRepository _artistRepository;
        private readonly IHobbyistRepository _hobbyistRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ClaimTicketService(IClaimTicketRepository claimTicketRepository, IUnitOfWork unitOfWork, IArtistRepository artistRepository, IHobbyistRepository hobbyistRepository)
        {
            _claimTicketRepository = claimTicketRepository;
            _unitOfWork = unitOfWork;
            _artistRepository = artistRepository;
            _hobbyistRepository = hobbyistRepository;
        }

        public async Task<ClaimTicketResponse> DeleteAsync(long personId, long claimTicketId)
        {
            var existingClaimTicket = await _claimTicketRepository.FindByIdAndPersonId(claimTicketId, personId);

            if (existingClaimTicket == null)
                return new ClaimTicketResponse("Claim Ticket not found");

            try
            {
                _claimTicketRepository.Remove(existingClaimTicket);
                await _unitOfWork.CompleteAsync();

                return new ClaimTicketResponse(existingClaimTicket);
            }
            catch (Exception ex)
            {
                return new ClaimTicketResponse($"An error ocurred while deleting the Claim Ticket: {ex.Message}");
            }
        }

        public async Task<ClaimTicketResponse> GetByIdAndPersonIdAsync(long personId, long claimTicketId)
        {
            var existingArtist = await _artistRepository.FindById(personId);
            var existingHobbyist = await _hobbyistRepository.FindById(personId);
            if (existingArtist == null && existingHobbyist == null)
                return new ClaimTicketResponse("Person not found.");

            var existingClaimTicket = await _claimTicketRepository.FindByIdAndPersonId(claimTicketId, personId);
            if (existingClaimTicket == null)
                return new ClaimTicketResponse("Claim Ticket not found by Person with Id: " + personId);

            return new ClaimTicketResponse(existingClaimTicket);
        }

        public async Task<IEnumerable<ClaimTicket>> ListAsync()
        {
            return await _claimTicketRepository.ListAsync();
        }

        public async Task<IEnumerable<ClaimTicket>> ListAsyncByPersonId(long personId)
        {
            return await _claimTicketRepository.ListByPersonIdAsync(personId);
        }

        public async Task<IEnumerable<ClaimTicket>> ListAsyncByReportedPersonId(long personId)
        {
            return await _claimTicketRepository.ListByReportedPersonIdAsync(personId);
        }

        public async Task<ClaimTicketResponse> SaveAsync(long personId, ClaimTicket claimTicket)
        {
            var existingArtist = await _artistRepository.FindById(personId);
            var existingHobbyist = await _hobbyistRepository.FindById(personId);
            if (existingArtist == null && existingHobbyist == null)
                return new ClaimTicketResponse("Person not found");
            claimTicket.ReportMadeById = personId;
            try
            {
                await _claimTicketRepository.AddAsync(claimTicket);
                await _unitOfWork.CompleteAsync();

                return new ClaimTicketResponse(claimTicket);
            }
            catch (Exception ex)
            {
                return new ClaimTicketResponse($"An error ocurred while saving the Claim Ticket: {ex.Message}");
            }
        }

        public async Task<ClaimTicketResponse> UpdateAsync(long personId, long claimTicketId, ClaimTicket claimTicket)
        {
            var existingClaimTicket = await _claimTicketRepository.FindByIdAndPersonId(claimTicketId, personId);

            if (existingClaimTicket == null)
                return new ClaimTicketResponse("Claim Ticket not found");

            existingClaimTicket.ClaimDescription = claimTicket.ClaimDescription;
            existingClaimTicket.ClaimSubject = claimTicket.ClaimSubject;
            existingClaimTicket.IncedentDate = claimTicket.IncedentDate;
            existingClaimTicket.ReportedPerson = claimTicket.ReportedPerson;

            try
            {
                _claimTicketRepository.Update(existingClaimTicket);
                await _unitOfWork.CompleteAsync();

                return new ClaimTicketResponse(existingClaimTicket);
            }
            catch (Exception ex)
            {
                return new ClaimTicketResponse($"An error ocurred while updating the Claim Ticket: {ex.Message}");
            }
        }
    }
}