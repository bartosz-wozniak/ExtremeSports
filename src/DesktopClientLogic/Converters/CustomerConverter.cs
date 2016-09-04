using DesktopClientLogic.ViewModelObjects;
using Shared.DtoObjects;

namespace DesktopClientLogic.Converters
{
    /// <summary>
    ///     Customer Conversion class between Data Transfer Object and View Model
    /// </summary>
    public static class CustomerConverter
    {
        /// <summary>
        ///     View Model to Data Transfer Object Conversion
        /// </summary>
        /// <param name="d">View Model Customer</param>
        /// <returns>DTO Customer</returns>
        public static DtoCustomer ViewModelToDto(Customer d)
        {
            return new DtoCustomer
            {
                Id = d.Id,
                Name = d.Name,
                Surname = d.Surname,
                Email = d.Email,
                PhoneNumber = d.PhoneNumber,
                IcePhoneNumber = d.IcePhoneNumber,
                PersonalIdentityNumber = d.PersonalIdentityNumber,
                IdentityCardNumber = d.IdentityCardNumber,
                City = d.City,
                Street = d.Street,
                PostalCode = d.PostalCode,
                HouseNumber = d.HouseNumber,
                ApartmentNumber = d.ApartmentNumber,
                Password = d.Password
            };
        }

        /// <summary>
        ///     DTO to Viem Model Conversion
        /// </summary>
        /// <param name="customer">DTO Customer</param>
        /// <returns>View Model Customer</returns>
        public static Customer DtoToViewModel(DtoCustomer customer)
        {
            return new Customer
            {
                Id = customer.Id,
                Name = customer.Name,
                Surname = customer.Surname,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                IcePhoneNumber = customer.IcePhoneNumber,
                PersonalIdentityNumber = customer.PersonalIdentityNumber,
                IdentityCardNumber = customer.IdentityCardNumber,
                City = customer.City,
                Street = customer.Street,
                PostalCode = customer.PostalCode,
                HouseNumber = customer.HouseNumber,
                ApartmentNumber = customer.ApartmentNumber,
                Password = customer.Password
            };
        }
    }
}