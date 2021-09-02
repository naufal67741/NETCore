﻿using NETCore.Context;
using NETCore.Models;
using NETCore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext myContext;
        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public int ForgetPassword(string email)
        {
            /*Guid myuuid = Guid.NewGuid().ToString();
            string newPassword = myuuid.ToString();*/
            //100 = email ga ketemu
            var checkEmail = myContext.Persons.Where(e => e.Email == email).FirstOrDefault();
            if (checkEmail == null)
            {
                return 100;
            }
            var account = myContext.Accounts.Where(n => n.NIK == checkEmail.NIK).FirstOrDefault();
            if (account == null)
            {
                return 100;
            }
            /*account.Password = Guid.NewGuid().ToString();*/
            checkEmail.Token = Guid.NewGuid().ToString();

            string bodyEmail = $"Kamu lupa password ? Kalau iya, klik di sini reset-password/email={checkEmail.Email}&token={checkEmail.Token}, else abaikan";
            Email(bodyEmail,checkEmail.Email);
            myContext.SaveChanges();
            return 1;
        }

        public int ResetPassword(string email, string token)
        {
            //return 100 = NIK salah
            //return 200 = email salah
            var checkEmail = myContext.Persons.Where(e => e.Email == email).FirstOrDefault();
            if (checkEmail == null)
            {
                return 200;
            }
            if (checkEmail.Token != token)
            {
                return 100;
            }
            var account = myContext.Accounts.Where(n => n.NIK == checkEmail.NIK).FirstOrDefault();
            if (account == null)
            {
                return 100;
            }
            account.Password = BCrypt.Net.BCrypt.HashPassword(Guid.NewGuid().ToString());
            Update(account); checkEmail.Token = null;
            myContext.SaveChanges();

            return 1; //kirim email
        }
    }
}
