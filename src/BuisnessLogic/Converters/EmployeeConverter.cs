using System.Collections.Generic;
using DataAccess;
using Shared.DtoObjects;

namespace BuisnessLogic.Converters
{
    /// <summary>
    ///     Employee Conversion class between data and dto
    /// </summary>
    public static class EmployeeConverter
    {
        /// <summary>
        ///     Data Access to dto Conversion
        /// </summary>
        /// <param name="d">Data Access Employee</param>
        /// <returns>dto Employee</returns>
        public static DtoEmployee DataAccsessToDto(Employee d)
        {
            var e = new DtoEmployee
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
                Position = PositionConverter.DataAccsessToDto(d.Position),
                Description = d.description,
                SupervisorName = d.supervisorID == null ? string.Empty : d.Employee2.name + " " + d.Employee2.surname,
                SupervisorId = d.supervisorID,
                SportTypes = new List<DtoSportType>(),
                Password = d.password
            };
            foreach (var st in d.SportType)
                e.SportTypes.Add(SportTypeConverter.DataAccsessToDto(st));
            e.SportTypesString += e.SportTypes[0];
            for (var i = 1; i < e.SportTypes.Count; ++i)
                e.SportTypesString += ", " + e.SportTypes[i];
            return e;
        }

        /// <summary>
        ///     dto to Data Access Conversion
        /// </summary>
        /// <param name="employee">dto Employee</param>
        /// <returns>Data Access Employee</returns>
        public static Employee DtoToDataAccess(DtoEmployee employee)
        {
            var e = new Employee
            {
                id = employee.Id,
                name = employee.Name,
                surname = employee.Surname,
                email = employee.Email,
                phoneNumber = employee.PhoneNumber,
                icePhoneNumber = employee.IcePhoneNumber,
                personalIdentityNumber = employee.PersonalIdentityNumber,
                identityCardNumber = employee.IdentityCardNumber,
                city = employee.City,
                street = employee.Street,
                postalCode = employee.PostalCode,
                houseNumber = employee.HouseNumber,
                apartmentNumber = employee.ApartmentNumber,
                description = employee.Description,
                supervisorID = employee.SupervisorId,
                positionID = employee.Position.Id,
                password = AuthenticationLogic.HashPassword(employee.Password, employee)
            };
            return e;
        }
    }
}