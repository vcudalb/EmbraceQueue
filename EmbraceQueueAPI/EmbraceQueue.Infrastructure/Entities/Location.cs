using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace EmbraceQueue.Infrastructure.Entities
{
    [Index(nameof(BranchId), Name = "IX_Locations_BranchId", IsUnique = true)]
    public partial class Location
    {
        [Key]
        public int Id { get; set; }
        public string NearbyLandmark { get; set; }
        public int BranchId { get; set; }
        public string Area { get; set; }
        public int? Building { get; set; }
        public string City { get; set; }
        public string Mall { get; set; }
        public string Street { get; set; }

        [ForeignKey(nameof(BranchId))]
        [InverseProperty("Location")]
        public virtual Branch Branch { get; set; }
    }
}
