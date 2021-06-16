using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EmbraceQueue.Infrastructure.Entities
{
    [Index(nameof(ServiceLineId), Name = "IX_ServicesServiceLines_ServiceLineId")]
    public partial class ServicesServiceLine
    {
        [Key]
        public int ServiceId { get; set; }
        [Key]
        public int ServiceLineId { get; set; }

        [ForeignKey(nameof(ServiceId))]
        [InverseProperty("ServicesServiceLines")]
        public virtual Service Service { get; set; }
        [ForeignKey(nameof(ServiceLineId))]
        [InverseProperty("ServicesServiceLines")]
        public virtual ServiceLine ServiceLine { get; set; }
    }
}
