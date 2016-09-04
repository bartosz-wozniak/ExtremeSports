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
    ///     Static Class containing Business Logic for Employee Business Object
    /// </summary>
    public class EmployeeLogic
    {
        public EmployeeLogic()
        {
            Context = new ExtremeSportDBEntities();
        }

        /// <summary>
        ///     Data Context
        /// </summary>
        public ExtremeSportDBEntities Context { get; set; }

        /// <summary>
        ///     Method returns filtered Employees from databse
        /// </summary>
        /// <param name="filter">Name or Surname or PersonalIdentityNumber of Employee</param>
        /// <returns>Collection of Employees from Database</returns>
        public async Task<ObservableCollection<DtoEmployee>> GetEmployees(string filter = null)
        {
            var ret = new ObservableCollection<DtoEmployee>();
            using (var data = Context)
                if (!string.IsNullOrWhiteSpace(filter))
                    foreach (var item in await (from item in data.Employee
                        where
                            item.name.Contains(filter) || item.surname.Contains(filter) ||
                            item.personalIdentityNumber.Contains(filter)
                        select item).ToListAsync())
                        ret.Add(EmployeeConverter.DataAccsessToDto(item));
                else
                    foreach (var item in await (from item in data.Employee select item).ToListAsync())
                        ret.Add(EmployeeConverter.DataAccsessToDto(item));
            return ret;
        }

        /// <summary>
        ///     Returns Employee with specific ID
        /// </summary>
        /// <param name="employeeId">Employee ID</param>
        /// <returns>Employee Business Object</returns>
        public async Task<DtoEmployee> GetEmployee(int employeeId)
        {
            try
            {
                using (var data = Context)
                    return
                        EmployeeConverter.DataAccsessToDto(
                            await
                                (from item in data.Employee where item.id == employeeId select item)
                                    .FirstOrDefaultAsync());
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        ///     Returns Employee with specific email
        /// </summary>
        /// <param name="email">Employee e-mail</param>
        /// <returns>Employee Business Object</returns>
        public async Task<DtoEmployee> GetEmployee(string email)
        {
            using (var data = Context)
            {
                var emps = await data.Employee.Where(e => e.email == email).ToListAsync();
                if (!emps.Any())
                    return null;
                if (emps.Count > 1)
                    throw new Exception("Emails are not unique");
                return EmployeeConverter.DataAccsessToDto(emps.First());
            }
        }

        /// <summary>
        ///     Adding or Updating Employee in a database
        /// </summary>
        /// <param name="employee">Business Object Employee</param>
        /// <returns>True if succeeded and false otherwise</returns>
        public async Task<bool> SaveEmployee(DtoEmployee employee)
        {
            try
            {
                using (var data = Context)
                {
                    var e =
                        await
                            (from item in data.Employee where employee.Id == item.id select item).FirstOrDefaultAsync();
                    // Updating Employee
                    if (e != null)
                    {
                        e.name = employee.Name;
                        e.surname = employee.Surname;
                        e.street = employee.Street;
                        e.postalCode = employee.PostalCode;
                        e.phoneNumber = employee.PhoneNumber;
                        e.personalIdentityNumber = employee.PersonalIdentityNumber;
                        e.identityCardNumber = employee.IdentityCardNumber;
                        e.icePhoneNumber = employee.IcePhoneNumber;
                        e.houseNumber = employee.HouseNumber;
                        e.email = employee.Email;
                        e.city = employee.City;
                        e.apartmentNumber = employee.ApartmentNumber;
                        e.description = employee.Description;
                        e.positionID = employee.Position.Id;
                        e.supervisorID = employee.SupervisorId;
                        e.password = AuthenticationLogic.HashPassword(employee.Password, employee);
                        e.SportType.Clear();
                        foreach (var item in employee.SportTypes)
                        {
                            var st = data.SportType.First(s => s.id == item.Id);
                            e.SportType.Add(st);
                        }
                    }
                    // Adding new Employee
                    else
                    {
                        if (await (from item in data.Employee
                            where
                                employee.PersonalIdentityNumber == item.personalIdentityNumber ||
                                employee.IdentityCardNumber == item.identityCardNumber
                            select item).AnyAsync())
                            return false;
                        var emp = EmployeeConverter.DtoToDataAccess(employee);
                        foreach (var item in employee.SportTypes)
                        {
                            var st = data.SportType.First(s => s.id == item.Id);
                            emp.SportType.Add(st);
                        }
                        data.Employee.Add(emp);
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
        ///     Method removes employee from a database
        /// </summary>
        /// <param name="employeeId">Employee ID</param>
        /// <returns>True if succeeded and false othewise</returns>
        public async Task<bool> RemoveEmployee(int employeeId)
        {
            try
            {
                using (var data = Context)
                {
                    var e = await (from item in data.Employee where item.id == employeeId select item).FirstAsync();
                    if (e.Employee1.Count != 0 || e.Service.Count != 0)
                        return false;
                    e.SportType.Clear();
                    data.Employee.Remove(e);
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