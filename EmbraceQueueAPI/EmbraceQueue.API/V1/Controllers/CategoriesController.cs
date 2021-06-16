using EmbraceQueue.Application.Services.Interfaces;
using EmbraceQueue.Domain.Dtos.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EmbraceQueue.API.V1.Controllers
{
    /// <summary>
    /// Provides Categories Operations
    /// </summary>
    [Route("api/v{version:apiVersion}/categories/")]
    [Authorize(Roles = "superadmin, branchmanager, helpdeskemployee, enduser")]
    public class CategoriesController : ApiController
    {
        private readonly ICategoryService _categoryService;

        /// <summary>
        /// Default constructor
        /// </summary>
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Get all existing categories
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<ActionResult> GetCategories()
        {
            var categories = await _categoryService.GetCategoriesAsync().ConfigureAwait(false);
            return Ok(categories);
        }

        /// <summary>
        /// Find an existing category by provided category id
        /// </summary>
        /// <returns></returns>
        [HttpGet("find-single/{id}")]
        public async Task<ActionResult> FindCategoryById(int id)
        {
            try
            {
                if (id <= 0) return BadRequest(new { Code = "InvalidId", Error = "Please provide a valid id." });

                var category = await _categoryService.FindCategoryByIdAsync(id).ConfigureAwait(false);
                if (category != null) return Ok(category);

                return NotFound(new { Message = $"Category with id: {id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Find all existing categories by provided id
        /// </summary>
        /// <returns></returns>
        [HttpGet("find-all/{id}")]
        public async Task<ActionResult> FindCategoriesById(int id)
        {
            try
            {
                if (id <= 0) return BadRequest(new { Code = "InvalidId", Error = "Please provide a valid id." });

                var categories = await _categoryService.FindCategoriesByIdAsync(id).ConfigureAwait(false);
                if (categories.Any()) return Ok(categories);

                return NotFound(new { Message = $"Categories with id: {id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Create a new category
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "superadmin, branchmanager")]
        public async Task<ActionResult> CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var createdCategory = await _categoryService.AddCategoryAsync(createCategoryDto).ConfigureAwait(false);

                return CreatedAtAction("CreateCategory", createdCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Update an existing category
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "superadmin, branchmanager")]
        public async Task<ActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryDto updateCategoryDto)
        {
            try
            {
                if (id <= 0) return BadRequest(new { Code = "InvalidId", Error = "Please provide a valid id." });
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var existingCategory = await _categoryService.FindCategoryByIdAsync(id).ConfigureAwait(false);
                if (existingCategory == null) return NotFound(new { Message = $"Category with id: {id} not found. Please provide a valid entity id." });

                await _categoryService.UpdateCategoryAsync(id, updateCategoryDto);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Delete an existing category
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "superadmin, branchmanager")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            try
            {
                if (id <= 0) return BadRequest(new { Code = "InvalidId", Error = "Please provide a valid id." });

                var existingCategory = await _categoryService.FindCategoryByIdAsync(id).ConfigureAwait(false);
                if (existingCategory == null) return NotFound(new { Message = $"Category with id: {id} not found. Please provide a valid entity id." });

                await _categoryService.DeleteCategoryAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
