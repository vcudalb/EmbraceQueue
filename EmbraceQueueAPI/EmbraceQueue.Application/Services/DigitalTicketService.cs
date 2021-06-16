using EmbraceQueue.Application.Services.Interfaces;
using EmbraceQueue.Domain.Dtos.DigitalTickets;
using EmbraceQueue.Infrastructure.Entities;
using EmbraceQueue.Infrastructure.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmbraceQueue.Application.Services
{
    public class DigitalTicketService : IDigitalTicketService
    {
        private readonly IDigitalTicketRepository _digitalTicketRepository;

        public DigitalTicketService(IDigitalTicketRepository digitalTicketRepository)
        {
            _digitalTicketRepository = digitalTicketRepository;
        }

        public async Task<IEnumerable<GetDigitalTicketDto>> GetDigitalTicketsAsync()
        {
            var digitalTickets = await _digitalTicketRepository.GetDigitalTicketsAsync().ConfigureAwait(false);
            return digitalTickets.Select(x => Map(x));
        }

        public async Task<GetDigitalTicketDto> FindDigitalTicketByIdAsync(int id)
        {
            var digitalTicket = await _digitalTicketRepository.FindDigitalTicketByIdAsync(id).ConfigureAwait(false);
            if (digitalTicket != null) return Map(digitalTicket);
            return null;
        }

        public async Task<IEnumerable<GetDigitalTicketDto>> FindDigitalTicketsByIdAsync(int id)
        {
            var digitalTickets = await _digitalTicketRepository.FindDigitalTicketsByIdAsync(id).ConfigureAwait(false);
            return digitalTickets.Select(x => Map(x));
        }

        public async Task UpdateDigitalTicketAsync(int id, UpdateDigitalTicketDto digitalTicket)
        {
            await _digitalTicketRepository.UpdateDigitalTicketAsync(Map(id, digitalTicket)).ConfigureAwait(false);
        }

        public async Task<GetDigitalTicketDto> AddDigitalTicketAsync(CreateDigitalTicketDto digitalTicket)
        {
            var createdDigitalTicket = await _digitalTicketRepository.AddDigitalTicketAsync(Map(digitalTicket)).ConfigureAwait(false);
            return Map(createdDigitalTicket);
        }

        public async Task DeleteDigitalTicketAsync(int id)
        {
            await _digitalTicketRepository.DeleteDigitalTicketAsync(id).ConfigureAwait(false);
        }

        private static GetDigitalTicketDto Map(DigitalTicket digitalTicket) => new GetDigitalTicketDto
        {
            Id = digitalTicket.Id,
            CompanyId = digitalTicket.CompanyId,
            MessageTemplateOne = digitalTicket.MessageTemplateOne,
            MessageTemplateTwo = digitalTicket.MessageTemplateTwo,
            NotificationsNumber = digitalTicket.NotificationsNumber
        };

        private static DigitalTicket Map(int id, UpdateDigitalTicketDto updateDigitalTicketDto) => new DigitalTicket
        {
            Id = id,
            CompanyId = updateDigitalTicketDto.CompanyId,
            MessageTemplateOne = updateDigitalTicketDto.MessageTemplateOne,
            MessageTemplateTwo = updateDigitalTicketDto.MessageTemplateTwo,
            NotificationsNumber = updateDigitalTicketDto.NotificationsNumber
        };

        private static DigitalTicket Map(CreateDigitalTicketDto createDigitalTicketDto) => new DigitalTicket
        {
            CompanyId = createDigitalTicketDto.CompanyId,
            MessageTemplateOne = createDigitalTicketDto.MessageTemplateOne,
            MessageTemplateTwo = createDigitalTicketDto.MessageTemplateTwo,
            NotificationsNumber = createDigitalTicketDto.NotificationsNumber
        };
    }
}
