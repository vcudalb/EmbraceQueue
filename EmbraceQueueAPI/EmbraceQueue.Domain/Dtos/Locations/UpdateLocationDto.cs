using System.ComponentModel.DataAnnotations;

namespace EmbraceQueue.Domain.Dtos.Locations
{
    public class UpdateLocationDto
    {
        /// <summary>
        /// Near byL andmark
        /// </summary>
        public string NearbyLandmark { get; set; }

        /// <summary>
        /// Branch id
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
        public int BranchId { get; set; }

        /// <summary>
        /// Area
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// Building
        /// </summary>
        public int? Building { get; set; }

        /// <summary>
        /// City
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Mall
        /// </summary>
        public string Mall { get; set; }

        /// <summary>
        /// Street 
        /// </summary>
        public string Street { get; set; }
    }
}
