using EmbraceQueue.Domain.Dtos.DigitalTickets;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmbraceQueue.Application.Services.Interfaces
{
    public interface IDigitalTicketService
    {
        Task<IEnumerable<GetDigitalTicketDto>> GetDigitalTicketsAsync();
        Task<GetDigitalTicketDto> FindDigitalTicketByIdAsync(int id);
        Task<IEnumerable<GetDigitalTicketDto>> FindDigitalTicketsByIdAsync(int id);
        Task UpdateDigitalTicketAsync(int id, UpdateDigitalTicketDto digitalTicket);
        Task<GetDigitalTicketDto> AddDigitalTicketAsync(CreateDigitalTicketDto digitalTicket);
        Task DeleteDigitalTicketAsync(int id);
    }
}
