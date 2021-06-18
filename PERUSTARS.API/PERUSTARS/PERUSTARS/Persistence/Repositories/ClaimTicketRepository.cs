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
    public class ClaimTicketRepository: BaseRepository, IClaimTicketRepository
    {
        public ClaimTicketRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<ClaimTicket>> ListAsync()
        {
            return await _context.ClaimTickets.ToListAsync();
        }
        public async Task AddAsync(ClaimTicket claimTicket)
        {
            await _context.ClaimTickets.AddAsync(claimTicket);
        }
        public void Remove(ClaimTicket claimTicket)
        {
            _context.ClaimTickets.Remove(claimTicket);
        }

        public void Update(ClaimTicket claimTicket)
        {
            _context.ClaimTickets.Update(claimTicket);
        }

        public async Task<ClaimTicket> FindByIdAndPersonId(long id, long personId)
        {
            var claimTickets = await _context.ClaimTickets.Include(c => c.ReportMadeBy).ToListAsync();
            return claimTickets.Where(c => c.ClaimId == id && c.ReportMadeById == personId).FirstOrDefault();
        }

        public async Task<IEnumerable<ClaimTicket>> ListByPersonIdAsync(long personId)
        {
            return await _context.ClaimTickets
                  .Where(pt => pt.ReportMadeById == personId)
                  .ToListAsync();
        }

        public async Task<IEnumerable<ClaimTicket>> ListByReportedPersonIdAsync(long personId)
        {
            return await _context.ClaimTickets
                  .Where(pt => pt.ReportedPersonId == personId)
                  .ToListAsync();
        }
    
    }
}
