using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EmbraceQueue.Infrastructure.Entities
{
    [Index(nameof(CompanyId), Name = "IX_Services_CompanyId")]
    public partial class Service
    {
        public Service()
        {
            ServicesServiceLines = new HashSet<ServicesServiceLine>();
        }

        [Key]
        public int Id { get; set; }
        public string ServiceType { get; set; }
        public int CompanyId { get; set; }
        public int RecentIncrementedLineId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        [InverseProperty("Services")]
        public virtual Company Company { get; set; }
        [InverseProperty(nameof(ServicesServiceLine.Service))]
        public virtual ICollection<ServicesServiceLine> ServicesServiceLines { get; set; }
    }
}
