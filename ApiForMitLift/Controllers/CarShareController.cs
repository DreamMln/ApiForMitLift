using ApiForMitLift.Manager;
using ApiForMitLift.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiForMitLift.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarShareController : Controller
    {
        private readonly CarDBManager _dbManager;

        public CarShareController(CorolabPraktikDBContext context)
        {
            _dbManager = new CarDBManager(context);

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("/Api/Accounts")]
        public ActionResult<List<Account>> GetAllAccounts()
        {
            IEnumerable<Account> accounts = _dbManager.GetAllAccounts();

            if (!accounts.Any())
            {
                return NotFound("No accounts here");
            }
            return Ok(accounts);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("/Api/Accounts/{id}")]
        // GET: CarShareController
        public ActionResult<Account> GetAccountById(int id)
        {
            Account account = _dbManager.GetAccountById(id);

            if (account == null)
            {
                return NotFound("No account with this id: " + id);
            }
            return Ok(account);
        }

        [HttpPost]
        [Route("/Api/Accounts")]
        public ActionResult<Account> PostAccount([FromBody] Account newAccount)
        {
            Account createdAccount = _dbManager.AddAccount(newAccount);
            return Ok(createdAccount);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete]
        [Route("/Api/Accounts/{id}")]
        public ActionResult<Account> DeleteAccount(int id)
        {
            Account account = _dbManager.GetAccountById(id);
            if (account == null)
            {
                return NotFound("No account with that id " + id);
            }
            return _dbManager.DeleteAccount(id);
        }

        [HttpPut]
        [Route("/Api/Accounts/{id}")]
        public Account PutAccount(int id, [FromBody] Account value)
        {
            if (value == null)
            {
                NotFound("no account with this id");
            }
            return _dbManager.UpdateAccount(id, value);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("/Api/Cars")]
        // GET: CarShareController
        public ActionResult<List<Car>> GetAllCars()
        {
            IEnumerable<Car> cars = _dbManager.GetAllCars();

            if (!cars.Any())
            {
                return NotFound("No cars here");
            }
            return Ok(cars);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("/Api/Cars/{id}")]
        // GET: CarShareController
        public ActionResult<Car> GetCarByID(int id)
        {
            Car car = _dbManager.GetCarById(id);

            if (car == null)
            {
                return NotFound("No car with this id: " + id);
            }
            return Ok(car);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // DELETE api/<CarShareController>/5
        [HttpDelete]
        [Route("/Api/Cars/{id}")]
        public ActionResult<Car> DeleteCar(int id)
        {
            Car result = _dbManager.DeleteCar(id);
            if (result == null)
            {
                return NotFound("There is no cars with, ID" + id);
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("/Api/Cars")]
        public ActionResult<Car> PostCar([FromBody] Car newCar)
        {
            Car createdCar = _dbManager.AddCar(newCar);
            return Ok(createdCar);
        }

        // PUT api/<CarShareController>/5
        [HttpPut]
        [Route("/Api/Cars/{id}")]
        public Car PutCar(int id, [FromBody] Car value)
        {
            return _dbManager.UpdateCar(id, value);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("/Api/CarRides")]
        // GET: CarShareController
        public ActionResult<List<CarRide>> GetAllCarRides([FromQuery] DateTime? dateAndTimeFilter)
        {
            IEnumerable<CarRide> carRides = _dbManager.GetAllCarRides(dateAndTimeFilter);

            if (!carRides.Any())
            {
                return NotFound("No car rides here :(");
            }
            return Ok(carRides);
        }

        

        

        

    }
}
