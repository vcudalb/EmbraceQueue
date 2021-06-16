using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmbraceQueue.Infrastructure.Entities
{
    public partial class Category
    {
        public Category()
        {
            Companies = new HashSet<Company>();
        }

        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }

        [InverseProperty(nameof(Company.Category))]
        public virtual ICollection<Company> Companies { get; set; }
    }
}
