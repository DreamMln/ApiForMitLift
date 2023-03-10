using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiForMitLift.Models
{
    public class CorolabPraktikDBContext : DbContext
    {
        public static readonly string Connectionstring = "Server=tcp:corolabserver.database.windows.net,1433;Initial Catalog=corolabDB;Persist Security Info=False;User ID=CloudSA44cfc0bb;Password=Corolab1;";
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
