using EmbraceQueue.Infrastructure.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmbraceQueue.Infrastructure.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<Customer> FindCustomerByIdAsync(int id);
        Task<IEnumerable<Customer>> FindCustomersByIdAsync(int id);
        Task UpdateCustomerAsync(Customer location);
        Task<Customer> AddCustomerAsync(Customer location);
        Task DeleteCustomerAsync(int id);
    }
}
