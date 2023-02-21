﻿using ApiForMitLift.Models;
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

        //Vi henter den fulde liste af biler.Her bruger vi using (var context) for kun at hente listen med biler
        public List<Car> GetAllCars(DateTime? dateTimeFilter)
        {


        using (var context = _corolabContext)
        {
            //Vi sørger for, at vi kan hente Car klassen og dens indhold.
            List<Car> result = new List<Car>(_corolabContext.Cars);


            //Vi sørger for at vi filtrerer i listen, så der kun bliver de biler, hvor der stadig er ledige pladser, da en fyldt bil ikke har nogen interesse for brugeren.
            result = result.FindAll(filterItem => filterItem.IsFull.Equals(false)); //ISFULL filtrering


            if (dateTimeFilter != null) // DATO filtrering
            {
                //dato skal ligge i dette if statement, da objektet ellers vil være null, hvis man forsøger at finde alle biler frem uden filtreringen.
                //vi filtrerer efter dato, hvor vi derefter tjekker at datoen man indtaster passer med det i databasen.
                var dato = dateTimeFilter.Value.Date;
                result = result.FindAll(filterItem => filterItem.DriveDate.Equals(dato));
            }
            return result.ToList();
        }
    }

        //Vi finder en specifik account ud fra accountets id
        public Account GetAccountById(int id)
        {
            return _corolabContext.Accounts.Find(id);
        }
        //Vi finder en specifik bil ud fra bilens id
        public Car GetCarById(int id)
        {
            return _corolabContext.Cars.Find(id);
        }

        //Her tilføjer vi en ny account til vores database tabel. Værdierne til hvad Account indeholder er i vores Account.cs klasse i model folderen.
        public Account AddAccount(Account addAccount)
        {
            using (var context = _corolabContext)
            {
                var accounts = context.Accounts.Add(addAccount);
                _corolabContext.SaveChanges();
                return addAccount;
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
        //Rental Car Delete
        public Account DeleteAccount(int id)
        {
            Account account = _corolabContext.Accounts.Find(id);
            // Car car = _corolabContext.Cars.Find(id);
            _corolabContext.Accounts.Remove(account);
            _corolabContext.SaveChanges();
            return account;
        }
        public Car DeleteCar(int id)
        {
            Car car = _corolabContext.Cars.Find(id);
            // Car car = _corolabContext.Cars.Find(id);
            _corolabContext.Cars.Remove(car);
            _corolabContext.SaveChanges();
            return car;
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
        ////Vi opdaterer Car og dens værdier, vi kan ikke opdatere Car.Id, da det er primary key. Det samme gælder for Account.Id, da en primary key skal være unik.
        public Car UpdateCar(int id, Car updates)
        {
            Car car = _corolabContext.Cars.Find(id);
            car.StartDestination = updates.StartDestination;
            car.EndDestination = updates.EndDestination;
            car.DriveDate = updates.DriveDate;
            car.Price = updates.Price;
            car.AvailableSeats = updates.AvailableSeats;
            car.Brand = updates.Brand;
            car.Model = updates.Model;
            car.FuelType = updates.FuelType;
            car.IsFull = updates.IsFull;
            _corolabContext.SaveChanges();
            return car;
        }

    }
}
