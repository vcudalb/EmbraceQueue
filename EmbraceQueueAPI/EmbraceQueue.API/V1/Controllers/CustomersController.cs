using EmbraceQueue.Application.Services.Interfaces;
using EmbraceQueue.Domain.Dtos.Customers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EmbraceQueue.API.V1.Controllers
{
    /// <summary>
    /// Provides Customer  Operations
    /// </summary>
    [Route("api/v{version:apiVersion}/customers/")]
    [Authorize(Roles = "superadmin, branchmanager, helpdeskemployee, enduser")]
    public class CustomersController : ApiController
    {
        private readonly ICustomerService _customerService;
        private readonly IDigitalTicketService _digitalTicketService;
        private readonly IServiceLineService _serviceLineService;

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomersController(ICustomerService customerService, IDigitalTicketService digitalTicketService, IServiceLineService serviceLineService)
        {
            _customerService = customerService;
            _digitalTicketService = digitalTicketService;
            _serviceLineService = serviceLineService;
        }

        /// <summary>
        /// Get all existing customers
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        [Authorize(Roles = "superadmin, branchmanager, helpdeskemployee")]
        public async Task<ActionResult> GetCustomers()
        {
            var customers = await _customerService.GetCustomersAsync().ConfigureAwait(false);
            return Ok(customers);
        }

        /// <summary>
        /// Find an existing customer by provided customer id
        /// </summary>
        /// <returns></returns>
        [HttpGet("find-single/{id}")]
        [Authorize(Roles = "superadmin, branchmanager, helpdeskemployee")]
        public async Task<ActionResult> FindCustomerById(int id)
        {
            try
            {
                if (id <= 0) return BadRequest(new { Code = "InvalidId", Error = "Please provide a valid id." });

                var customer = await _customerService.FindCustomerByIdAsync(id).ConfigureAwait(false);
                if (customer != null) return Ok(customer);

                return NotFound(new { Message = $"Customer ticket with id: {id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Find all existing customers by provided id
        /// </summary>
        /// <returns></returns>
        [HttpGet("find-all/{id}")]
        [Authorize(Roles = "superadmin, branchmanager, helpdeskemployee")]
        public async Task<ActionResult> FindCustomersById(int id)
        {
            try
            {
                if (id <= 0) return BadRequest(new { Code = "InvalidId", Error = "Please provide a valid id." });

                var customers = await _customerService.FindCustomersByIdAsync(id).ConfigureAwait(false);
                if (customers.Any()) return Ok(customers);

                return NotFound(new { Message = $"Customers with id: {id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Create a new customer
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateCustomer([FromBody] CreateCustomerDto createCustomerDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var createdCustomer = await _customerService.AddCustomerAsync(createCustomerDto).ConfigureAwait(false);

                return CreatedAtAction("CreateCustomer", createdCustomer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        /// <summary>
        /// Update an existing customer
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "superadmin, branchmanager, helpdeskemployee")]
        public async Task<ActionResult> UpdateCustomer(int id, [FromBody] UpdateCustomerDto updateCustomerDto)
        {
            try
            {
                if (id <= 0) return BadRequest(new { Code = "InvalidId", Error = "Please provide a valid id." });
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var existingCustomer = await _customerService.FindCustomerByIdAsync(id).ConfigureAwait(false);
                if (existingCustomer == null) return NotFound(new { Message = $"Customer with id: {id} not found. Please provide a valid entity id." });

                if (updateCustomerDto.ServiceLineId > 0)
                {
                    var existingServiceLine = await _serviceLineService.FindServiceLineByIdAsync(updateCustomerDto.ServiceLineId).ConfigureAwait(false);
                    if (existingServiceLine == null) return NotFound(new { Message = $"Service line with id: {updateCustomerDto.ServiceLineId} not found. Please provide a valid entity id." });
                }

                if (updateCustomerDto.DigitalTicketId > 0)
                {
                    var existingDigitalTicket = await _digitalTicketService.FindDigitalTicketByIdAsync(updateCustomerDto.DigitalTicketId).ConfigureAwait(false);
                    if (existingDigitalTicket == null) return NotFound(new { Message = $"Digital ticket with id: {updateCustomerDto.DigitalTicketId} not found. Please provide a valid entity id." });
                }

                await _customerService.UpdateCustomerAsync(id, updateCustomerDto);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Delete an existing customer
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "superadmin, branchmanager, helpdeskemployee")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            try
            {
                if (id <= 0) return BadRequest(new { Error = "Please provide a valid id." });

                var existingCustomer = await _customerService.FindCustomerByIdAsync(id).ConfigureAwait(false);
                if (existingCustomer == null) return NotFound(new { Message = $"Customer with id: {id} not found. Please provide a valid entity id." });

                await _customerService.DeleteCustomerAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
