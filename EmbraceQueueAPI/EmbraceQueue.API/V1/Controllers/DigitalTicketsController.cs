using EmbraceQueue.Application.Services.Interfaces;
using EmbraceQueue.Domain.Dtos.DigitalTickets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EmbraceQueue.API.V1.Controllers
{
    /// <summary>
    /// Provides Digital ticket Operations
    /// </summary>
    [Route("api/v{version:apiVersion}/digitaltickets/")]
    [Authorize(Roles = "superadmin, branchmanager, helpdeskemployee, enduser")]
    public class DigitalTicketsController : ApiController
    {
        private readonly IDigitalTicketService _digitalTicketService;
        private readonly ICompanyService _companyService;

        /// <summary>
        /// Default constructor
        /// </summary>
        public DigitalTicketsController(IDigitalTicketService digitalTicketService, ICompanyService companyService)
        {
            _digitalTicketService = digitalTicketService;
            _companyService = companyService;
        }

        /// <summary>
        /// Get all existing Digital tickets
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        [Authorize(Roles = "superadmin, branchmanager, helpdeskemployee")]
        public async Task<ActionResult> GetDigitalTickets()
        {
            var digitalTickets = await _digitalTicketService.GetDigitalTicketsAsync().ConfigureAwait(false);
            return Ok(digitalTickets);
        }

        /// <summary>
        /// Find an existing digital ticket by provided digital ticket id
        /// </summary>
        /// <returns></returns>
        [HttpGet("find-single/{id}")]
        [Authorize(Roles = "superadmin, branchmanager, helpdeskemployee")]
        public async Task<ActionResult> FindDigitalTicketById(int id)
        {
            try
            {
                if (id <= 0) return BadRequest(new { Code = "InvalidId", Error = "Please provide a valid id." });

                var digitalTicket = await _digitalTicketService.FindDigitalTicketByIdAsync(id).ConfigureAwait(false);
                if (digitalTicket != null) return Ok(digitalTicket);

                return NotFound(new { Message = $"Digital ticket with id: {id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Find all existing digital tickets by provided id
        /// </summary>
        /// <returns></returns>
        [HttpGet("find-all/{id}")]
        [Authorize(Roles = "superadmin, branchmanager, helpdeskemployee")]
        public async Task<ActionResult> FindDigitalTicketsById(int id)
        {
            try
            {
                if (id <= 0) return BadRequest(new { Code = "InvalidId", Error = "Please provide a valid id." });

                var digitalTickets = await _digitalTicketService.FindDigitalTicketsByIdAsync(id).ConfigureAwait(false);
                if (digitalTickets.Any()) return Ok(digitalTickets);

                return NotFound(new { Message = $"Digital tickets with id: {id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Create a new digital ticket
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateDigitalTicket([FromBody] CreateDigitalTicketDto createDigitalTicketDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var company = await _companyService.FindCompanyByIdAsync(createDigitalTicketDto.CompanyId).ConfigureAwait(false);
                if (company == null) return NotFound(new { Message = $"Company with Id: {createDigitalTicketDto.CompanyId} not found. Please provide a valid CategoryId." });

                var createdDigitalTicket = await _digitalTicketService.AddDigitalTicketAsync(createDigitalTicketDto).ConfigureAwait(false);

                return CreatedAtAction("CreateDigitalTicket", createdDigitalTicket);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        /// <summary>
        /// Update an existing digital ticket
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "superadmin, branchmanager, helpdeskemployee")]
        public async Task<ActionResult> UpdateDigitalTicket(int id, [FromBody] UpdateDigitalTicketDto updateDigitalTicketDto)
        {
            try
            {
                if (id <= 0) return BadRequest(new { Code = "InvalidId", Error = "Please provide a valid id." });
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var existingDigitalTicket = await _digitalTicketService.FindDigitalTicketByIdAsync(id).ConfigureAwait(false);
                if (existingDigitalTicket == null) return NotFound(new { Message = $"Digital ticket with id: {id} not found. Please provide a valid entity id." });

                if (updateDigitalTicketDto.CompanyId > 0)
                {
                    var existingComany = await _companyService.FindCompanyByIdAsync(updateDigitalTicketDto.CompanyId).ConfigureAwait(false);
                    if (existingComany == null) return NotFound(new { Message = $"Company with id: {updateDigitalTicketDto.CompanyId} not found. Please provide a valid entity id." });
                }

                await _digitalTicketService.UpdateDigitalTicketAsync(id, updateDigitalTicketDto);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Delete an existing digital ticket
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "superadmin, branchmanager, helpdeskemployee")]
        public async Task<ActionResult> DeleteDigitalTicket(int id)
        {
            try
            {
                if (id <= 0) return BadRequest(new { Error = "Please provide a valid id." });

                var existingDigitalTicket = await _digitalTicketService.FindDigitalTicketByIdAsync(id).ConfigureAwait(false);
                if (existingDigitalTicket == null) return NotFound(new { Message = $"Digital ticket with id: {id} not found. Please provide a valid entity id." });

                await _digitalTicketService.DeleteDigitalTicketAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
