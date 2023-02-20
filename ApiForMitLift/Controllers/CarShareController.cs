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
        [Route("/api/Accounts")]
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
        // GET: CarShareController
        public ActionResult<List<Car>> GetAllCars([FromQuery] DateTime? dateTimeFilter)
        {
            IEnumerable<Car> cars = _dbManager.GetAllCars(dateTimeFilter);

            if (!cars.Any())
            {
                return NotFound("No cars here");
            }
            return Ok(cars);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
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

        [HttpPut]
        [Route("/api/UpdateAccountByID/{id}")]
        public Account PutAccount(int id, [FromBody] Account value)
        {
            if (value == null)
            {
                NotFound("no account with this id");
            }
            return _dbManager.UpdateAccount(id, value);
        }

        // PUT api/<CarShareController>/5
        [HttpPut("{id}")]
        public Car PutCar(int id, [FromBody] Car value)
        {
            return _dbManager.UpdateCar(id, value);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // DELETE api/<CarShareController>/5
        [HttpDelete("{id}")]
        public ActionResult<Car> DeleteCar(int id)
        {
            Car result = _dbManager.DeleteCar(id);
            if (result == null)
            {
                return NotFound("There is no cars with, ID" + id);
            }
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete]
        [Route("/api/DeleteAccount/{id}")]
        public ActionResult<Account> DeleteAccount(int id)
        {
            Account account = _dbManager.GetAccountById(id);
            if (account == null)
            {
                return NotFound("No account with that id " + id);
            }
            return _dbManager.DeleteAccount(id);
        }


        [HttpPost]
        [Route("/AccountPost")]
        public ActionResult<Account> PostAccount([FromBody] Account newAccount)
        {
            Account createdAccount = _dbManager.AddAccount(newAccount);
            return Ok(createdAccount);
        }

    }
}
