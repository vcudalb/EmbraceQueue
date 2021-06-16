using EmbraceQueue.Application.Services.Interfaces;
using EmbraceQueue.Domain.Dtos.Customers;
using EmbraceQueue.Infrastructure.Entities;
using EmbraceQueue.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmbraceQueue.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<GetCustomerDto>> GetCustomersAsync()
        {
            var customers = await _customerRepository.GetCustomersAsync().ConfigureAwait(false);
            return customers.Select(x => Map(x));
        }

        public async Task<GetCustomerDto> FindCustomerByIdAsync(int id)
        {
            var customer = await _customerRepository.FindCustomerByIdAsync(id).ConfigureAwait(false);
            if (customer != null) return Map(customer);
            return null;
        }

        public async Task<IEnumerable<GetCustomerDto>> FindCustomersByIdAsync(int id)
        {
            var customers = await _customerRepository.FindCustomersByIdAsync(id).ConfigureAwait(false);
            return customers.Select(x => Map(x));
        }

        public async Task UpdateCustomerAsync(int id, UpdateCustomerDto customer)
        {
            await _customerRepository.UpdateCustomerAsync(Map(id, customer)).ConfigureAwait(false);
        }

        public async Task<GetCustomerDto> AddCustomerAsync(CreateCustomerDto customer)
        {
            var createdCustomer = await _customerRepository.AddCustomerAsync(Map(customer)).ConfigureAwait(false);
            return Map(createdCustomer);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            await _customerRepository.DeleteCustomerAsync(id).ConfigureAwait(false);
        }

        private static GetCustomerDto Map(Customer customer) => new GetCustomerDto
        {
            Id = customer.Id,
            ServiceLineId = customer.ServiceLineId,
            DigitalTicketId = customer.DigitalTicketId,
            CustomerPhoneNumber = customer.CustomerPhoneNumber,
            HasReceivedSmsreminder = customer.HasReceivedSmsreminder,
            HasReceivedSmsticket = customer.HasReceivedSmsticket,
            HasShownUpAndGotServed = customer.HasShownUpAndGotServed,
            PhoneNumberSubmissionDateTime = customer.PhoneNumberSubmissionDateTime,
            SequentialNumber = customer.SequentialNumber,
            ServiceFinishDateTime = customer.ServiceFinishDateTime
        };

        private static Customer Map(int id, UpdateCustomerDto updateCustomerDto) => new Customer
        {
            Id = id,
            ServiceLineId = updateCustomerDto.ServiceLineId,
            DigitalTicketId = updateCustomerDto.DigitalTicketId,
            CustomerPhoneNumber = updateCustomerDto.CustomerPhoneNumber,
            HasReceivedSmsreminder = updateCustomerDto.HasReceivedSmsreminder,
            HasReceivedSmsticket = updateCustomerDto.HasReceivedSmsticket,
            HasShownUpAndGotServed = updateCustomerDto.HasShownUpAndGotServed,
            PhoneNumberSubmissionDateTime = updateCustomerDto.PhoneNumberSubmissionDateTime,
            SequentialNumber = updateCustomerDto.SequentialNumber,
            ServiceFinishDateTime = updateCustomerDto.ServiceFinishDateTime
        };

        private static Customer Map(CreateCustomerDto createCustomerDto) => new Customer
        {
            ServiceLineId = createCustomerDto.ServiceLineId,
            DigitalTicketId = createCustomerDto.DigitalTicketId,
            CustomerPhoneNumber = createCustomerDto.CustomerPhoneNumber,
            HasReceivedSmsreminder = createCustomerDto.HasReceivedSmsreminder,
            HasReceivedSmsticket = createCustomerDto.HasReceivedSmsticket,
            HasShownUpAndGotServed = createCustomerDto.HasShownUpAndGotServed,
            PhoneNumberSubmissionDateTime = createCustomerDto.PhoneNumberSubmissionDateTime,
            SequentialNumber = createCustomerDto.SequentialNumber,
            ServiceFinishDateTime = createCustomerDto.ServiceFinishDateTime
        };
    }
}
