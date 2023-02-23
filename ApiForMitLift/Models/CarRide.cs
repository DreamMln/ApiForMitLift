using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace ApiForMitLift.Models
{
    [Table("CarRides")]
    public partial class CarRide
    {
        
            [Key]
            [Column("CarRideID")]
            public int CarRideId { get; set; }
            [Column("CarID")]
            public int CarId { get; set; }
            [Column(TypeName = "date")]
            public DateTime DriveDate { get; set; }
            [Required]
            [StringLength(255)]
            public string StartDestination { get; set; }
            [Required]
            [StringLength(255)]
            public string EndDestination { get; set; }
            
            public decimal Price { get; set; }
            public int AvailableSeats { get; set; }
            public bool IsFull { get; set; }
            [ForeignKey(nameof(CarId))]
            [InverseProperty("CarRides")]
            public virtual Car Cars { get; set; }
    }
}