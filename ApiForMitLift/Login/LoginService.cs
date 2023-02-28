using ApiForMitLift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiForMitLift.Login
{
    public class LoginService
    {
        private readonly CorolabPraktikDBContext _corolabContext;

        //sætte password i account tabellen?
        public bool CreateLogin(string email, string password) 
        {
            _corolabContext.ChangeTracker.Clear();
            //object af account
            Account newAccountLogin = new Account();
            newAccountLogin.Email = email;
            //newAccount.Password = password;
            try
            {
                _corolabContext.Add(newAccountLogin);
                _corolabContext.SaveChanges();
            }
            catch (Exception)
            {
                throw new ArgumentException("Error!");
            }
            return true;

        }
        public Account GetLogin(int id)
        {
            return _corolabContext.Accounts.Find(id);
        }

    }
}
