using System.ComponentModel.DataAnnotations;

namespace EmbraceQueue.Domain.Dtos.ServicesServiceLines
{
    public class ServicesServiceLineDto
    {
        /// <summary>
        /// Service id
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
        public int ServiceId { get; set; }

        /// <summary>
        /// ServiceLineId
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
        public int ServiceLineId { get; set; }
    }
}
