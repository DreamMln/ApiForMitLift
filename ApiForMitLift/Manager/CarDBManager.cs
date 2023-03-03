using ApiForMitLift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiForMitLift.Manager
{
    public class CarDBManager
    {
        private readonly CorolabPraktikDBContext _corolabContext;

        public CarDBManager(CorolabPraktikDBContext context)
        {
            _corolabContext = context;

        }

        //Vi henter en liste af Accounts
        public List<Account> GetAllAccounts()
        {
            using (var context = _corolabContext)
            {
                var accounts = context.Accounts.ToList();

                return accounts;
            }
        }

        //Her tilføjer vi en ny account til vores database tabel. Værdierne til hvad Account indeholder er i vores Account.cs klasse i model folderen.
        public Account AddAccount(Account addAccount)
        {
            _corolabContext.Accounts.Add(addAccount);
            _corolabContext.SaveChanges();
            return addAccount;

        }

        //Vi finder en specifik account ud fra accountets id
        public Account GetAccountById(int id)
        {
            return _corolabContext.Accounts.Find(id);
        }

        //Account Delete
        public Account DeleteAccount(int id)
        {
            Account account = _corolabContext.Accounts.Find(id);
            // Car car = _corolabContext.Cars.Find(id);
            _corolabContext.Accounts.Remove(account);
            _corolabContext.SaveChanges();
            return account;
        }
        public Account UpdateAccount(int id, Account updates)
        {
            Account account = _corolabContext.Accounts.Find(id);
            account.UserName = updates.UserName;
            account.DateOfBirth = updates.DateOfBirth;
            account.UserAddress = updates.UserAddress;
            account.Phone = updates.Phone;
            account.Email = updates.Email;
            _corolabContext.SaveChanges();
            return account;
        }

        //Vi henter den fulde liste af biler.Her bruger vi using (var context) for kun at hente listen med biler
        public List<Car> GetAllCars()
        {


        using (var context = _corolabContext)
        {
            //Vi sørger for, at vi kan hente Car klassen og dens indhold.
            List<Car> result = new List<Car>(_corolabContext.Cars);
            return result.ToList();
        }
        }

        //car from body car, accouúntid, slå account op for at finde - accountid som en int
        //kald getbyid account
        public Car AddCar(Car addCar)
        {
            Account account = GetAccountById(addCar.AccountId);

            if (account == null)
            {
                return null;
            }
            account.Cars.Add(addCar);
            addCar.Account = account;
            _corolabContext.SaveChanges();
            return addCar;
        }


        //Vi finder en specifik bil ud fra bilens id
        public Car GetCarById(int id)
        {
            return _corolabContext.Cars.Find(id);
        }




        //Car Delete
        public Car DeleteCar(int id)
        {
            Car car = _corolabContext.Cars.Find(id);
            // Car car = _corolabContext.Cars.Find(id);
            _corolabContext.Cars.Remove(car);
            _corolabContext.SaveChanges();
            return car;
        }



        ////Vi opdaterer Car og dens værdier, vi kan ikke opdatere Car.Id, da det er primary key. Det samme gælder for Account.Id, da en primary key skal være unik.
        public Car UpdateCar(int id, Car updates)
        {
            Car car = _corolabContext.Cars.Find(id);
            car.Brand = updates.Brand;
            car.Model = updates.Model;
            car.FuelType = updates.FuelType;
            _corolabContext.SaveChanges();
            return car;
        }

        public List<CarRide> GetAllCarRides(DateTime? dateAndTimeFilter)
        {
            using (var context = _corolabContext)
            {
                //Vi sørger for, at vi kan hente Car klassen og dens indhold.
                List<CarRide> result = new List<CarRide>(_corolabContext.CarRides);


                //Vi sørger for at vi filtrerer i listen, så der kun bliver de biler, hvor der stadig er ledige pladser, da en fyldt bil ikke har nogen interesse for brugeren.
                result = result.FindAll(filterItem => filterItem.IsFull.Equals(false)); //ISFULL filtrering


                if (dateAndTimeFilter != null) // DATO filtrering
                {
                    //dato skal ligge i dette if statement, da objektet ellers vil være null, hvis man forsøger at finde alle biler frem uden filtreringen.
                    //vi filtrerer efter dato, hvor vi derefter tjekker at datoen man indtaster passer med det i databasen.
                    var dato = dateAndTimeFilter.Value.Date;
                    result = result.FindAll(filterItem => filterItem.DriveDate.Equals(dato));
                }
                return result.ToList();
            }
        }

        public CarRide AddCarRide(CarRide addRide)
        {
            Car car = GetCarById(addRide.CarId);

            if (car == null)
            {
                return null;
            }
            car.CarRides.Add(addRide);
            addRide.Cars = car;
            _corolabContext.SaveChanges();
            return addRide;
        }


        //Vi finder en specifik bil ud fra bilens id
        public CarRide GetCarRideById(int id)
        {
            return _corolabContext.CarRides.Find(id);
        }




        //Car Delete
        public CarRide DeleteCarRide(int id)
        {
            CarRide carRide = _corolabContext.CarRides.Find(id);
            // Car car = _corolabContext.Cars.Find(id);
            _corolabContext.CarRides.Remove(carRide);
            _corolabContext.SaveChanges();
            return carRide;
        }



        ////Vi opdaterer Car og dens værdier, vi kan ikke opdatere Car.Id, da det er primary key. Det samme gælder for Account.Id, da en primary key skal være unik.
        public CarRide UpdateCarRide(int id, CarRide updates)
        {
            CarRide carRide = _corolabContext.CarRides.Find(id);
            carRide.DriveDate = updates.DriveDate;
            carRide.StartDestination = updates.StartDestination;
            carRide.EndDestination = updates.EndDestination;
            carRide.Price = updates.Price;
            carRide.AvailableSeats = updates.AvailableSeats;
            carRide.IsFull = updates.IsFull;
            _corolabContext.SaveChanges();
            return carRide;
        }

        public Account GetAccountByEmail(string email)
        {
            return _corolabContext.Accounts.Where(Account => Account.Email == email).FirstOrDefault();
        }






    }
}
