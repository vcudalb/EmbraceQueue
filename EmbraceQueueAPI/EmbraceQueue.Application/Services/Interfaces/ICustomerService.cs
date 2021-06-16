using EmbraceQueue.Domain.Dtos.Customers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmbraceQueue.Application.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<GetCustomerDto>> GetCustomersAsync();
        Task<GetCustomerDto> FindCustomerByIdAsync(int id);
        Task<IEnumerable<GetCustomerDto>> FindCustomersByIdAsync(int id);
        Task UpdateCustomerAsync(int id, UpdateCustomerDto customer);
        Task<GetCustomerDto> AddCustomerAsync(CreateCustomerDto customer);
        Task DeleteCustomerAsync(int id);
    }
}
