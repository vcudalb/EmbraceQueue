namespace EmbraceQueue.Domain.Dtos.Locations
{
    /// <summary>
    /// Object used to get an existing location
    /// </summary>
    public class GetLocationDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Near byL andmark
        /// </summary>
        public string NearbyLandmark { get; set; }

        /// <summary>
        /// Branch id
        /// </summary>
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
