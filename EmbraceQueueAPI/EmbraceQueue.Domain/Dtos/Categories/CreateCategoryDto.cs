using System.ComponentModel.DataAnnotations;

namespace EmbraceQueue.Domain.Dtos.Categories
{
    /// <summary>
    /// Object used to create a new category
    /// </summary>
    public class CreateCategoryDto
    {
        /// <summary>
        /// Category name
        /// </summary>
        [Required]
        public string CategoryName { get; set; }
    }
}
