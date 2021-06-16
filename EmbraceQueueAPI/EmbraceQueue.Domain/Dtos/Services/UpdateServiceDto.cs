using System.ComponentModel.DataAnnotations;

namespace EmbraceQueue.Domain.Dtos.Services
{
    /// <summary>
    /// Object used to update an existing service
    /// </summary>
    public class UpdateServiceDto
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
