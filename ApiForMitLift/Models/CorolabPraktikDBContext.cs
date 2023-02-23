using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiForMitLift.Models
{
    public class CorolabPraktikDBContext : DbContext
    {
        public static readonly string Connectionstring = "Server=tcp:praktikdbserver.database.windows.net,1433;Initial Catalog=MitLiftDB;Persist Security Info=False;User ID=Praktik2023;Password=Corolab1;";
        //public CorolabPraktikDBContext()
        //{
        //}

        public CorolabPraktikDBContext(DbContextOptions<CorolabPraktikDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<CarRide> CarRides { get; set; }

    }
}
