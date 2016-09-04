using System.Collections.ObjectModel;

namespace DesktopClientLogic.ViewModelObjects
{
    /// <summary>
    ///     Business Class Employee
    /// </summary>
    /// <remarks>
    ///     Referenced by Foreign Key with employee, sport type and service
    /// </remarks>
    public class Employee
    {
        /// <summary>
        ///     Employee ID (Unique, Not Null, Identity)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Employee Name (Not Null)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Employee Surname (Not Null)
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        ///     Employee Email (Not Null)
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Employee Phone Number (Not Null)
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        ///     Employee ICE Phone Number
        /// </summary>
        public string IcePhoneNumber { get; set; }

        /// <summary>
        ///     Employee Personal Identity Number (Unique, Not Null)
        /// </summary>
        public string PersonalIdentityNumber { get; set; }

        /// <summary>
        ///     Employee Identity Card Number (Unique, Not Null)
        /// </summary>
        public string IdentityCardNumber { get; set; }

        /// <summary>
        ///     Employee City (Not Null)
        /// </summary>
        public string City { get; set; }

        /// <summary>
        ///     Employee Street (Not Null)
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        ///     Employee Postal Code (Not Null)
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        ///     Employee House Number (Not Null)
        /// </summary>
        public string HouseNumber { get; set; }

        /// <summary>
        ///     Employee Apartment Number (Not Null)
        /// </summary>
        public string ApartmentNumber { get; set; }

        /// <summary>
        ///     Employee Apartment Number (Not Null)
        /// </summary>
        public Position Position { get; set; }

        /// <summary>
        ///     Employee Position (Not Null)
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Employee Description
        /// </summary>
        public int? SupervisorId { get; set; }

        /// <summary>
        ///     Employee Supervisor ID
        /// </summary>
        public string SupervisorName { get; set; }

        /// <summary>
        ///     Employee Supervisor Name
        /// </summary>
        public ObservableCollection<SportType> SportTypes { get; set; }

        /// <summary>
        ///     Employee Sport Types Collection Not Null
        /// </summary>
        public string SportTypesString { get; set; }

        /// <summary>
        ///     Employee Password Not Null
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///     Employee To String Method
        /// </summary>
        /// <returns>Employee Name and Surname</returns>
        public override string ToString()
        {
            return Name + " " + Surname;
        }
    }
}