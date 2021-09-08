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

            string newPassword = Guid.NewGuid().ToString();
            account.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            Update(account);

            string bodyEmail = $"Password baru Anda: {newPassword}, Jangan sebarkan dan segera lakukan change password";
            Email(bodyEmail, checkEmail.Email);
            return 1;
        }

        /*public int ResetPassword(string email, string token)
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
            string newPassword = Guid.NewGuid().ToString();
            account.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            *//*myContext.SaveChanges();*//*
            Update(account);
            checkEmail.Token = null;
            myContext.SaveChanges();

            string bodyEmail = $"Reset password berhasil ! Sekarang password kamu adalah : {newPassword}, Jangan disebar ya !";
            Email(bodyEmail, checkEmail.Email);

            return 1;
        }*/

        public int ChangePassword(ChangePasswordVM cpVM)
        {
            //return 100 = old password salah/wrong password
            //return 200 = no email/account
            //return 300 = confirmation password doesnt match
            var checkEmail = myContext.Persons.Where(e => e.Email == cpVM.Email).FirstOrDefault();
            if (checkEmail == null)
            {
                return 200;
            }
            var account = myContext.Accounts.Where(n => n.NIK == checkEmail.NIK).FirstOrDefault();
            if (account == null)
            {
                return 200;
            }
            if(!BCrypt.Net.BCrypt.Verify(cpVM.OldPassword, account.Password))
            {
                return 100;
            }
            if(cpVM.NewPassword != cpVM.ConfirmNewPassword)
            {
                return 300;
            }
            account.Password = BCrypt.Net.BCrypt.HashPassword(cpVM.NewPassword);
            Update(account);
            return 1;
        }
    }
}
