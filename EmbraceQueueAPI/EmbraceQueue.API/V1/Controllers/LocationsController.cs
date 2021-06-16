using EmbraceQueue.Application.Services.Interfaces;
using EmbraceQueue.Domain.Dtos.Locations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EmbraceQueue.API.V1.Controllers
{
    /// <summary>
    /// Provides Location Operations
    /// </summary>
    [Route("api/v{version:apiVersion}/locations/")]
    [Authorize(Roles = "superadmin, branchmanager, helpdeskemployee, enduser")]
    public class LocationsController : ApiController
    {
        private readonly ILocationService _locationService;
        private readonly IBranchService _brachService;

        /// <summary>
        /// Default constructor
        /// </summary>
        public LocationsController(ILocationService locationService, IBranchService branchService)
        {
            _locationService = locationService;
            _brachService = branchService;
        }

        /// <summary>
        /// Get all existing locations
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<ActionResult> GetLocations()
        {
            var locations = await _locationService.GetLocationsAsync().ConfigureAwait(false);
            return Ok(locations);
        }

        /// <summary>
        /// Find an existing location by provided location id
        /// </summary>
        /// <returns></returns>
        [HttpGet("find-single/{id}")]
        public async Task<ActionResult> FindLocationById(int id)
        {
            try
            {
                if (id <= 0) return BadRequest(new { Code = "InvalidId", Error = "Please provide a valid id." });

                var location = await _locationService.FindLocationByIdAsync(id).ConfigureAwait(false);
                if (location != null) return Ok(location);

                return NotFound(new { Message = $"Location with id: {id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Find all existing locations by provided id
        /// </summary>
        /// <returns></returns>
        [HttpGet("find-all/{id}")]
        public async Task<ActionResult> FindLocationsById(int id)
        {
            try
            {
                if (id <= 0) return BadRequest(new { Code = "InvalidId", Error = "Please provide a valid id." });
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var locations = await _locationService.FindLocationsByIdAsync(id).ConfigureAwait(false);
                if (locations.Any()) return Ok(locations);

                return NotFound(new { Message = $"Locations with id: {id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Create a new location
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "superadmin, branchmanager")]
        public async Task<ActionResult> CreateLocation([FromBody] CreateLocationDto createLocationDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var branch = await _brachService.FindBranchByIdAsync(createLocationDto.BranchId).ConfigureAwait(false);
                if (branch == null) return NotFound(new { Message = $"Brach with Id: {createLocationDto.BranchId} not found. Please provide a valid BranchId." });

                var createdLocation = await _locationService.AddLocationAsync(createLocationDto).ConfigureAwait(false);

                return CreatedAtAction("CreateLocation", createdLocation);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Update an existing location
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "superadmin, branchmanager")]
        public async Task<ActionResult> UpdateLocation(int id, [FromBody] UpdateLocationDto updateLocationDto)
        {
            try
            {
                if (id <= 0) return BadRequest(new { Code = "InvalidId", Error = "Please provide a valid id." });

                var existingLocation = await _locationService.FindLocationByIdAsync(id).ConfigureAwait(false);
                if (existingLocation == null) return NotFound(new { Message = $"Location with id: {id} not found. Please provide a valid entity id." });

                if (updateLocationDto.BranchId > 0)
                {
                    var branch = await _brachService.FindBranchByIdAsync(updateLocationDto.BranchId).ConfigureAwait(false);
                    if (branch == null) return NotFound(new { Message = $"Branch with Id: {updateLocationDto.BranchId} not found. Please provide a valid CategoryId." });
                }

                await _locationService.UpdateLocationAsync(id, updateLocationDto);

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
        public async Task<ActionResult> DeleteLocation(int id)
        {
            try
            {
                if (id <= 0) return BadRequest(new { Error = "Please provide a valid id." });

                var existingLocation = await _locationService.FindLocationByIdAsync(id).ConfigureAwait(false);
                if (existingLocation == null) return NotFound(new { Message = $"Location with id: {id} not found. Please provide a valid entity id." });

                await _locationService.DeleteLocationAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
