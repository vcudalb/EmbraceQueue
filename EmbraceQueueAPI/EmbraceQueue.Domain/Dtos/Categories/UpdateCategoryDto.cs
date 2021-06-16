using System.ComponentModel.DataAnnotations;

namespace EmbraceQueue.Domain.Dtos.Categories
{
    /// <summary>
    /// Object used to update an existing category
    /// </summary>
    public class UpdateCategoryDto
    {
        /// <summary>
        /// Category name
        /// </summary>
        [Required]
        public string CategoryName { get; set; }
    }
}
