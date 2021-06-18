using PERUSTARS.Domain.Models;
using PERUSTARS.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PERUSTARS.Domain.Services
{
    public interface IEventAssistanceService
    {
        Task<IEnumerable<EventAssistance>> ListAsync();


        Task<IEnumerable<EventAssistance>> ListAsyncByHobbyistId(long Id);

        Task<EventAssistanceResponse> AssignEventAssistanceAsync(long HobbyistId, long EventId, DateTime attendance);
        Task<EventAssistanceResponse> UnassignEventAssistanceAsync(long HobbyistId, long EventId);
    }
}
