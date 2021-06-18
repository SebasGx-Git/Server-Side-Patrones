using PERUSTARS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PERUSTARS.Domain.Persistence.Repositories
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> ListAsync();
        Task<IEnumerable<Event>> ListByArtistIdAsync(long artistId);
        Task<IEnumerable<Event>> ListByEventTypeAsync(ETypeOfEvent typeOfEvent);
        Task AddAsync(Event _event);
        Task<Event> FindById(long id);
        void Update(Event _event);
        void Remove(Event _event);
        Task<bool> isSameTitle(string title, long ArtistId);

    }
}
