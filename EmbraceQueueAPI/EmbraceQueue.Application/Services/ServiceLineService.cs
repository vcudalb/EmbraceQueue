using EmbraceQueue.Application.Services.Interfaces;
using EmbraceQueue.Domain.Dtos.ServiceLines;
using EmbraceQueue.Infrastructure.Entities;
using EmbraceQueue.Infrastructure.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmbraceQueue.Application.Services
{
    public class ServiceLineService : IServiceLineService
    {
        private readonly IServiceLineRepository _serviceLineRepository;

        public ServiceLineService(IServiceLineRepository serviceLineRepository)
        {
            _serviceLineRepository = serviceLineRepository;
        }

        public async Task<IEnumerable<GetServiceLineDto>> GetServiceLinesAsync()
        {
            var serviceLines = await _serviceLineRepository.GetServiceLinesAsync().ConfigureAwait(false);
            return serviceLines.Select(x => Map(x));
        }

        public async Task<GetServiceLineDto> FindServiceLineByIdAsync(int id)
        {
            var serviceLine = await _serviceLineRepository.FindServiceLineByIdAsync(id).ConfigureAwait(false);
            if (serviceLine != null) return Map(serviceLine);
            return null;
        }

        public async Task<IEnumerable<GetServiceLineDto>> FindServiceLinesByIdAsync(int id)
        {
            var serviceLines = await _serviceLineRepository.FindServiceLinesByIdAsync(id).ConfigureAwait(false);
            return serviceLines.Select(x => Map(x));
        }

        public async Task UpdateServiceLineAsync(int id, UpdateServiceLineDto serviceLine)
        {
            await _serviceLineRepository.UpdateServiceLineAsync(Map(id, serviceLine)).ConfigureAwait(false);
        }

        public async Task<GetServiceLineDto> AddServiceLineAsync(CreateServiceLineDto serviceLine)
        {
            var createdServiceLine = await _serviceLineRepository.AddServiceLineAsync(Map(serviceLine)).ConfigureAwait(false);
            return Map(createdServiceLine);
        }

        public async Task DeleteServiceLineAsync(int id)
        {
            await _serviceLineRepository.DeleteServiceLineAsync(id).ConfigureAwait(false);
        }

        private static GetServiceLineDto Map(ServiceLine serviceLine) => new GetServiceLineDto
        {
            Id = serviceLine.Id,
            BranchId = serviceLine.BranchId,
            CounterNumber = serviceLine.CounterNumber,
            CreatedAt = serviceLine.CreatedAt,
            CurrentQueueStatus = serviceLine.CurrentQueueStatus,
            CurrentSequentialNumber = serviceLine.CurrentSequentialNumber,
            LastIncrementedDateTime = serviceLine.LastIncrementedDateTime,
            PeopleGotInLineCounter = serviceLine.PeopleGotInLineCounter
        };

        private static ServiceLine Map(int id, UpdateServiceLineDto updateServiceLineDto) => new ServiceLine
        {
            Id = id,
            BranchId = updateServiceLineDto.BranchId,
            CounterNumber = updateServiceLineDto.CounterNumber,
            CreatedAt = updateServiceLineDto.CreatedAt,
            CurrentQueueStatus = updateServiceLineDto.CurrentQueueStatus,
            CurrentSequentialNumber = updateServiceLineDto.CurrentSequentialNumber,
            LastIncrementedDateTime = updateServiceLineDto.LastIncrementedDateTime,
            PeopleGotInLineCounter = updateServiceLineDto.PeopleGotInLineCounter
        };

        private static ServiceLine Map(CreateServiceLineDto createServiceLineDto) => new ServiceLine
        {
            BranchId = createServiceLineDto.BranchId,
            CounterNumber = createServiceLineDto.CounterNumber,
            CreatedAt = createServiceLineDto.CreatedAt,
            CurrentQueueStatus = createServiceLineDto.CurrentQueueStatus,
            CurrentSequentialNumber = createServiceLineDto.CurrentSequentialNumber,
            LastIncrementedDateTime = createServiceLineDto.LastIncrementedDateTime,
            PeopleGotInLineCounter = createServiceLineDto.PeopleGotInLineCounter
        };
    }
}
