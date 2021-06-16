using EmbraceQueue.Application.Services.Interfaces;
using EmbraceQueue.Domain.Dtos.ServicesServiceLines;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EmbraceQueue.API.V1.Controllers
{
    /// <summary>
    /// Provides Services service line Operations
    /// </summary>
    [Route("api/v{version:apiVersion}/servicesoutput/")]
    [Authorize(Roles = "superadmin, branchmanager, helpdeskemployee, enduser")]
    public class ServicesServiceLinesController : ApiController
    {
        private readonly IServicesServiceLineService _servicesServiceLineService;

        /// <summary>
        /// Default constructor
        /// </summary>
        public ServicesServiceLinesController(IServicesServiceLineService servicesServiceLineService)
        {
            _servicesServiceLineService = servicesServiceLineService;
        }

        /// <summary>
        /// Get all existing services service lines
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<ActionResult> GetAllServices()
        {
            try
            {
                var services = await _servicesServiceLineService.GetServicesServiceLinesAsync().ConfigureAwait(false);
                return Ok(services);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Get all existing services service lines by service id
        /// </summary>
        /// <returns></returns>
        [HttpGet("find-services/{serviceId}")]
        public async Task<ActionResult> FindServicesByServiceId(int serviceId)
        {
            try
            {
                if (serviceId <= 0) return BadRequest(new { Code = "InvalidId", Error = "Please provide a valid id." });

                var services = await _servicesServiceLineService.FindAllServices(serviceId).ConfigureAwait(false);
                return Ok(services);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Get all existing services service lines by service line id
        /// </summary>
        /// <returns></returns>
        [HttpGet("find-lines/{serviceLineId}")]
        public async Task<ActionResult> FindServicesByServiceLineId(int serviceLineId)
        {
            try
            {
                if (serviceLineId <= 0) return BadRequest(new { Code = "InvalidId", Error = "Please provide a valid id." });

                var services = await _servicesServiceLineService.FindAllServiceLines(serviceLineId).ConfigureAwait(false);
                return Ok(services);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Get all existing services service lines by service line id
        /// </summary>
        /// <returns></returns>
        [HttpGet("find-lines/{serviceId}/{serviceLineId}")]
        public async Task<ActionResult> FindServiceByServiceLineIdAndServiceId(int serviceId, int serviceLineId)
        {
            try
            {
                if (serviceLineId <= 0 || serviceId <= 0) return BadRequest(new { Code = "InvalidId", Error = "Please provide a valid id." });

                var services = await _servicesServiceLineService.FindServicesServiceLine(serviceId, serviceLineId).ConfigureAwait(false);
                return Ok(services);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Create a new services service line
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateServicesServiceLine([FromBody] ServicesServiceLineDto servicesServiceLineDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var services = await _servicesServiceLineService.AddServiceServiceLineAsync(servicesServiceLineDto).ConfigureAwait(false);
                return Ok(services);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Delete an existing services service line
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{serviceId}/{serviceLineId}")]
        public async Task<ActionResult> DeleteServicesServiceLine(int serviceId, int serviceLineId)
        {
            try
            {
                await _servicesServiceLineService.DeleteServiceServiceLineAsync(serviceId, serviceLineId).ConfigureAwait(false);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
