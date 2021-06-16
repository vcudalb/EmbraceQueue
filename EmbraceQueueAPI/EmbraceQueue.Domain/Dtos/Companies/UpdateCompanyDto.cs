using System.ComponentModel.DataAnnotations;

namespace EmbraceQueue.Domain.Dtos.Companies
{
    /// <summary>
    /// Object used to update an existing company
    /// </summary>
    public class UpdateCompanyDto
    {
        /// <summary>
        /// Category id
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
        public int CategoryId { get; set; }

        /// <summary>
        /// Company name
        /// </summary>
        [Required]
        public string CompanyName { get; set; }
    }
}
