using EmbraceQueue.Application.Services.Interfaces;
using EmbraceQueue.Domain.Dtos.Branches;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EmbraceQueue.API.V1.Controllers
{
    /// <summary>
    /// Provides Branches Operations
    /// </summary>
    [Route("api/v{version:apiVersion}/branches/")]
    [Authorize(Roles = "superadmin, branchmanager, helpdeskemployee, enduser")]
    public class BranchesController : ApiController
    {
        private readonly IBranchService _branchService;
        private readonly ICompanyService _companyService;

        /// <summary>
        /// Default constructor
        /// </summary>
        public BranchesController(IBranchService branchService, ICompanyService companyService)
        {
            _branchService = branchService;
            _companyService = companyService;
        }

        /// <summary>
        /// Get all existing branches
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<ActionResult> GetBranches()
        {
            var branches = await _branchService.GetBranchesAsync().ConfigureAwait(false);
            return Ok(branches);
        }

        /// <summary>
        /// Find an existing branch by provided branch id
        /// </summary>
        /// <returns></returns>
        [HttpGet("find-single/{id}")]
        public async Task<ActionResult> FindBranchById(int id)
        {
            try
            {
                if (id <= 0) return BadRequest(new { Code = "InvalidId", Error = "Please provide a valid id." });

                var branch = await _branchService.FindBranchByIdAsync(id).ConfigureAwait(false);
                if (branch != null) return Ok(branch);

                return NotFound(new { Message = $"Branch with id: {id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Find all existing branches by provided id
        /// </summary>
        /// <returns></returns>
        [HttpGet("find-all/{id}")]
        public async Task<ActionResult> FindBranchesById(int id)
        {
            try
            {
                if (id <= 0) return BadRequest(new { Code = "InvalidId", Error = "Please provide a valid id." });

                var branches = await _branchService.FindBranchesByIdAsync(id).ConfigureAwait(false);
                if (branches.Any()) return Ok(branches);

                return NotFound(new { Message = $"Branches with id: {id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Create a new branch
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "superadmin, branchmanager")]
        public async Task<ActionResult> CreateBranch([FromBody] CreateBranchDto createBranchDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var company = await _companyService.FindCompanyByIdAsync(createBranchDto.CompanyId).ConfigureAwait(false);
                if (company == null) return NotFound(new { Message = $"Company with Id: {createBranchDto.CompanyId} not found. Please provide a valid CategoryId." });

                var createdBranch = await _branchService.AddBranchAsync(createBranchDto).ConfigureAwait(false);

                return CreatedAtAction("CreateBranch", createdBranch);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        /// <summary>
        /// Update an existing branch
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "superadmin, branchmanager")]
        public async Task<ActionResult> UpdateBranch(int id, [FromBody] UpdateBranchDto updateBranchDto)
        {
            try
            {
                if (id <= 0) return BadRequest(new { Code = "InvalidId", Error = "Please provide a valid id." });
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var existingBranch = await _branchService.FindBranchByIdAsync(id).ConfigureAwait(false);
                if (existingBranch == null) return NotFound(new { Message = $"Branch with id: {id} not found. Please provide a valid entity id." });

                if (updateBranchDto.CompanyId > 0)
                {
                    var existingComany = await _companyService.FindCompanyByIdAsync(updateBranchDto.CompanyId).ConfigureAwait(false);
                    if (existingComany == null) return NotFound(new { Message = $"Company with id: {updateBranchDto.CompanyId} not found. Please provide a valid entity id." });
                }

                await _branchService.UpdateBranchAsync(id, updateBranchDto);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Delete an existing branch
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "superadmin, branchmanager")]
        public async Task<ActionResult> DeleteBranch(int id)
        {
            try
            {
                if (id <= 0) return BadRequest(new { Error = "Please provide a valid id." });

                var existingBranch = await _branchService.FindBranchByIdAsync(id).ConfigureAwait(false);
                if (existingBranch == null) return NotFound(new { Message = $"Branch with id: {id} not found. Please provide a valid entity id." });

                await _branchService.DeleteBranchAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
