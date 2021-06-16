namespace EmbraceQueue.Domain.Dtos.Companies
{
    /// <summary>
    /// Object used to get an existing company
    /// </summary>
    public class GetCompanyDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Company name
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Category id
        /// </summary>
        public int CategoryId { get; set; }
    }
}
