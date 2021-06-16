using EmbraceQueue.Infrastructure.Entities;
using EmbraceQueue.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmbraceQueue.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly EmbraceQueueDbContext _dbContext;
        public CustomerRepository(EmbraceQueueDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return await _dbContext.Customers.AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        public async Task<Customer> FindCustomerByIdAsync(int id)
        {
            return await _dbContext.Customers.AsNoTracking().FirstOrDefaultAsync(l => l.Id == id).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Customer>> FindCustomersByIdAsync(int id)
        {
            return await _dbContext.Customers.AsNoTracking().Where(c => c.Id == id).ToListAsync().ConfigureAwait(false);
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            var existingCustomer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == customer.Id).ConfigureAwait(false);

            if (existingCustomer.ServiceLineId != customer.ServiceLineId) existingCustomer.ServiceLineId = customer.ServiceLineId;
            if (existingCustomer.DigitalTicketId != customer.DigitalTicketId) existingCustomer.DigitalTicketId = customer.DigitalTicketId;
            if (existingCustomer.HasReceivedSmsreminder != customer.HasReceivedSmsreminder) existingCustomer.HasReceivedSmsreminder = customer.HasReceivedSmsreminder;
            if (existingCustomer.HasReceivedSmsticket != customer.HasReceivedSmsticket) existingCustomer.HasReceivedSmsticket = customer.HasReceivedSmsticket;
            if (existingCustomer.HasShownUpAndGotServed != customer.HasShownUpAndGotServed) existingCustomer.HasShownUpAndGotServed = customer.HasShownUpAndGotServed;
            if (existingCustomer.PhoneNumberSubmissionDateTime != customer.PhoneNumberSubmissionDateTime) existingCustomer.PhoneNumberSubmissionDateTime = customer.PhoneNumberSubmissionDateTime;
            if (existingCustomer.SequentialNumber != customer.SequentialNumber) existingCustomer.SequentialNumber = customer.SequentialNumber;
            if (existingCustomer.ServiceFinishDateTime != customer.ServiceFinishDateTime) existingCustomer.ServiceFinishDateTime = customer.ServiceFinishDateTime;


            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
            var isAnExistingCustomer = await _dbContext.Customers.AnyAsync(c => c.ServiceLineId == customer.ServiceLineId && c.DigitalTicketId == customer.DigitalTicketId).ConfigureAwait(false);
            if (isAnExistingCustomer) throw new Exception($"Customer with ServiceLineId: {customer.ServiceLineId} and DigitalTicketId: {customer.DigitalTicketId} already exists.");

            await _dbContext.AddAsync(customer).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return customer;
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var existingCustomer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == id).ConfigureAwait(false);
            _dbContext.Customers.Remove(existingCustomer);

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
