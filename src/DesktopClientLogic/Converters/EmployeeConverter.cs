using System.Collections.Generic;
using System.Collections.ObjectModel;
using DesktopClientLogic.ViewModelObjects;
using Shared.DtoObjects;

namespace DesktopClientLogic.Converters
{
    /// <summary>
    ///     Employee Conversion class between DTO and Viem Model
    /// </summary>
    public static class EmployeeConverter
    {
        /// <summary>
        ///     DTO to View Model Conversion
        /// </summary>
        /// <param name="d">DTO Employee</param>
        /// <returns>View Model Employee</returns>
        public static Employee DtoToViewModel(DtoEmployee d)
        {
            var e = new Employee
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
                Position = PositionConverter.DtoToViewModel(d.Position),
                Description = d.Description,
                SupervisorName = d.SupervisorName,
                SupervisorId = d.SupervisorId,
                SportTypes = new ObservableCollection<SportType>(),
                Password = d.Password
            };
            foreach (var st in d.SportTypes)
                e.SportTypes.Add(SportTypeConverter.DtoToViewModel(st));
            e.SportTypesString += e.SportTypes[0];
            for (var i = 1; i < e.SportTypes.Count; ++i)
                e.SportTypesString += ", " + e.SportTypes[i];
            return e;
        }

        /// <summary>
        ///     View Model to DTO Conversion
        /// </summary>
        /// <param name="employee">View Model Employee</param>
        /// <returns>DTO Employee</returns>
        public static DtoEmployee ViewModelToDto(Employee employee)
        {
            var e = new DtoEmployee
            {
                Id = employee.Id,
                Name = employee.Name,
                Surname = employee.Surname,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                IcePhoneNumber = employee.IcePhoneNumber,
                PersonalIdentityNumber = employee.PersonalIdentityNumber,
                IdentityCardNumber = employee.IdentityCardNumber,
                City = employee.City,
                Street = employee.Street,
                PostalCode = employee.PostalCode,
                HouseNumber = employee.HouseNumber,
                ApartmentNumber = employee.ApartmentNumber,
                Description = employee.Description,
                SupervisorId = employee.SupervisorId,
                Position = PositionConverter.ViewModelToDto(employee.Position),
                SportTypes = new List<DtoSportType>(),
                Password = employee.Password
            };
            foreach (var st in employee.SportTypes)
                e.SportTypes.Add(SportTypeConverter.ViewModelToDto(st));
            return e;
        }
    }
}