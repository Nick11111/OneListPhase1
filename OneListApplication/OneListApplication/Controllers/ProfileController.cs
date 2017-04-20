using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OneListApplication.ViewModels;
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
            var username = User.Identity.Name;
            OneListEntitiesCore db = new OneListEntitiesCore();
            User user = db.Users.Where(a => a.UserName == username).FirstOrDefault();
            UserVM userForView = new UserVM();
            userForView.FirstName = user.FirstName;
            userForView.LastName = user.LastName;
            userForView.UserName = user.UserName;
            userForView.Email = user.Email;
            return View(userForView);
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
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
                ViewBag.Fail = "The current password is incorrect.";

                return View();
            }

            else if (password == passwordConfirm)
            {
                IdentityResult result = manager.ResetPassword(userID, passwordToken, password);
                if (result.Succeeded)
                {
                    ViewBag.Success = "The password has been reset!";
                }
                else
                {
                    ViewBag.Fail = "Failed, password has to be at least 6 characters!"; 
                }
            }

            else
                ViewBag.Fail = "Two passwords don't match!";
            return View();
        }

        [HttpPost]
        public ActionResult Edit(UserVM userInput)
        {
            if (userInput.FirstName!=null && userInput.LastName!=null)
            {
                OneListEntitiesCore db = new OneListEntitiesCore();
                User user = db.Users.Where(a => a.UserName == User.Identity.Name).FirstOrDefault();
                user.FirstName = userInput.FirstName;
                user.LastName = userInput.LastName;
                db.SaveChanges();
                TempData["Success"] = "Updated successfully!";
            }
            else {
                TempData["Fail"] = "Failed to update!";
            }
            return RedirectToAction("Index", "Profile");
        }
    }
}