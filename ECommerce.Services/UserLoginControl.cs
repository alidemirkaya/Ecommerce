using ECommerce.Core.Entities;
using ECommerce.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services
{
    public class UserLoginControl
    {
        public string GetHash(string salt,string password)
        {
            System.Security.Cryptography.SHA1 encrypt = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            var encrypted = encrypt.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password + salt));
            var stringBuilder = new System.Text.StringBuilder(encrypted.Length * 2);
            foreach (byte b in encrypted)
            {
                stringBuilder.Append(b.ToString("x2"));
            }
            string hashPassword = stringBuilder.ToString();
            return hashPassword;
        }
        public bool checkLogin(ECommerce.WebUI.Domain.UserLogin userLogin,string salt)
        {            
            var dc = new ECommerceDbContext();
            string tempPassword = GetHash(salt, userLogin.Password);
            var queryLoginDetails = from user in dc.Users
                                    where user.Email == userLogin.Email && user.Password == tempPassword
                                    select user;
            if (queryLoginDetails.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string FindUser(ECommerce.WebUI.Domain.UserLogin userLogin,string salt)
        {            
            bool checkUser=checkLogin(userLogin,salt);
            if (checkUser)
            {
                string tempPassword = GetHash(salt, userLogin.Password);
                var dc = new ECommerceDbContext();
                var queryLoginDetails = (from user in dc.Users
                                         where user.Email == userLogin.Email && user.Password == tempPassword
                                         select user).FirstOrDefault();
                return queryLoginDetails.Email;
            }
            else
            {
                return "Not found";
            }
        }
        public void UserCreate(WebUI.Domain.UserSignUp userSignUp,string salt)
        {
            var dc = new ECommerceDbContext();
            User user = new User
            {
                 Email=userSignUp.Email,
                  FirstName=userSignUp.FirstName,
                   LastName=userSignUp.LastName,
                    Password=GetHash(salt,userSignUp.Password),
            };
            dc.Users.Add(user);
            dc.SaveChanges();
        }
        public bool CheckUser(string eMail)
        {
            var dc = new ECommerceDbContext();
            return dc.Users.Any(x => x.Email == eMail);
        }
        public User GetUser(string eMail)
        {
           var dc = new ECommerceDbContext();
           return  dc.Users.FirstOrDefault(x => x.Email == eMail);
        }
        public bool GetChangePassword(ECommerce.WebUI.Domain.UserChangePassword userChangePassword,string salt)
        {
            string tempPassword = GetHash(userChangePassword.OldPassword, salt);
            string tempNewPassword = GetHash(userChangePassword.NewPassword, salt);
            var dc = new ECommerceDbContext();
            if (dc.Users.Any(x => x.Email == userChangePassword.Email && x.Password==tempPassword))
            {
                var findUser = dc.Users.FirstOrDefault(x => x.Email == userChangePassword.Email);
                findUser.Password = tempNewPassword;
                dc.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }             
    }
}
