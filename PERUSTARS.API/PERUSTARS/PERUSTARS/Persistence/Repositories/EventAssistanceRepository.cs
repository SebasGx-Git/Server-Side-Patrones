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
    public class EventAssistanceRepository : BaseRepository, IEventAssistanceRepository
    {
        public EventAssistanceRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(EventAssistance eventAssistance)
        {           
            await _context.EventAssistances.AddAsync(eventAssistance);
        }

        public async Task AssignEventAssistance(long hobbyistId, long eventId, DateTime attendance)
        {
            EventAssistance eventAssistance = await FindByHobbyistIdAndEventIdAsync(hobbyistId, eventId);
            if (eventAssistance == null)
            {
                eventAssistance = new EventAssistance { HobbyistId = hobbyistId, EventId = eventId  , AttendanceDay = attendance};
                await AddAsync(eventAssistance);
            }

        }

        public async Task<IEnumerable<EventAssistance>> ListAsync()
        {
            return await _context.EventAssistances.ToListAsync();
        }

        public async Task<IEnumerable<EventAssistance>> ListByEventIdAsync(long eventId)
        {
            return await _context.EventAssistances
                   .Where(pt => pt.EventId == eventId)
                   .Include(pt => pt.Event)
                   .Include(pt => pt.Hobbyist)
                   .ToListAsync();
        }

        public async Task<EventAssistance> FindByHobbyistIdAndEventIdAsync(long hobbyistId, long eventId)
        {
            return await _context.EventAssistances
                .Where(ev => ev.EventId == eventId && ev.HobbyistId == hobbyistId)
                .Include(ev => ev.Event)
                .FirstOrDefaultAsync();
            //return await _context.EventAssistances.FindAsync(hobbyistId,eventId);
        }

        public async Task<IEnumerable<EventAssistance>> ListByHobbyistIdAsync(long hobbyistId)
        {
            return await _context.EventAssistances
                 .Where(pt => pt.HobbyistId == hobbyistId)
                 .Include(pt => pt.Event)
                 .Include(pt => pt.Hobbyist)
                 .ToListAsync();
        }

        public void Remove(EventAssistance eventAssistance)
        {
            _context.EventAssistances.Remove(eventAssistance);
        }

        public async Task UnassignEventAssistance(long hobbyistId, long eventId)
        {
            EventAssistance eventAssistance = await FindByHobbyistIdAndEventIdAsync(hobbyistId, eventId);
            if (eventAssistance != null)
                Remove(eventAssistance);
        }
    }
}
