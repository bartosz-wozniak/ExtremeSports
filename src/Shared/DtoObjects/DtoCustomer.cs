namespace Shared.DtoObjects
{
    /// <summary>
    ///     Business Class Customer
    /// </summary>
    /// <remarks>
    ///     Referenced by Foreign Key with Payment and Service
    /// </remarks>
    public class DtoCustomer
    {
        /// <summary>
        ///     Customer ID (Unique, Not Null, Identity)
        /// </summary>
        public int Id { get; set; }

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
        public string Password { get; set; }
    }
}