using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using BuisnessLogic.Converters;
using DataAccess;
using Shared.DtoObjects;

namespace BuisnessLogic
{
    /// <summary>
    ///     Class containing Business Logic for Customer Business Object
    /// </summary>
    public class CustomerLogic
    {
        public CustomerLogic()
        {
            Context = new ExtremeSportDBEntities();
        }

        /// <summary>
        ///     Data Context
        /// </summary>
        public ExtremeSportDBEntities Context { get; set; }

        /// <summary>
        ///     Method returns filtered customers from databse
        /// </summary>
        /// <param name="filter">Name or Surname of Customer</param>
        /// <returns>Collection of Customers from Database</returns>
        public async Task<ObservableCollection<DtoCustomer>> GetCustomers(string filter = null)
        {
            var ret = new ObservableCollection<DtoCustomer>();
            using (var data = Context)
            {
                if (!string.IsNullOrWhiteSpace(filter))
                    foreach (var item in await (from item in data.Customer
                        where
                            item.name.Contains(filter) || item.surname.Contains(filter) ||
                            item.personalIdentityNumber.Contains(filter)
                        select item).Take(50).ToListAsync())
                        ret.Add(CustomerConverter.DataAccsessToDto(item));
                else
                    foreach (var item in await (from item in data.Customer select item).Take(50).ToListAsync())
                        ret.Add(CustomerConverter.DataAccsessToDto(item));
                return ret;
            }
        }

        /// <summary>
        ///     Returns Custmer with specific ID
        /// </summary>
        /// <param name="customerId">Customer ID</param>
        /// <returns>Customer Business Object</returns>
        public async Task<DtoCustomer> GetCustomer(int customerId)
        {
            try
            {
                using (var data = Context)
                    return
                        CustomerConverter.DataAccsessToDto(
                            await
                                (from item in data.Customer where item.id == customerId select item)
                                    .FirstOrDefaultAsync());
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        ///     Returns Customer with specific email
        /// </summary>
        /// <param name="email">Customer e-mail</param>
        /// <returns>Customer Business Object</returns>
        public async Task<DtoCustomer> GetCustomer(string email)
        {
            using (var data = Context)
            {
                var c = await data.Customer.Where(cc => cc.email == email).ToListAsync();
                if (!c.Any())
                    return null;
                if (c.Count > 1)
                    throw new Exception("Emails are not unique");
                return CustomerConverter.DataAccsessToDto(c.First());
            }
        }

        /// <summary>
        ///     Adding or Updating Customer in a database
        /// </summary>
        /// <param name="customer">Business Object Customer</param>
        /// <returns>True if succeeded and false otherwise</returns>
        public async Task<bool> SaveCustomer(DtoCustomer customer)
        {
            try
            {
                using (var data = Context)
                {
                    var c =
                        await
                            (from item in data.Customer where customer.Id == item.id select item).FirstOrDefaultAsync();
                    // Updating Customer
                    if (c != null)
                    {
                        c.name = customer.Name;
                        c.surname = customer.Surname;
                        c.street = customer.Street;
                        c.postalCode = customer.PostalCode;
                        c.phoneNumber = customer.PhoneNumber;
                        c.personalIdentityNumber = customer.PersonalIdentityNumber;
                        c.identityCardNumber = customer.IdentityCardNumber;
                        c.icePhoneNumber = customer.IcePhoneNumber;
                        c.houseNumber = customer.HouseNumber;
                        c.email = customer.Email;
                        c.city = customer.City;
                        c.apartmentNumber = customer.ApartmentNumber;
                        c.password = AuthenticationLogic.HashPassword(customer.Password, customer);
                    }
                    // Adding new Customer
                    else
                    {
                        if (
                            await
                                (from item in data.Customer
                                    where customer.PersonalIdentityNumber == item.personalIdentityNumber
                                    select item).AnyAsync())
                            return false;
                        data.Customer.Add(CustomerConverter.DtoToDataAccess(customer));
                    }
                    await data.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        ///     Method removes customer from a database
        /// </summary>
        /// <param name="customerId">Customer ID</param>
        /// <returns>True if succeeded and false othewise</returns>
        public async Task<bool> RemoveCustomer(int customerId)
        {
            try
            {
                using (var data = Context)
                {
                    var c = await (from item in data.Customer where item.id == customerId select item).FirstAsync();
                    if (c.Service.Count != 0)
                        return false;
                    data.Customer.Remove(c);
                    await data.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}