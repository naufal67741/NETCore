using NETCore.Context;
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
            string bodyEmail = $"Kamu lupa password ? Kalau iya, klik di sini reset-password/email={checkEmail.Email}&token={checkEmail.NIK}, else abaikan";
            Email(bodyEmail,checkEmail.Email);
            return 1;
        }

        public int ResetPassword(string email, string NIK)
        {
            //return 100 = NIK salah
            //return 200 = email salah
            var checkEmail = myContext.Persons.Where(e => e.Email == email).FirstOrDefault();
            if (checkEmail == null)
            {
                return 200;
            }
            if (checkEmail.NIK != NIK)
            {
                return 100;
            }
            var account = myContext.Accounts.Where(n => n.NIK == checkEmail.NIK).FirstOrDefault();
            if (account == null)
            {
                return 100;
            }
            account.Password = Guid.NewGuid().ToString();
            /*myContext.SaveChanges();*/
            Update(account);
            //kirim email
            return 1;
        }
    }
}
