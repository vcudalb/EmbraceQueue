using EmbraceQueue.Infrastructure.Entities;
using EmbraceQueue.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmbraceQueue.Infrastructure.Repositories
{
    public class DigitalTicketRepository : IDigitalTicketRepository
    {
        private readonly EmbraceQueueDbContext _dbContext;
        public DigitalTicketRepository(EmbraceQueueDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<DigitalTicket>> GetDigitalTicketsAsync()
        {
            return await _dbContext.DigitalTickets.AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        public async Task<DigitalTicket> FindDigitalTicketByIdAsync(int id)
        {
            return await _dbContext.DigitalTickets.AsNoTracking().FirstOrDefaultAsync(dt => dt.Id == id).ConfigureAwait(false);
        }

        public async Task<IEnumerable<DigitalTicket>> FindDigitalTicketsByIdAsync(int id)
        {
            return await _dbContext.DigitalTickets.AsNoTracking().Where(dt => dt.Id == id).ToListAsync().ConfigureAwait(false);
        }

        public async Task UpdateDigitalTicketAsync(DigitalTicket digitalTicket)
        {
            var existingDigitalTicket = await _dbContext.DigitalTickets.FirstOrDefaultAsync(s => s.Id == digitalTicket.Id).ConfigureAwait(false);

            if (existingDigitalTicket.CompanyId != digitalTicket.CompanyId && digitalTicket.CompanyId > 0) existingDigitalTicket.CompanyId = digitalTicket.CompanyId;
            if (existingDigitalTicket.NotificationsNumber != digitalTicket.NotificationsNumber && digitalTicket.NotificationsNumber > 0) existingDigitalTicket.NotificationsNumber = digitalTicket.NotificationsNumber;
            if (existingDigitalTicket.MessageTemplateOne != digitalTicket.MessageTemplateOne && !string.IsNullOrEmpty(digitalTicket.MessageTemplateOne)) existingDigitalTicket.MessageTemplateOne = digitalTicket.MessageTemplateOne;
            if (existingDigitalTicket.MessageTemplateTwo != digitalTicket.MessageTemplateTwo && !string.IsNullOrEmpty(digitalTicket.MessageTemplateTwo)) existingDigitalTicket.MessageTemplateTwo = digitalTicket.MessageTemplateTwo;

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<DigitalTicket> AddDigitalTicketAsync(DigitalTicket digitalTicket)
        {
            await _dbContext.AddAsync(digitalTicket).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return digitalTicket;
        }

        public async Task DeleteDigitalTicketAsync(int id)
        {
            var existingDigitalTicket = await _dbContext.DigitalTickets.FirstOrDefaultAsync(s => s.Id == id).ConfigureAwait(false);
            _dbContext.DigitalTickets.Remove(existingDigitalTicket);

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
