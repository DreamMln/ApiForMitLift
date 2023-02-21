﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiForMitLift.Models
{
    [Table("Car")]
    public partial class Car
    {
        [Key]
        [Column("CarID")]
        public int CarId { get; set; }
        [Column("AccountID")]
        public int AccountId { get; set; }
        [Required]
        [StringLength(255)]
        public string StartDestination { get; set; }
        [Required]
        [StringLength(255)]
        public string EndDestination { get; set; }
        [Column(TypeName = "date")]
        public DateTime DriveDate { get; set; }
        public decimal Price { get; set; }
        public int AvailableSeats { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string FuelType { get; set; }
        public bool IsFull { get; set; }

        [ForeignKey(nameof(AccountId))]
        [InverseProperty("Cars")]
        public virtual Account Account { get; set; }
    }
}
