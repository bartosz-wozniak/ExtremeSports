using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Shared.DtoObjects;

namespace WebClient.Models
{
    public class RegisterViewModel
    {
        public string ErrorText { get; set; }

        /// <summary>
        ///     Customer Name (Not Null)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Customer Surname (Not Null)
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        ///     Customer Email
        /// </summary>
        ///         
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        ///     Customer Phone Number
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        ///     Customer ICE Phone Number
        /// </summary>
        public string IcePhoneNumber { get; set; }

        /// <summary>
        ///     Customer Personal Identity Number (Unique, Not Null)
        /// </summary>
        public string PersonalIdentityNumber { get; set; }

        /// <summary>
        ///     Customer Identity Card Number
        /// </summary>
        public string IdentityCardNumber { get; set; }

        /// <summary>
        ///     Customer City
        /// </summary>
        public string City { get; set; }

        /// <summary>
        ///     Customer Street
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        ///     Customer Postal Code
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        ///     Customer House Number
        /// </summary>
        public string HouseNumber { get; set; }

        /// <summary>
        ///     Customer Apartment Number
        /// </summary>
        public string ApartmentNumber { get; set; }

        /// <summary>
        ///     Customer Password Not Null
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public RegisterViewModel()
        {
        }
    }
}