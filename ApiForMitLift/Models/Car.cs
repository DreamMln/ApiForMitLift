using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace ApiForMitLift.Models
{
    [Table("Cars")]
    public partial class Car
    {
        public Car()
        {
            CarRides = new HashSet<CarRide>();
        }


        [Key]
        [Column("CarID")]
        public int CarId { get; set; }
        [Column("AccountID")]
        public int AccountId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string FuelType { get; set; }
        //test
        [ForeignKey(nameof(AccountId))]
        [InverseProperty("Cars")]
        public virtual Account Account { get; set; }

        [JsonIgnore]
        [InverseProperty(nameof(CarRide.Cars))]
        public virtual ICollection<CarRide> CarRides { get; set; }

    }
}
