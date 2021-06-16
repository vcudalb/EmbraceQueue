using EmbraceQueue.Application.Services.Interfaces;
using EmbraceQueue.Domain.Dtos.Companies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EmbraceQueue.API.V1.Controllers
{
    /// <summary>
    /// Provides Company Operations
    /// </summary>
    [Route("api/v{version:apiVersion}/companies/")]
    [Authorize(Roles = "superadmin, branchmanager, helpdeskemployee, enduser")]
    public class CompaniesController : ApiController
    {
        private readonly ICompanyService _companyService;
        private readonly ICategoryService _categoryService;

        /// <summary>
        /// Default constructor
        /// </summary>
        public CompaniesController(ICompanyService companyService, ICategoryService categoryService)
        {
            _companyService = companyService;
            _categoryService = categoryService;
        }

        /// <summary>
        /// Get all existing companies
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<ActionResult> GetCompanies()
        {
            var companies = await _companyService.GetCompaniesAsync().ConfigureAwait(false);
            return Ok(companies);
        }

        /// <summary>
        /// Find an existing company by provided company id
        /// </summary>
        /// <returns></returns>
        [HttpGet("find-single/{id}")]
        public async Task<ActionResult> FindCompanyById(int id)
        {
            try
            {
                if (id <= 0) return BadRequest(new { Code = "InvalidId", Error = "Please provide a valid id." });

                var company = await _companyService.FindCompanyByIdAsync(id).ConfigureAwait(false);
                if (company != null) return Ok(company);

                return NotFound(new { Message = $"Company with id: {id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Find all existing companies by provided id
        /// </summary>
        /// <returns></returns>
        [HttpGet("find-all/{id}")]
        public async Task<ActionResult> FindCompaniesById(int id)
        {
            try
            {
                if (id <= 0) return BadRequest(new { Code = "InvalidId", Error = "Please provide a valid id." });

                var companies = await _companyService.FindCompaniesByIdAsync(id).ConfigureAwait(false);
                if (companies.Any()) return Ok(companies);

                return NotFound(new { Message = $"Companies with id: {id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Create a new company
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "superadmin, branchmanager")]
        public async Task<ActionResult> CreateCompany([FromBody] CreateCompanyDto createCompanyDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var category = await _categoryService.FindCategoryByIdAsync(createCompanyDto.CategoryId).ConfigureAwait(false);
                if (category == null) return NotFound(new { Message = $"Category with Id: {createCompanyDto.CategoryId} not found. Please provide a valid CategoryId." });

                var createdCompany = await _companyService.AddCompanyAsync(createCompanyDto).ConfigureAwait(false);

                return CreatedAtAction("CreateCompany", createdCompany);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Update an existing company
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "superadmin, branchmanager")]
        public async Task<ActionResult> UpdateCompany(int id, [FromBody] UpdateCompanyDto updateCompanyDto)
        {
            try
            {
                if (id <= 0) return BadRequest(new { Code = "InvalidId", Error = "Please provide a valid id." });
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var existingComany = await _companyService.FindCompanyByIdAsync(id).ConfigureAwait(false);
                if (existingComany == null) return NotFound(new { Message = $"Company with id: {id} not found. Please provide a valid entity id." });

                if (updateCompanyDto.CategoryId > 0)
                {
                    var category = await _categoryService.FindCategoryByIdAsync(updateCompanyDto.CategoryId).ConfigureAwait(false);
                    if (category == null) return NotFound(new { Message = $"Category with Id: {updateCompanyDto.CategoryId} not found. Please provide a valid CategoryId." });
                }

                await _companyService.UpdateCompanyAsync(id, updateCompanyDto);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Delete an existing company
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "superadmin, branchmanager")]
        public async Task<ActionResult> DeleteCompany(int id)
        {
            try
            {
                if (id <= 0) return BadRequest(new { Error = "Please provide a valid id." });

                var existingCompany = await _companyService.FindCompanyByIdAsync(id).ConfigureAwait(false);
                if (existingCompany == null) return NotFound(new { Message = $"Company with id: {id} not found. Please provide a valid entity id." });

                await _companyService.DeleteCompanyAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
