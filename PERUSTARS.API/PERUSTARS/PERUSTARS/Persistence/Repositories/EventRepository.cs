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
    public class EventRepository : BaseRepository, IEventRepository
    {
        public EventRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Event _event)
        {
            await _context.Events.AddAsync(_event);
        }

        public async Task<Event> FindById(long id)
        {
            return await _context.Events.FindAsync(id);
        }

        public async Task<bool> isSameTitle(string title, long ArtistId)
        {
            bool ver = false;
            IEnumerable<Event> Verification = await ListByArtistIdAsync(ArtistId);
            List<string> titles = new List<string>();

            foreach (Event item in Verification)
            {
                titles.Add(item.EventTitle);
            }

            for (int i = 0; i < titles.Count(); i++)
            {

                if (titles[i] == title)
                {
                    ver = true;
                    return ver;
                }
            }
            ver = false;
            return ver;
        }

        public async Task<IEnumerable<Event>> ListAsync()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<IEnumerable<Event>> ListByArtistIdAsync(long artistId) //falta implementar
        {
            return await _context.Events
                  .Where(ev => ev.ArtistId == artistId)
                  .ToListAsync();
        }

        public async Task<IEnumerable<Event>> ListByEventTypeAsync(ETypeOfEvent typeOfEvent)
        {
            return await _context.Events
                  .Where(ev => ev.EventType == typeOfEvent)
                  .ToListAsync();
        }

        public void Remove(Event _event)
        {
            _context.Events.Remove(_event);
        }

        public void Update(Event _event)
        {
            _context.Events.Update(_event);
        }
    }
}
