
using ApiForMitLift.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ApiForMitLift.Login
{
    public class LoginSessionService
    {
        private readonly CorolabPraktikDBContext _DbContext;

        public Account GetAccountFromSession(HttpContext httpContext)
        {
            if (httpContext.Session.GetInt32("AccountId").HasValue)
            {
                return _DbContext.Accounts.Where(a => a.AccountId == httpContext.Session.GetInt32("AccountId")).FirstOrDefault();
            }
            return null;
        }

        public bool IsLoggedIn(HttpContext httpContext)
        {
            return httpContext.Session.GetInt32("AccountId").HasValue;
        }

    }
}
