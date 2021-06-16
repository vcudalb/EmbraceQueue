using EmbraceQueue.Application.Services.Interfaces;
using EmbraceQueue.Domain.Dtos.WorkingDays;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EmbraceQueue.API.V1.Controllers
{
    /// <summary>
    /// Provides Working Day Operations
    /// </summary>
    [Route("api/v{version:apiVersion}/workingdays/")]
    [Authorize(Roles = "superadmin, branchmanager, helpdeskemployee, enduser")]
    public class WorkingDayController : ApiController
    {
        private readonly IWorkingDayService _workingDayService;
        private readonly IBranchService _brachService;

        /// <summary>
        /// Default constructor
        /// </summary>
        public WorkingDayController(IWorkingDayService workingDayService, IBranchService branchService)
        {
            _workingDayService = workingDayService;
            _brachService = branchService;
        }

        /// <summary>
        /// Get all existing working days
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<ActionResult> GeWorkingDays()
        {
            var workingDays = await _workingDayService.GetWorkingDaysAsync().ConfigureAwait(false);
            return Ok(workingDays);
        }

        /// <summary>
        /// Find an existing working day by provided working day id
        /// </summary>
        /// <returns></returns>
        [HttpGet("find-single/{id}")]
        public async Task<ActionResult> FindWorkingDayById(int id)
        {
            try
            {
                if (id <= 0) return BadRequest(new { Code = "InvalidId", Error = "Please provide a valid id." });

                var workingDay = await _workingDayService.FindWorkingDayByIdAsync(id).ConfigureAwait(false);
                if (workingDay != null) return Ok(workingDay);

                return NotFound(new { Message = $"Working day with id: {id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Find all existing working days by provided id
        /// </summary>
        /// <returns></returns>
        [HttpGet("find-all/{id}")]
        public async Task<ActionResult> FindWorkingDaysById(int id)
        {
            try
            {
                if (id <= 0) return BadRequest(new { Code = "InvalidId", Error = "Please provide a valid id." });

                var workingDays = await _workingDayService.FindWorkingDaysByIdAsync(id).ConfigureAwait(false);
                if (workingDays.Any()) return Ok(workingDays);

                return NotFound(new { Message = $"Working days with id: {id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Create a new working day
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "superadmin, branchmanager")]
        public async Task<ActionResult> CreateWorkingDay([FromBody] CreateWorkingDayDto createWorkingDayDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var branch = await _brachService.FindBranchByIdAsync(createWorkingDayDto.BranchId).ConfigureAwait(false);
                if (branch == null) return NotFound(new { Message = $"Brach with Id: {createWorkingDayDto.BranchId} not found. Please provide a valid BranchId." });

                var createdWorkingDay = await _workingDayService.AddWorkingDayAsync(createWorkingDayDto).ConfigureAwait(false);

                return CreatedAtAction("CreateWorkingDay", createdWorkingDay);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Update an existing working day
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "superadmin, branchmanager")]
        public async Task<ActionResult> UpdateWorkingDay(int id, [FromBody] UpdateWorkingDayDto updateWorkingDayDto)
        {
            try
            {
                if (id <= 0) return BadRequest(new { Code = "InvalidId", Error = "Please provide a valid id." });
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var existingWorkingDay = await _workingDayService.FindWorkingDayByIdAsync(id).ConfigureAwait(false);
                if (existingWorkingDay == null) return NotFound(new { Message = $"Working day with id: {id} not found. Please provide a valid entity id." });

                if (updateWorkingDayDto.BranchId > 0)
                {
                    var branch = await _brachService.FindBranchByIdAsync(updateWorkingDayDto.BranchId).ConfigureAwait(false);
                    if (branch == null) return NotFound(new { Message = $"Branch with Id: {updateWorkingDayDto.BranchId} not found. Please provide a valid CategoryId." });
                }

                await _workingDayService.UpdateWorkingDayAsync(id, updateWorkingDayDto);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Delete an existing working day
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "superadmin, branchmanager")]
        public async Task<ActionResult> DeleteWorkingDay(int id)
        {
            try
            {
                if (id <= 0) return BadRequest(new { Error = "Please provide a valid id." });

                var existingWorkingDay = await _workingDayService.FindWorkingDayByIdAsync(id).ConfigureAwait(false);
                if (existingWorkingDay == null) return NotFound(new { Message = $"Working day with id: {id} not found. Please provide a valid entity id." });

                await _workingDayService.DeleteWorkingDayAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
