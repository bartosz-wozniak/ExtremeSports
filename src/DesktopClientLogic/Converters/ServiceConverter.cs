using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DesktopClientLogic.ViewModelObjects;
using Shared.DtoObjects;

namespace DesktopClientLogic.Converters
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
        public static Course DtoToCourseViewModel(DtoService course)
        {
            var ret = new Course
            {
                Id = course.Id,
                ServiceTypeId = course.ServiceTypeId,
                ServiceTypeName = course.ServiceTypeName,
                InstructorId = course.InstructorId,
                InstructorFullName = course.InstructorFullName,
                Dates = new ObservableCollection<DateTime>(course.Dates),
                Customers = new ObservableCollection<Customer>(),
                SportName = course.SportName
            };
            foreach (var c in course.Customers)
            {
                ret.Customers.Add(CustomerConverter.DtoToViewModel(c));
            }
            return ret;
        }

        /// <summary>
        ///     Data access to business object conversion
        /// </summary>
        /// <param name="service">Data access service</param>
        /// <returns>Business object course</returns>
        public static DtoService ViewModelCourseToDto(Course service)
        {
            var ret = new DtoService
            {
                Id = service.Id,
                ServiceTypeId = service.ServiceTypeId,
                ServiceTypeName = service.ServiceTypeName,
                InstructorId = service.InstructorId,
                InstructorFullName = service.InstructorFullName,
                Dates = new List<DateTime>(service.Dates)
            };
            var customers = service.Customers;
            ret.Customers = new List<DtoCustomer>();
            foreach (var c in customers)
            {
                ret.Customers.Add(CustomerConverter.ViewModelToDto(c));
            }
            return ret;
        }

        /// <summary>
        ///     Business object to data access conversion
        /// </summary>
        /// <param name="service">Business object single service</param>
        /// <returns>Data access course object</returns>
        public static SingleService DtoToViewModelSingleService(DtoService service)
        {
            var ret = new SingleService
            {
                Id = service.Id,
                ServiceTypeId = service.ServiceTypeId,
                ServiceTypeName = service.ServiceTypeName,
                InstructorId = service.InstructorId,
                InstructorFullName = service.InstructorFullName,
                Date = service.Dates.First(),
                Customer = CustomerConverter.DtoToViewModel(service.Customers.First()),
                SportName = service.SportName
            };
            return ret;
        }

        /// <summary>
        ///     Data access to business object conversion
        /// </summary>
        /// <param name="service">Data access service</param>
        /// <returns>Business object single service</returns>
        public static DtoService ViewModelSingleServiceToDto(SingleService service)
        {
            var ret = new DtoService
            {
                Id = service.Id,
                ServiceTypeId = service.ServiceTypeId,
                ServiceTypeName = service.ServiceTypeName,
                InstructorId = service.InstructorId,
                InstructorFullName = service.InstructorFullName,
                Dates = new List<DateTime> {service.Date},
                Customers = new List<DtoCustomer> {CustomerConverter.ViewModelToDto(service.Customer)}
            };
            return ret;
        }
    }
}