namespace EmbraceQueue.Domain.Dtos.Categories
{
    /// <summary>
    /// Object used to get an existing category
    /// </summary>
    public class GetCategoryDto
    {
        /// <summary>
        /// Category id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Category name
        /// </summary>
        public string CategoryName { get; set; }
    }
}
