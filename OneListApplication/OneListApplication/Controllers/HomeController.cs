using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.IO;
using OneListApplication.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Security.Claims;
using OneListApplication.Service;

namespace OneListApplication.Controllers
{
    public class HomeController : Controller
    {
        const string EMAIL_CONFIRMATION = "EmailConfirmation";
        const string PASSWORD_RESET = "ResetPassword";

        void CreateTokenProvider(UserManager<IdentityUser> manager, string tokenType)
        {
            manager.UserTokenProvider = new EmailTokenProvider<IdentityUser>();
        }
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.date = GetBuildDate();
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginVM login, string rememberMe)
        {
            ViewBag.ErrorMessage = "";
            // UserStore and UserManager manages data retreival.
            UserStore<IdentityUser> userStore = new UserStore<IdentityUser>();
            UserManager<IdentityUser> manager = new UserManager<IdentityUser>(userStore);
            OneListCAEntities context = new OneListCAEntities();

            if (ModelState.IsValid)
            {
                IdentityUser identityUser = manager.Find(login.UserName, login.Password);
                var user = context.AspNetUsers.Find(identityUser.Id);

                if (ValidLogin(login))
                {
                    if (user.PhoneNumberConfirmed == true)
                    {
                        ViewBag.ErrorMessage = "Your account has been banned,please contact admin for further actions!";
                        return View();
                    }
                    else {

                        IAuthenticationManager authenticationManager
                       = HttpContext.GetOwinContext()
                        .Authentication;
                        authenticationManager
                       .SignOut(DefaultAuthenticationTypes.ExternalCookie);

                        var identity = new ClaimsIdentity(new[] {
                                            new Claim(ClaimTypes.Name, login.UserName),
                                        },
                                            DefaultAuthenticationTypes.ApplicationCookie,
                                            ClaimTypes.Name, ClaimTypes.Role);
                        // SignIn() accepts ClaimsIdentity and issues logged in cookie. 
                        if (rememberMe == "true")
                        {
                            authenticationManager.SignIn(new AuthenticationProperties
                            {
                                IsPersistent = true
                            }, identity);
                        }
                        else {
                            authenticationManager.SignIn(new AuthenticationProperties
                            {
                                IsPersistent = false
                            }, identity);
                        }
                        return RedirectToAction("Home", "Home");
                    }

                }
                else {
                    ViewBag.ErrorMessage = "Login failed, please try again!";

                }
            }
            return View();
        }

