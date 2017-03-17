using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using LMS.Models;
using ServiceLayer.Interface;
using ServiceLayer;
using System.Collections.Generic;

namespace LMS.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {
        private IAccountSvc acctService;

       public AccountController()
        {
            acctService = new AccountSvc();
        }

        public AccountController(IAccountSvc service)
        {
            this.acctService = service;
        }
        [HttpGet]
        [Route("Account/FetchUserMenus")]
        public ActionResult FetchUserMenus()
        {            
            return Json(acctService.getMenus(), JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            Session["loginDetails"] = null;
            return   RedirectToAction("Index", "Home");
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            Session["loginDetails"] = null;
            List<Dictionary<string, object>> result = acctService.Login(model.Email.ToUpper(), model.Password);
            if(result.Count>0)
            {
                if (result[0]["Status"].ToString() == "Open")
                {
                    Session["loginDetails"] = result;
                    return RedirectToAction("Index", "Home", null);
                }
                else if (result.Count > 0)
                {
                    ModelState.AddModelError("", "Account is " + result[0]["Status"].ToString());
                    return View(model);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View(model);
            }
           



            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}

            //// This doesn't count login failures towards account lockout
            //// To enable password failures to trigger account lockout, change to shouldLockout: true
            //var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);


            //switch (result)var 
            //{
            //    case SignInStatus.Success:
            //        return RedirectToLocal(returnUrl);
            //    case SignInStatus.LockedOut:
            //        return View("Lockout");
            //    case SignInStatus.RequiresVerification:
            //        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
            //    case SignInStatus.Failure:
            //    default:
            //        ModelState.AddModelError("", "Invalid login attempt.");
            //        return View(model);
            //}
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (1==0)
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        //{
        //    //if (!ModelState.IsValid)
        //    //{
        //    //    return View(model);
        //    //}

        //    //// The following code protects for brute force attacks against the two factor codes. 
        //    //// If a user enters incorrect codes for a specified amount of time then the user account 
        //    //// will be locked out for a specified amount of time. 
        //    //// You can configure the account lockout settings in IdentityConfig
        //    //var result = null;// await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);
        //    //switch (result)
        //    //{
        //    //    case SignInStatus.Success:
        //    //        return RedirectToLocal(model.ReturnUrl);
        //    //    case SignInStatus.LockedOut:
        //    //        return View("Lockout");
        //    //    case SignInStatus.Failure:
        //    //    default:
        //    //        ModelState.AddModelError("", "Invalid code.");
        //    //        return View(model);
        //    //}
        //}

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
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
        }
        #endregion
    }
}