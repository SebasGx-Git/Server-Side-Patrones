
using PERUSTARS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PERUSTARS.Domain.Persistence.Repositories
{
    public interface IEventAssistanceRepository
    {
        Task<IEnumerable<EventAssistance>> ListAsync();
        Task AddAsync(EventAssistance eventAssistance);
        Task<IEnumerable<EventAssistance>> ListByEventIdAsync(long eventId);
        Task<IEnumerable<EventAssistance>> ListByHobbyistIdAsync(long hobbyistId);

        Task<EventAssistance> FindByHobbyistIdAndEventIdAsync(long hobbyistId, long eventId);

        Task AssignEventAssistance(long hobbyistId, long eventId, DateTime attendance);
        Task UnassignEventAssistance(long hobbyistId, long eventId);

        void Remove(EventAssistance eventAssistance);
    }
}
