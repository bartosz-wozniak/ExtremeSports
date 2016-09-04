using DataAccess;
using Shared.DtoObjects;

namespace BuisnessLogic.Converters
{
    /// <summary>
    ///     Customer Conversion class between data and dto Objects
    /// </summary>
    public static class CustomerConverter
    {
        /// <summary>
        ///     Data Access to dto Conversion
        /// </summary>
        /// <param name="d">Data Access Customer</param>
        /// <returns>dto Customer</returns>
        public static DtoCustomer DataAccsessToDto(Customer d)
        {
            return new DtoCustomer
            {
                Id = d.id,
                Name = d.name,
                Surname = d.surname,
                Email = d.email,
                PhoneNumber = d.phoneNumber,
                IcePhoneNumber = d.icePhoneNumber,
                PersonalIdentityNumber = d.personalIdentityNumber,
                IdentityCardNumber = d.identityCardNumber,
                City = d.city,
                Street = d.street,
                PostalCode = d.postalCode,
                HouseNumber = d.houseNumber,
                ApartmentNumber = d.apartmentNumber,
                Password = d.password
            };
        }

        /// <summary>
        ///     dto to Data Access Conversion
        /// </summary>
        /// <param name="customer">dto Customer</param>
        /// <returns>Data Access Customer</returns>
        public static Customer DtoToDataAccess(DtoCustomer customer)
        {
            return new Customer
            {
                id = customer.Id,
                name = customer.Name,
                surname = customer.Surname,
                email = customer.Email,
                phoneNumber = customer.PhoneNumber,
                icePhoneNumber = customer.IcePhoneNumber,
                personalIdentityNumber = customer.PersonalIdentityNumber,
                identityCardNumber = customer.IdentityCardNumber,
                city = customer.City,
                street = customer.Street,
                postalCode = customer.PostalCode,
                houseNumber = customer.HouseNumber,
                apartmentNumber = customer.ApartmentNumber,
                password = AuthenticationLogic.HashPassword(customer.Password, customer)
            };
        }
    }
}