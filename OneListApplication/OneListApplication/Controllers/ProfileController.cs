using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OneListApplication.Controllers
{
    public class ProfileController : Controller
    {
        const string EMAIL_CONFIRMATION = "EmailConfirmation";
        const string PASSWORD_RESET = "ResetPassword";
        void CreateTokenProvider(UserManager<IdentityUser> manager, string tokenType)
        {
            manager.UserTokenProvider = new EmailTokenProvider<IdentityUser>();
        }
        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult ChangePassword()
        //{
        //    return View();
        //}
        [HttpGet]
        public ActionResult ChangePassword()
        {
            //var user = UserManager.FindById(User.Identity.GetUserId());
            var userStore = new UserStore<IdentityUser>();
            UserManager<IdentityUser> manager = new UserManager<IdentityUser>(userStore);
            var user = manager.FindByName(User.Identity.Name);
            CreateTokenProvider(manager, PASSWORD_RESET);

            var code = manager.GeneratePasswordResetToken(user.Id);
            ViewBag.PasswordToken = code;
            ViewBag.UserID = user.Id;
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(string currentPassword, string password, string passwordConfirm,
                                          string passwordToken, string userID)
        {

            var userStore = new UserStore<IdentityUser>();
            UserManager<IdentityUser> manager = new UserManager<IdentityUser>(userStore);
            var user = manager.FindById(userID);
            CreateTokenProvider(manager, PASSWORD_RESET);
            var compareResult = manager.PasswordHasher.VerifyHashedPassword(user.PasswordHash, currentPassword);
            if (!(compareResult == PasswordVerificationResult.Success))
            {
                ViewBag.Result = "The current password is incorrect.";

                return View();
            }

            else if (password == passwordConfirm)
            {
                IdentityResult result = manager.ResetPassword(userID, passwordToken, password);
                if (result.Succeeded)
                {
                    ViewBag.Result = "The password has been reset.";
                }
                else
                {
                    ViewBag.Result = "The password has not been reset.";
                }
            }

            else
                ViewBag.Result = "Two passwords don't match!";
            return View();
        }
        public ActionResult Edit()
        {
            return View();
        }
    }
}