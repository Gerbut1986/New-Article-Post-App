namespace NewsPortal.PL.Controllers
{
    #region Namespaces:
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using NewsPortal.PL.Models;
    using System.Threading.Tasks;
    using Microsoft.Owin.Security;
    using Microsoft.AspNet.Identity;
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity.Owin;
    using NewsPortal.PL._829528a_441d_484m_862i_22475963ffdn;
    #endregion

    [Authorize]
    public class AccountController : Controller
    {
        #region Fields:
        public static List<ApplicationUser> AllUsers { get; private set; }
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private string CurrentUserEmail { get; set; }
        #endregion

        #region Constructors:
        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        #endregion

        #region SignInManager & UserManager:
        public ApplicationSignInManager SignInManager
        {
            get => _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            private set => _signInManager = value;
        }

        public ApplicationUserManager UserManager
        {
            get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            private set => _userManager = value;
        }
        #endregion

        #region Login:
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            try
            {
                AllUsers = UserManager.Users.ToList();
                var lastEnter = AllUsers.OrderByDescending(de => de.DateEnter).First();
                if(lastEnter.RememberMe == true)
                {
                    return View(new LoginViewModel
                    {
                        Email = lastEnter.Email,
                        Password = lastEnter.PasswordHash,
                        RememberMe = lastEnter.RememberMe
                    });
                }
            }
            catch { }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                if (model.Email == null && model.Password == null) ViewBag.Error = "Login and Password Fields have to be Filled out!";
                else ViewBag.Error = "Some of the fields are EMPTY!";
                return View(model);
            }
            else
            {
                AllUsers = UserManager.Users.ToList();
                ApplicationUser signedUser = UserManager.FindByEmail(model.Email);
                if (signedUser == null) // if login and password doesn't exists
                {
                    ViewBag.Error = "Your Login and/or Password is Incorrect!";
                    return View(model);
                }

                SignInStatus result = await SignInManager.PasswordSignInAsync(signedUser.Email, model.Password, model.RememberMe, shouldLockout: false);

                switch (result)
                {
                    case SignInStatus.Success:
                        switch (signedUser.Role)
                        {
                            case "Guest":
                                break;
                            case "Admin":
                                signedUser.DateEnter = DateTime.Now;
                                break;
                            case "RegisterUser":
                                signedUser.DateEnter = DateTime.Now;
                                break;
                            case "Unknown":
                                break;
                            default: break;
                        }
                        return RedirectToLocal(returnUrl);
                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                    case SignInStatus.Failure:
                    default:
                        ViewBag.Error = "This Email or Password does not exist or the user is not confirmed!";
                        return View(model);
                }
            }
        }
        #endregion

        #region Register:
        [AllowAnonymous]
        public ActionResult Register()
        {
            ViewBag.Genders = new SelectList(new string[] { "Male", "Female" });
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                int cntId = 0;
                try
                {
                    AllUsers = UserManager.Users.ToList();
                    cntId = AllUsers.Count;
                }
                catch (Exception ex) { ViewBag.Error = ex.Message; }
                var user = new ApplicationUser
                {
                    UserId = ++cntId,
                    UserName = model.Email,
                    Email = model.Email,
                    Gender = model.Gender,
                    LastName = model.LastName,
                    FirstName = model.FirstName,
                    Role = Role.RegisterUser.ToString(),
                    DateRegister = DateTime.Now
                };
                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    using (var sw = new System.IO.StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "StoragePass.txt", true))
                        sw.WriteLine(DateTime.Now + " => " + model.Email + " | " + model.Password);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AddErrors(result);
                    ViewBag.Error = result.Errors.ToArray()[0];
                }
            }
            ViewBag.Genders = new SelectList(new string[] { "Male", "Female" });
            return View(model);
        }
        #endregion

        #region Forgot, Reset Password:
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }
        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }

            //private string GetUserPassword()
            //{
            //    string[] fromFile = default, eachLineSplt = default;
            //    var list = new List<Credential>();
            //    fromFile =
            //           System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "StoragePass.txt");
            //    eachLineSplt = new string[6]; var cnt = 0;
            //    for (var i = 0; i < fromFile.Length; i++)
            //    {
            //        eachLineSplt = fromFile[i].Split(new char[] { ' ' });
            //        var cred = new Credential();
            //        cred.RecordDate = Convert.ToDateTime(eachLineSplt[0] + " " + eachLineSplt[1]);
            //        cred.Email = eachLineSplt[4];
            //        cred.Password = eachLineSplt[6];
            //        cred.Id = ++cnt;
            //        list.Add(cred);
            //    }

            //}
        }
        #endregion      
    }
}