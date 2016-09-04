using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BuisnessLogic;
using Shared.DtoObjects;
using Shared.Filters;
using WebClient.Models;

namespace WebClient.Controllers
{
    /// <summary>
    /// Register controller
    /// </summary>
    public class RegisterController : Controller
    {
        /// <summary>
        /// Register
        /// </summary>
        /// <returns>Register view</returns>
        public ActionResult Index()
        {
            return View("~/Views/Register/Register.cshtml");
        }

        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            DtoCustomer customer = new DtoCustomer()
            {
                Id = -1,
                Name = model.Name,
                Surname = model.Surname,
                Street = model.Street,
                City = model.City,
                PostalCode = model.PostalCode,
                PhoneNumber = model.PhoneNumber,
                IcePhoneNumber = model.IcePhoneNumber,
                Email = model.Email,
                IdentityCardNumber = model.IdentityCardNumber,
                PersonalIdentityNumber = model.PersonalIdentityNumber,
                ApartmentNumber = model.ApartmentNumber,
                HouseNumber = model.HouseNumber,
                Password = model.Password
            };
            CustomerLogic logic = new CustomerLogic();
            bool isRegistrationSuccessful = await logic.SaveCustomer(customer);
            string errorText = null;
            if (isRegistrationSuccessful)
            {
                FormsAuthentication.RedirectFromLoginPage(model.Email, true);
            }
            else
            {
                errorText = "Błąd podczas rejestracji";
            }

            return View(new RegisterViewModel() { ErrorText = errorText });
        }
    }
}