namespace EmbraceQueue.Domain.Dtos.Services
{
    /// <summary>
    /// Object used to get service
    /// </summary>
    public class GetServiceDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Service type
        /// </summary>
        public string ServiceType { get; set; }

        /// <summary>
        /// Company id
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// Recent incremented line id
        /// </summary>
        public int RecentIncrementedLineId { get; set; }
    }
}
