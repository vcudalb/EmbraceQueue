using EmbraceQueue.Application.Services.Interfaces;
using EmbraceQueue.Domain.Dtos.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EmbraceQueue.API.V1.Controllers
{
    /// <summary>
    /// Provides Service Operations
    /// </summary>
    [Route("api/v{version:apiVersion}/services/")]
    [Authorize(Roles = "superadmin, branchmanager, helpdeskemployee, enduser")]
    public class ServicesController : ApiController
    {
        private readonly IServiceService _serviceService;
        private readonly ICompanyService _companyService;

        /// <summary>
        /// Default constructor
        /// </summary>
        public ServicesController(IServiceService serviceService, ICompanyService companyService)
        {
            _serviceService = serviceService;
            _companyService = companyService;
        }

        /// <summary>
        /// Get all existing services
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<ActionResult> GetServices()
        {
            var services = await _serviceService.GetServicesAsync().ConfigureAwait(false);
            return Ok(services);
        }

        /// <summary>
        /// Find an existing service by provided service id
        /// </summary>
        /// <returns></returns>
        [HttpGet("find-single/{id}")]
        public async Task<ActionResult> FindServiceById(int id)
        {
            try
            {
                if (id <= 0) return BadRequest(new { Code = "InvalidId", Error = "Please provide a valid id." });

                var service = await _serviceService.FindServiceByIdAsync(id).ConfigureAwait(false);
                if (service != null) return Ok(service);

                return NotFound(new { Message = $"Service with id: {id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Find all existing services by provided id
        /// </summary>
        /// <returns></returns>
        [HttpGet("find-all/{id}")]
        public async Task<ActionResult> FindServicesById(int id)
        {
            try
            {
                if (id <= 0) return BadRequest(new { Code = "InvalidId", Error = "Please provide a valid id." });

                var services = await _serviceService.FindServicesByIdAsync(id).ConfigureAwait(false);
                if (services.Any()) return Ok(services);

                return NotFound(new { Message = $"Services with id: {id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Create a new service
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "superadmin, branchmanager")]
        public async Task<ActionResult> CreateService([FromBody] CreateServiceDto createServiceDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var company = await _companyService.FindCompanyByIdAsync(createServiceDto.CompanyId).ConfigureAwait(false);
                if (company == null) return NotFound(new { Message = $"Company with Id: {createServiceDto.CompanyId} not found. Please provide a valid CategoryId." });

                var createdService = await _serviceService.AddServiceAsync(createServiceDto).ConfigureAwait(false);

                return CreatedAtAction("CreateService", createdService);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Update an existing service
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "superadmin, branchmanager")]
        public async Task<ActionResult> UpdateService(int id, [FromBody] UpdateServiceDto updateServiceDto)
        {
            try
            {
                if (id <= 0) return BadRequest(new { Code = "InvalidId", Error = "Please provide a valid id." });
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var existingService = await _serviceService.FindServiceByIdAsync(id).ConfigureAwait(false);
                if (existingService == null) return NotFound(new { Message = $"Service with id: {id} not found. Please provide a valid entity id." });

                if (updateServiceDto.CompanyId > 0)
                {
                    var company = await _companyService.FindCompanyByIdAsync(updateServiceDto.CompanyId).ConfigureAwait(false);
                    if (company == null) return NotFound(new { Message = $"Company with Id: {updateServiceDto.CompanyId} not found. Please provide a valid CategoryId." });

                }

                await _serviceService.UpdateServiceAsync(id, updateServiceDto);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Delete an existing service
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "superadmin, branchmanager")]
        public async Task<ActionResult> DeleteService(int id)
        {
            try
            {
                if (id <= 0) return BadRequest(new { Error = "Please provide a valid id." });

                var existingService = await _serviceService.FindServiceByIdAsync(id).ConfigureAwait(false);
                if (existingService == null) return NotFound(new { Message = $"Service with id: {id} not found. Please provide a valid entity id." });

                await _serviceService.DeleteServiceAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
