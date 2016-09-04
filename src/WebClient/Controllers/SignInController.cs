using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using BuisnessLogic;
using Shared.DtoObjects;
using Shared.Filters;
using WebClient.Models;

namespace WebClient.Controllers
{
    /// <summary>
    /// Sign in controller
    /// </summary>
    public class SignInController : Controller
    {
        /// <summary>
        /// Sign in page
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Index()
        {
            return View("~/Views/SignIn/SignIn.cshtml");
        }

        /// <summary>
        /// Sign in user
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>View</returns>
        public async Task<ActionResult> SignIn(SignInViewModel model)
        {
            string error = null;
            if (string.IsNullOrWhiteSpace(model.Email) == false && string.IsNullOrWhiteSpace(model.Password) == false)
            {
                var user = await
                    AuthenticationLogic.AuthenticateCustomer(new DtoSignIn
                    {
                        EMail = model.Email,
                        Password = model.Password
                    });
                if (user != "Unauthorized")
                {
                    FormsAuthentication.RedirectFromLoginPage(model.Email, true);
                }
                else
                {
                    error = "Błąd podczas logowania";
                }
            }
            error = "Błąd podczas logowania";
            return View(new SignInViewModel() { ErrorText = error });
        }
        public async Task<ActionResult> SignOut(SignInViewModel model)
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            // clear authentication cookie
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            // clear session cookie (not necessary for your current problem but i would recommend you do it anyway)
            HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);

            FormsAuthentication.RedirectToLoginPage();
            return View(new SignInViewModel() { });
        }
    }
}
