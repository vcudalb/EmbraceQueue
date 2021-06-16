using System.ComponentModel.DataAnnotations;

namespace EmbraceQueue.Domain.Dtos.Services
{
    /// <summary>
    /// Object used to create a new service
    /// </summary>
    public class CreateServiceDto
    {
        /// <summary>
        /// Service type
        /// </summary>
        public string ServiceType { get; set; }

        /// <summary>
        /// Company id
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
        public int CompanyId { get; set; }

        /// <summary>
        /// Recent incremented line id
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
        public int RecentIncrementedLineId { get; set; }
    }
}
