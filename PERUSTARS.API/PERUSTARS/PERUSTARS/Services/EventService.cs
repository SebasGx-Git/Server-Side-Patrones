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
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IEventAssistanceRepository _eventAssistanceRepository;
        private readonly IArtistRepository _artistRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EventService(IEventRepository eventRepository, IEventAssistanceRepository eventAsisstanceRepository, IUnitOfWork unitOfWork, IArtistRepository artistRepository)
        {
            _eventRepository = eventRepository;
            _eventAssistanceRepository = eventAsisstanceRepository;
            _unitOfWork = unitOfWork;
            _artistRepository = artistRepository;
        }

        public async Task<EventResponse> DeleteAsync(long eventId, long artistId)
        {
            var existingArtist = await _artistRepository.FindById(artistId);
            if (existingArtist == null)
                return new EventResponse("Artist not found");

            var existingEvent = await _eventRepository.FindById(eventId);
            if (existingEvent == null)
                return new EventResponse("Event not found");

            if (!existingArtist.Events.Contains(existingEvent))
                return new EventResponse("Event not found by Artist with Id: " + artistId);

            try
            {
                _eventRepository.Remove(existingEvent);
                await _unitOfWork.CompleteAsync();

                return new EventResponse(existingEvent);
            }
            catch (Exception ex)
            {
                return new EventResponse($"An error ocurred while deleting the Event: {ex.Message}");
            }
        }

        public async Task<EventResponse> GetByIdAndArtistIdAsync(long eventId, long artistId)
        {
            var existingArtist = await _artistRepository.FindById(artistId);
            if (existingArtist == null)
                return new EventResponse("Artist not found");

            var existingEvent = await _eventRepository.FindById(eventId);
            if (existingEvent == null)
                return new EventResponse("Event not found");

            if (!existingArtist.Events.Contains(existingEvent))
                return new EventResponse("Event not found by Artist with Id: " + artistId);

            return new EventResponse(existingEvent);
        }

        public async Task<bool> isSameTitle(string title, long artistId)
        {
            return await _eventRepository.isSameTitle(title, artistId);

        }

        public async Task<IEnumerable<Event>> ListAsync()
        {
            return await _eventRepository.ListAsync();
        }

        public async Task<IEnumerable<Event>> ListAsyncByArtistId(long artistId)
        {
            return await _eventRepository.ListByArtistIdAsync(artistId);
        }

        public async Task<IEnumerable<Event>> ListAsyncByEventType(ETypeOfEvent eTypeOf)
        {
            return await _eventRepository.ListByEventTypeAsync(eTypeOf);
        }

        //Para event assistance
        public async Task<IEnumerable<Event>> ListByHobbyistAsync(long hobbyistId)
        {
            var eventAssistance = await _eventAssistanceRepository.ListByHobbyistIdAsync(hobbyistId);
            var events = eventAssistance.Select(pt => pt.Event).ToList();
            return events;
        }

        public async Task<EventResponse> SaveAsync(long artistId, Event _event)
        {
            var existingArtist = await _artistRepository.FindById(artistId);
            if (existingArtist == null)
                return new EventResponse("Artist not found");

            _event.ArtistId = artistId;

            if (_eventRepository.isSameTitle(_event.EventTitle, _event.ArtistId).Result == true)
            {
                return new EventResponse($"You already created an event with the same title");
            }

            try
            {
                await _eventRepository.AddAsync(_event);
                await _unitOfWork.CompleteAsync();

                return new EventResponse(_event);
            }
            catch (Exception ex)
            {
                return new EventResponse($"An error ocurred while saving the event: {ex.Message}");
            }
        
        }

        public async Task<EventResponse> UpdateAsync(long eventId, long artistId, Event _event)
        {
            var existingArtist = await _artistRepository.FindById(artistId);
            if (existingArtist == null)
                return new EventResponse("Artist not found");

            var existingEvent = await _eventRepository.FindById(eventId);
            if (existingEvent == null)
                return new EventResponse("Event not found");

            if (!existingArtist.Events.Contains(existingEvent))
                return new EventResponse("Event not found by Artist with Id: " + artistId);

            if (existingEvent.EventTitle != _event.EventTitle)
            {
                if (_eventRepository.isSameTitle(_event.EventTitle, eventId).Result == true)
                {
                    return new EventResponse($"You already created an event with the same title");
                }
            }

            existingEvent.EventTitle = _event.EventTitle;
            existingEvent.EventType = _event.EventType;
            existingEvent.DateStart = _event.DateStart;
            existingEvent.DateEnd = _event.DateEnd;
            existingEvent.EventDescription = _event.EventDescription;
            existingEvent.EventAditionalInfo = _event.EventAditionalInfo;
            existingEvent.ArtistId = artistId;

            try
            {
                _eventRepository.Update(existingEvent);
                await _unitOfWork.CompleteAsync();

                return new EventResponse(existingEvent);
            }
            catch (Exception ex)
            {
                return new EventResponse($"An error ocurred while updating the Event: {ex.Message}");
            }
        }
    }
}
