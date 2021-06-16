using EmbraceQueue.Application.Services.Interfaces;
using EmbraceQueue.Domain.Dtos.ServiceLines;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EmbraceQueue.API.V1.Controllers
{
    /// <summary>
    /// Provides Service lines Operations
    /// </summary>
    [Route("api/v{version:apiVersion}/servicelines/")]
    [Authorize(Roles = "superadmin, branchmanager, helpdeskemployee, enduser")]
    public class ServiceLinesController : ApiController
    {
        private readonly IServiceLineService _serviceLineService;
        private readonly IBranchService _brachService;

        /// <summary>
        /// Default constructor
        /// </summary>
        public ServiceLinesController(IServiceLineService serviceLineService, IBranchService branchService)
        {
            _serviceLineService = serviceLineService;
            _brachService = branchService;
        }

        /// <summary>
        /// Get all existing service lines
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<ActionResult> GetServiceLines()
        {
            var serviceLines = await _serviceLineService.GetServiceLinesAsync().ConfigureAwait(false);
            return Ok(serviceLines);
        }

        /// <summary>
        /// Find an existing service line by provided service line id
        /// </summary>
        /// <returns></returns>
        [HttpGet("find-single/{id}")]
        public async Task<ActionResult> FindServiceLineById(int id)
        {
            try
            {
                if (id <= 0) return BadRequest(new { Code = "InvalidId", Error = "Please provide a valid id." });

                var serviceLine = await _serviceLineService.FindServiceLineByIdAsync(id).ConfigureAwait(false);
                if (serviceLine != null) return Ok(serviceLine);

                return NotFound(new { Message = $"Service line with id: {id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Find all existing service lines by provided id
        /// </summary>
        /// <returns></returns>
        [HttpGet("find-all/{id}")]
        public async Task<ActionResult> FindServiceLinesById(int id)
        {
            try
            {
                if (id <= 0) return BadRequest(new { Code = "InvalidId", Error = "Please provide a valid id." });

                var serviceLines = await _serviceLineService.FindServiceLinesByIdAsync(id).ConfigureAwait(false);
                if (serviceLines.Any()) return Ok(serviceLines);

                return NotFound(new { Message = $"Service lines with id: {id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Create a new service line
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "superadmin, branchmanager")]
        public async Task<ActionResult> CreateServiceLine([FromBody] CreateServiceLineDto createServiceLineDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var branch = await _brachService.FindBranchByIdAsync(createServiceLineDto.BranchId).ConfigureAwait(false);
                if (branch == null) return NotFound(new { Message = $"Brach with Id: {createServiceLineDto.BranchId} not found. Please provide a valid BranchId." });

                var createdServiceLine = await _serviceLineService.AddServiceLineAsync(createServiceLineDto).ConfigureAwait(false);

                return CreatedAtAction("CreateServiceLine", createdServiceLine);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Update an existing service line
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "superadmin, branchmanager")]
        public async Task<ActionResult> UpdateServiceLine(int id, [FromBody] UpdateServiceLineDto updateServiceLineDto)
        {
            try
            {
                if (id <= 0) return BadRequest(new { Code = "InvalidId", Error = "Please provide a valid id." });
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var existingServiceLine = await _serviceLineService.FindServiceLineByIdAsync(id).ConfigureAwait(false);
                if (existingServiceLine == null) return NotFound(new { Message = $"Service line with id: {id} not found. Please provide a valid entity id." });

                if (updateServiceLineDto.BranchId > 0)
                {
                    var branch = await _brachService.FindBranchByIdAsync(updateServiceLineDto.BranchId).ConfigureAwait(false);
                    if (branch == null) return NotFound(new { Message = $"Branch with Id: {updateServiceLineDto.BranchId} not found. Please provide a valid CategoryId." });
                }

                await _serviceLineService.UpdateServiceLineAsync(id, updateServiceLineDto);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Delete an existing locaiton
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "superadmin, branchmanager")]
        public async Task<ActionResult> DeleteServiceLine(int id)
        {
            try
            {
                if (id <= 0) return BadRequest(new { Error = "Please provide a valid id." });

                var existingServiceLine = await _serviceLineService.FindServiceLineByIdAsync(id).ConfigureAwait(false);
                if (existingServiceLine == null) return NotFound(new { Message = $"Service line with id: {id} not found. Please provide a valid entity id." });

                await _serviceLineService.DeleteServiceLineAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
