using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BuisnessLogic;
using BuisnessLogic.Converters;
using Shared.DtoObjects;
using Shared.Filters;
using WebClient.Models;

namespace WebClient.Controllers
{
    /// <summary>
    /// Edit data controller
    /// </summary>
    public class EditDataController : Controller
    {
        public async Task<ActionResult> EditData()
        {
            DtoCustomer model = await new CustomerLogic().GetCustomer(System.Web.HttpContext.Current.User.Identity.Name);
            return View(new EditDataViewModel
            {
                Id = model.Id,
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
                HouseNumber = model.HouseNumber
            });
        }

        /// <summary>
        /// Save data
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>Save data view</returns>
        public async Task<ActionResult> SaveData(EditDataViewModel model)
        {
            DtoCustomer customer = new DtoCustomer()
            {
                Id = model.Id,
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
                errorText = "Błąd podczas zapisywania danych";
            }

            return View("EditData", new EditDataViewModel() { ErrorText = errorText });
        }
    }
}