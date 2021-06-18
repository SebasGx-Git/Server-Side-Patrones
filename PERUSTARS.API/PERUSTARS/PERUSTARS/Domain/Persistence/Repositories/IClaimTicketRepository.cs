using PERUSTARS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PERUSTARS.Domain.Persistence.Repositories
{
    public interface IClaimTicketRepository
    {
        Task<IEnumerable<ClaimTicket>> ListAsync();
        Task<IEnumerable<ClaimTicket>> ListByPersonIdAsync(long personId);
        Task<IEnumerable<ClaimTicket>> ListByReportedPersonIdAsync(long personId);
        Task AddAsync(ClaimTicket claimTicket);
        Task<ClaimTicket> FindByIdAndPersonId(long id, long personId);
        void Update(ClaimTicket claimTicket);
        void Remove(ClaimTicket claimTicket);

    }
}
