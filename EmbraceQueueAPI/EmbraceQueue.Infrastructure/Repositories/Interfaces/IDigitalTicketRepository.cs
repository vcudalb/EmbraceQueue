using EmbraceQueue.Infrastructure.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmbraceQueue.Infrastructure.Repositories.Interfaces
{
    public interface IDigitalTicketRepository
    {
        Task<IEnumerable<DigitalTicket>> GetDigitalTicketsAsync();
        Task<DigitalTicket> FindDigitalTicketByIdAsync(int id);
        Task<IEnumerable<DigitalTicket>> FindDigitalTicketsByIdAsync(int id);
        Task UpdateDigitalTicketAsync(DigitalTicket digitalTicket);
        Task<DigitalTicket> AddDigitalTicketAsync(DigitalTicket digitalTicket);
        Task DeleteDigitalTicketAsync(int id);
    }
}