        bool ValidLogin(LoginVM login)
        {
            UserStore<IdentityUser> userStore = new UserStore<IdentityUser>();
            UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(userStore)
            {
                UserLockoutEnabledByDefault = true,
                DefaultAccountLockoutTimeSpan = new TimeSpan(0, 10, 0),
                MaxFailedAccessAttemptsBeforeLockout = 3
            };
            var user = userManager.FindByName(login.UserName);

            if (user == null)
                return false;

            // User is locked out.
            if (userManager.SupportsUserLockout && userManager.IsLockedOut(user.Id))
                return false;

            // Validated user was locked out but now can be reset.
            if (userManager.CheckPassword(user, login.Password) && userManager.IsEmailConfirmed(user.Id))
            {
                if (userManager.SupportsUserLockout
                 && userManager.GetAccessFailedCount(user.Id) > 0)
                {
                    userManager.ResetAccessFailedCount(user.Id);
                    System.Threading.Thread.Sleep(2000);
                    return true;
                }
                System.Threading.Thread.Sleep(2000);
                return true;
            }
            // Login is invalid so increment failed attempts.
            else
            {
                bool lockoutEnabled = userManager.GetLockoutEnabled(user.Id);
                if (userManager.SupportsUserLockout && userManager.GetLockoutEnabled(user.Id))
                {
                    userManager.AccessFailed(user.Id);
                    return false;
                }
                return false;
            }

        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisteredUserVM newUser)
        {
            var userStore = new UserStore<IdentityUser>();
            UserManager<IdentityUser> manager = new UserManager<IdentityUser>(userStore)
            {
                UserLockoutEnabledByDefault = true,
                DefaultAccountLockoutTimeSpan = new TimeSpan(0, 10, 0),
                MaxFailedAccessAttemptsBeforeLockout = 5
            };
            var identityUser = new IdentityUser()
            {
                UserName = newUser.UserName,
                Email = newUser.Email
            };
            if (ModelState.IsValid)
            {
                CaptchaHelper captchaHelper = new CaptchaHelper();
                OneListCAEntities context = new OneListCAEntities();
                string captchaResponse = captchaHelper.CheckRecaptcha();
                if (captchaResponse == "Valid")
                {
                    var existUser = context.AspNetUsers.Where(u => u.Email == newUser.Email);
                    if (existUser == null)
                    {
                        ViewBag.CaptchaResponse = captchaResponse;
                        IdentityResult result = manager.Create(identityUser, newUser.Password);
                        if (result.Succeeded)
                        {

                            AspNetUser user = context.AspNetUsers
                                                .Where(u => u.UserName == newUser.UserName).FirstOrDefault();
                            AspNetRole role = new AspNetRole();
                            role.Id = "User";
                            role.Name = "User";

                            user.AspNetRoles.Add(context.AspNetRoles.Find(role.Id));
                            context.SaveChanges();
                            //add information of user and password to table users in core
                            CreateTokenProvider(manager, EMAIL_CONFIRMATION);

                            var code = manager.GenerateEmailConfirmationToken(identityUser.Id);
                            var callbackUrl = Url.Action("ConfirmEmail", "Home",
                                                            new { userId = identityUser.Id, code = code },
                                                                protocol: Request.Url.Scheme);

                            string email = "Please confirm your account by clicking this link: <a href=\""
                                            + callbackUrl + "\">Confirm Registration</a>";
                            SendGrid.sendEmail(newUser, callbackUrl);
                            ViewBag.Result = "Please check your email to activate your account!";
                        }
                        else
                        {
                            ViewBag.Result = "User already exist!";
                        }
                    }
                    else {
                        ViewBag.Result = "User already exist!";
                    }

                }
                else {
                    ViewBag.Result = "Registration failed!";
                }     
            }
            
            return View();
        }
        public ActionResult ConfirmEmail(string userID, string code)
        {
            var userStore = new UserStore<IdentityUser>();
            UserManager<IdentityUser> manager = new UserManager<IdentityUser>(userStore);
            var user = manager.FindById(userID);
            CreateTokenProvider(manager, EMAIL_CONFIRMATION);
            try
            {
                IdentityResult result = manager.ConfirmEmail(userID, code);
                if (result.Succeeded)
                    ViewBag.Message = "You are now registered!";
            }
            catch
            {
                ViewBag.Message = "Validation attempt failed!";
            }
            return View();
        }
        [Authorize]
        public ActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddRole(RoleVM roleVM)
        {
            if (ModelState.IsValid)
            {
                AspNetRole role = new AspNetRole();
                role.Id = roleVM.RoleName;
                role.Name = roleVM.RoleName;
                OneListCAEntities context = new OneListCAEntities();
                context.AspNetRoles.Add(role);
                context.SaveChanges();
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult AddUserToRole()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult AddUserToRole(UserRoleVM userRoleVM)
        {
            if (ModelState.IsValid)
            {
                OneListCAEntities context = new OneListCAEntities();
                AspNetUser user = context.AspNetUsers
                                    .Where(u => u.UserName == userRoleVM.UserName).FirstOrDefault();
                AspNetRole role = context.AspNetRoles
                                    .Where(r => r.Name == userRoleVM.RoleName).FirstOrDefault();

                user.AspNetRoles.Add(role);
                context.SaveChanges();
            }
            return View();
        }


        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteUser()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteUser(DeleteUserVM deletedUser)
        {
            if (ModelState.IsValid)
            {
                var userStore = new UserStore<IdentityUser>();
                UserManager<IdentityUser> manager = new UserManager<IdentityUser>(userStore);
                var currentUser = manager.FindByEmail(deletedUser.Email);
                OneListCAEntities context = new OneListCAEntities();

                if (currentUser != null)
                {
                    var user = context.AspNetUsers.Find(currentUser.Id);
                    var userProfile = context.Users.Find(currentUser.Id);
                    context.AspNetUsers.Remove(user);
                    context.Users.Remove(userProfile);
                    context.SaveChanges();
                    ViewBag.Success = "User has been deleted successfully!";
                }
                else
                {
                    ViewBag.Fail = "User not found!";
                }
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult BanUser()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult BanUser(DeleteUserVM deletedUser)
        {
            if (ModelState.IsValid)
            {
                var userStore = new UserStore<IdentityUser>();
                UserManager<IdentityUser> manager = new UserManager<IdentityUser>(userStore);
                var currentUser = manager.FindByEmail(deletedUser.Email);
                OneListCAEntities context = new OneListCAEntities();

                if (currentUser != null)
                {
                    var user = context.AspNetUsers.Find(currentUser.Id);
                    user.PhoneNumberConfirmed = true;
                    context.SaveChanges();
                    ViewBag.Success = "User has been banned successfully!";
                }
                else
                {
                    ViewBag.Fail = "User not found!";
                }
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult UnbanUser()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult UnbanUser(DeleteUserVM deletedUser)
        {
            if (ModelState.IsValid)
            {
                var userStore = new UserStore<IdentityUser>();
                UserManager<IdentityUser> manager = new UserManager<IdentityUser>(userStore);
                var currentUser = manager.FindByEmail(deletedUser.Email);
                OneListCAEntities context = new OneListCAEntities();

                if (currentUser != null)
                {
                    var user = context.AspNetUsers.Find(currentUser.Id);
                    if (user.PhoneNumberConfirmed == true)
                    {
                        user.PhoneNumberConfirmed = false;
                        context.SaveChanges();
                        ViewBag.Success = "User has been unbanned successfully!";
                    }

                    else {
                        ViewBag.Fail = "This user is not banned!";
                    }
                }
                else
                {
                    ViewBag.Fail = "User not found!";
                }
            }
            return View();
        }

        //[Authorize(Roles = "Administrator")]
        // To allow more than one role access use syntax like the following:
        // [Authorize(Roles="Admin, Staff")]
        //public ActionResult AdminOnly()
        //{
        //    return View();
        //}

        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            var userStore = new UserStore<IdentityUser>();
            UserManager<IdentityUser> manager = new UserManager<IdentityUser>(userStore);
            var user = manager.FindByEmail(email);
            if (user != null)
            {
                CreateTokenProvider(manager, PASSWORD_RESET);

                var code = manager.GeneratePasswordResetToken(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Home",
                                             new { userId = user.Id, code = code },
                                             protocol: Request.Url.Scheme);
                ViewBag.FakeEmailMessage = "Please reset your password by clicking <a href=\""
                                         + callbackUrl + "\">here</a>";
                SendGrid.sendResetEmail(user.Email, user.UserName, callbackUrl);
                ViewBag.Success = "Please check your email to complete!";
            }
            else {
                ViewBag.Fail = "Email not found!";
            }
            return View();
        }

        [HttpGet]
        public ActionResult ResetPassword(string userID, string code)
        {
            ViewBag.PasswordToken = code;
            ViewBag.UserID = userID;
            return View();
        }
        [HttpPost]
        public ActionResult ResetPassword(RegisteredUserVM currentUser, 
                                          string passwordToken, string userID)
        {
                CaptchaHelper captchaHelper = new CaptchaHelper();
                string captchaResponse = captchaHelper.CheckRecaptcha();
                ViewBag.CaptchaResponse = captchaResponse;

            if (captchaResponse == "Valid")
            {
                var userStore = new UserStore<IdentityUser>();
                UserManager<IdentityUser> manager = new UserManager<IdentityUser>(userStore);
                var user = manager.FindById(userID);
                CreateTokenProvider(manager, PASSWORD_RESET);

                if (currentUser.Password == currentUser.ConfirmPassword)
                {
                    IdentityResult result = manager.ResetPassword(userID, passwordToken, currentUser.Password);
                    if (result.Succeeded)
                    {
                        ViewBag.Result = "The password has been reset.";
                    }
                    else
                    {
                        ViewBag.Result = "The password has not been reset.";
                    }
                }

            }
            else {
                ViewBag.Result = "The password has not been reset.";
            }
            return View();
        }

    public ActionResult Home()
        {
            ViewBag.UserName = User.Identity.Name;
            return View();
        }

        public ActionResult ListManagement()
        {
           return View();
        }

        public ActionResult SubscriberManagement()
        {
            return View();
        }

        public ActionResult Sidebar()
        {
            return View();
        }

        public static DateTime GetBuildDate()
        {
            UriBuilder uri = new UriBuilder(Assembly.GetExecutingAssembly().CodeBase);
            return OneListApplication.Properties.Settings.Default.LastBuildtime;
        }
        public ActionResult GetDesignDocument()
        {
            return View();
        }
    }
}