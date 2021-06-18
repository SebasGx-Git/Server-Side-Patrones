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
    public class EventAssistanceService : IEventAssistanceService
    {
        private readonly IEventAssistanceRepository _eventAssistanceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EventAssistanceService(IEventAssistanceRepository eventAssistanceRepository, IUnitOfWork unitOfWork)
        {
            _eventAssistanceRepository = eventAssistanceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<EventAssistanceResponse> AssignEventAssistanceAsync(long hobbyistId, long eventId, DateTime attendance)
        {
            try
            {
                await _eventAssistanceRepository.AssignEventAssistance(hobbyistId, eventId, attendance);
                await _unitOfWork.CompleteAsync();
                EventAssistance eventAssistance = await _eventAssistanceRepository.FindByHobbyistIdAndEventIdAsync(hobbyistId, eventId);
                return new EventAssistanceResponse(eventAssistance);
            }
            catch (Exception ex)
            {
                return new EventAssistanceResponse($"An error ocurred while assigning a EventAssistance: {ex.Message}");
            }
        }

        public async Task<IEnumerable<EventAssistance>> ListAsync()
        {
            return await _eventAssistanceRepository.ListAsync();
        }

        public async Task<IEnumerable<EventAssistance>> ListAsyncByHobbyistId(long hobbyistId)
        {
            return await _eventAssistanceRepository.ListByHobbyistIdAsync(hobbyistId);
        }

        public async Task<EventAssistanceResponse> UnassignEventAssistanceAsync(long hobbyistId, long eventId)
        {
            try
            {
                EventAssistance eventAssistance = await _eventAssistanceRepository.FindByHobbyistIdAndEventIdAsync(hobbyistId, eventId);
                if (eventAssistance == null) throw new Exception();

                await _eventAssistanceRepository.UnassignEventAssistance(hobbyistId, eventId);
                await _unitOfWork.CompleteAsync();
                return new EventAssistanceResponse(eventAssistance);
            }
            catch (Exception ex)
            {
                return new EventAssistanceResponse($"An error ocurred while unassigning a EventAssistance: {ex.Message}");
            }
        }
    }
}