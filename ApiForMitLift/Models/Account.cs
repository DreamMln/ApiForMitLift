using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiForMitLift.Models
{
    [Table("Accounts")]
    public partial class Account
    {
        //Constructoren til vores Account objekt. Vi bruger HashSet<Car>() til 
        public Account()
        {
            Cars = new HashSet<Car>();

        }
        //Vores primary key, som gør at alle accounts har et unikt Id. Dette er automatiseret i databasen med IDENTITY (1, 1).
        [Key]
        [Column("AccountID")]
        public int AccountId { get; set; }
       // [Required]
        [StringLength(255)]
        public string UserName { get; set; }
        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }
        //[Required]
        [StringLength(255)]
        public string UserAddress { get; set; }
        public int Phone { get; set; }
        //[Required]
        [StringLength(255)]
        public string Email { get; set; }

        [JsonIgnore]
        [InverseProperty(nameof(Car.Account))]
        public virtual ICollection<Car> Cars { get; set; }
    }
}
