using ECommerce.Core.Entities;
using ECommerce.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;

namespace ECommerce.Web.Controllers
{
    public class SecurityController : Controller
    {
        private readonly string salt = "encryptedkey";
        private readonly UserLoginControl _userLoginControl;
        public SecurityController()
        {
            _userLoginControl = new UserLoginControl();
        }
        // GET: Security
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(ECommerce.WebUI.Domain.UserLogin userLogin,bool? Remember)
        {
            var userControl = _userLoginControl.FindUser(userLogin,salt);
            if (userControl != "Not found")
            {
                if(Remember.HasValue || Remember == true) 
                { 
                    FormsAuthentication.SetAuthCookie(userControl, true); 
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(userControl, false);
                }
                TempData.Add("Login", "Login succesful. Welcome "+_userLoginControl.GetUser(userControl).FirstName);
                return RedirectToAction("Index", "Order");
            }
            else
            {
                TempData.Add("Alert", "Username or password is wrong");
                return View();
            }        
        }
        public ActionResult SignUp()
        {
            // Forms Authantication get current user
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(ECommerce.WebUI.Domain.UserSignUp userSignUp)
        {
            if (ModelState.IsValid)
            {
                if (_userLoginControl.CheckUser(userSignUp.Email))
                {
                    TempData.Add("Alert", "User is already registered");
                    return View();
                }
                else
                {
                    _userLoginControl.UserCreate(userSignUp, salt);
                    TempData.Add("Login", "Your registration has been successfully ");
                    return RedirectToAction("Login", "Security");
                }
                
            }
            return View();
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Security");
        }
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ECommerce.WebUI.Domain.UserChangePassword userChangePassword)
        {
            if (ModelState.IsValid)
            {
                if(_userLoginControl.GetChangePassword(userChangePassword, salt))
                {
                    TempData.Add("Login", "Password successfully changed");
                    return RedirectToAction("Login", "Security");
                }
                else
                {
                    TempData.Add("Alert", "E-mail address or password is incorrect");
                    return View();
                }
            }
            return View();
        }
    }
}