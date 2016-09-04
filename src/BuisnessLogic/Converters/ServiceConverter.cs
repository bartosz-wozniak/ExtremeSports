using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess;
using Shared.DtoObjects;

namespace BuisnessLogic.Converters
{
    /// <summary>
    ///     Service Conversion class between data and business Objects
    /// </summary>
    public static class ServiceConverter
    {
        /// <summary>
        ///     Business object to data access conversion
        /// </summary>
        /// <param name="course">Business object course</param>
        /// <returns>Data access course object</returns>
        public static Service DtoToDataAccess(DtoService course)
        {
            return new Service
            {
                serviceTypeID = course.ServiceTypeId,
                employeeID = course.InstructorId
            };
        }

        /// <summary>
        ///     Data access to business object conversion
        /// </summary>
        /// <param name="service">Data access service</param>
        /// <returns>Business object course</returns>
        public static DtoService DataAccessToDto(Service service)
        {
            var ret = new DtoService
            {
                Id = service.id,
                ServiceTypeId = service.serviceTypeID,
                ServiceTypeName = service.ServiceType.name,
                InstructorId = service.employeeID,
                InstructorFullName = service.Employee.name + " " + service.Employee.surname,
                Dates = new List<DateTime>(service.ServiceDate.Select(d => d.date).ToList()),
                SportName = service.ServiceType.SportType.name
            };
            var customers = service.Customer.ToList();
            ret.Customers = new List<DtoCustomer>();
            foreach (var c in customers)
            {
                ret.Customers.Add(CustomerConverter.DataAccsessToDto(c));
            }
            return ret;
        }
    }
}