using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using BuisnessLogic.Converters;
using DataAccess;
using Shared.DtoObjects;
using Shared.Filters;

namespace BuisnessLogic
{
    /// <summary>
    ///     Class containing Business Logic for Services
    /// </summary>
    public class ServiceLogic
    {
        /// <summary>
        ///     Number of records to take
        /// </summary>
        private const int TakeTop = 100;

        public ServiceLogic()
        {
            Context = new ExtremeSportDBEntities();
        }

        /// <summary>
        ///     Context
        /// </summary>
        public ExtremeSportDBEntities Context { get; set; }

        /// <summary>
        ///     Adds new course to database
        /// </summary>
        /// <param name="service">Dtoobject course to add</param>
        /// <returns>Is Success</returns>
        public async Task<bool> AddService(DtoService service)
        {
            try
            {
                using (var data = Context)
                {
                    var databaseItem = ServiceConverter.DtoToDataAccess(service);
                    data.Service.Add(databaseItem);
                    foreach (var d in service.Dates)
                        data.ServiceDate.Add(new ServiceDate {serviceID = service.Id, date = d});
                    await data.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        ///     Method returning filtered courses
        /// </summary>
        /// <param name="filter">Filter to select specific courses</param>
        /// <returns>IEnumerable of DtoService</returns>
        public async Task<IEnumerable<DtoService>> GetServices(ServiceFilter filter = null)
        {
            var ret = new ObservableCollection<DtoService>();
            try
            {
                using (var data = Context)
                {
                    if (filter == null)
                        foreach (
                            var course in
                                await data.Service.Where(s => s.ServiceType.isCourse).Take(TakeTop).ToListAsync())
                            ret.Add(ServiceConverter.DataAccessToDto(course));
                    else
                    {
                        var services = await data.Service
                            .Where
                            (s =>
                                s.ServiceType.isCourse == filter.IsCourse
                                &&
                                (string.IsNullOrEmpty(filter.Customer) ||
                                 s.Customer.Any(
                                     c => c.name.Contains(filter.Customer) || c.surname.Contains(filter.Customer)))
                                &&
                                (string.IsNullOrEmpty(filter.Instructor) || s.Employee.name.Contains(filter.Instructor) ||
                                 s.Employee.surname.Contains(filter.Instructor))
                                && (filter.ServiceTypeId == null || s.ServiceType.id == filter.ServiceTypeId)
                                && (filter.SportId == null || s.ServiceType.sportTypeID == filter.SportId)
                                && (string.IsNullOrEmpty(filter.CustomerEmail) || s.Customer.Any(c => c.email == filter.CustomerEmail))
                            )
                            .Take(TakeTop).ToListAsync();
                        foreach (var course in services)
                            ret.Add(ServiceConverter.DataAccessToDto(course));
                    }
                }
            }
            catch (Exception e)
            {
                ret = null;
            }
            return ret;
        }

        /// <summary>
        ///     Method returning filtered courses
        /// </summary>
        /// <param name="filter">Filter to select specific courses</param>
        /// <returns>IEnumerable of DtoService</returns>
        public async Task<IEnumerable<DtoService>> GetCoursesWithoutCustomer(string customerEmail)
        {
            var ret = new ObservableCollection<DtoService>();
            try
            {
                using (var data = Context)
                {
                    var services = await data.Service
                        .Where
                        (s =>
                            s.ServiceType.isCourse
                            &&  s.Customer.All(c => c.email != customerEmail)
                        )
                        .Take(TakeTop).ToListAsync();
                    foreach (var course in services)
                        ret.Add(ServiceConverter.DataAccessToDto(course));
                }
            }
            catch (Exception e)
            {
                ret = null;
            }
            return ret;
        }

        /// <summary>
        ///     Adds date to specific course
        /// </summary>
        /// <param name="courseId">Id of course to which date is added</param>
        /// <param name="date">Date to add</param>
        /// <returns>Success</returns>
        public async Task<bool> AddDateToService(int courseId, DateTime date)
        {
            try
            {
                using (var data = Context)
                {
                    var serviceDate = new ServiceDate
                    {
                        date = date,
                        serviceID = courseId
                    };
                    data.Service.First(s => s.id == courseId).ServiceDate.Add(serviceDate);
                    await data.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        ///     Removes customer from course
        /// </summary>
        /// <param name="courseId">Id of course</param>
        /// <param name="customerId">Id of customer</param>
        /// <returns>Success</returns>
        public async Task<bool> RemoveCustomerFromService(int courseId, int customerId)
        {
            try
            {
                using (var data = Context)
                {
                    data.Service.First(s => s.id == courseId)
                        .Customer.Remove(data.Customer.First(c => c.id == customerId));
                    await data.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        ///     Removes customer from course
        /// </summary>
        /// <param name="courseId">Id of course</param>
        /// <param name="customerEmail">E-mail of customer</param>
        /// <returns>Success</returns>
        public async Task<bool> RemoveCustomerFromService(int courseId, string customerEmail)
        {
            try
            {
                using (var data = Context)
                {
                    data.Service.First(s => s.id == courseId)
                        .Customer.Remove(data.Customer.First(c => c.email == customerEmail));
                    await data.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        ///     Removes date from specific course
        /// </summary>
        /// <param name="courseId">Id of course</param>
        /// <param name="date">Date to remove</param>
        /// <returns>Success</returns>
        public async Task<bool> RemoveDateFromService(int courseId, DateTime date)
        {
            try
            {
                using (var data = Context)
                {
                    var toRemove = data.ServiceDate.FirstOrDefault(d => d.serviceID == courseId && d.date == date);
                    if (toRemove == null)
                        return false;
                    data.ServiceDate.Remove(toRemove);
                    await data.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        ///     Method returning course with specific id
        /// </summary>
        /// <param name="courseId">Id of course</param>
        /// <returns>Dto object of course</returns>
        public async Task<DtoService> GetCourse(int courseId)
        {
            DtoService course = null;
            using (var data = Context)
            {
                var service = await data.Service.FirstOrDefaultAsync(c => c.id == courseId);
                if (service != null)
                    course = ServiceConverter.DataAccessToDto(service);
            }
            return course;
        }

        /// <summary>
        ///     Updates specific course in the database to make it same as the argument
        /// </summary>
        /// <param name="course">Buisenss object course to edit - updated version of it</param>
        /// <returns>Success</returns>
        public async Task<bool> EditCourse(DtoService course)
        {
            try
            {
                using (var data = Context)
                {
                    var service = await data.Service.FirstOrDefaultAsync(c => c.id == course.Id);
                    service.employeeID = course.InstructorId;
                    service.serviceTypeID = course.ServiceTypeId;
                    var addedDates =
                        course.Dates.Except(data.ServiceDate.Where(s => s.serviceID == course.Id).Select(s => s.date))
                            .ToList();
                    foreach (var d in addedDates)
                        service.ServiceDate.Add(new ServiceDate {serviceID = course.Id, date = d});
                    var datesToRemove =
                        data.ServiceDate.Where(s => s.serviceID == course.Id && !course.Dates.Contains(s.date));
                    data.ServiceDate.RemoveRange(datesToRemove);
                    var cutomersToRemove =
                        service.Customer.Where(c => course.Customers.All(cu => cu.Id != c.id)).ToList();
                    foreach (var c in cutomersToRemove)
                        service.Customer.Remove(c);
                    data.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        ///     Method enrols customer on course
        /// </summary>
        /// <param name="customerId">Id of customer</param>
        /// <param name="courseId">Id of course</param>
        /// <returns>Success</returns>
        public async Task<bool> EnrolCourse(int customerId, int courseId)
        {
            try
            {
                using (var data = Context)
                {
                    var course = await data.Service.FirstOrDefaultAsync(c => c.id == courseId);
                    var customer = await data.Customer.FirstOrDefaultAsync(c => c.id == customerId);
                    if (course != null && customer != null)
                        course.Customer.Add(customer);
                    await data.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        ///     Method enrols customer on course
        /// </summary>
        /// <param name="customerId">Id of customer</param>
        /// <param name="courseId">Id of course</param>
        /// <returns>Success</returns>
        public async Task<bool> EnrolCourse(string customerEmail, int courseId)
        {
            var customer = await new CustomerLogic().GetCustomer(customerEmail);
            return await EnrolCourse(customer.Id, courseId);
        }

        /// <summary>
        ///     Method removing course
        /// </summary>
        /// <param name="courseId">Id of course</param>
        /// <returns>Success</returns>
        public async Task<bool> RemoveCourse(int courseId)
        {
            try
            {
                using (var data = Context)
                {
                    var course = await data.Service.FirstOrDefaultAsync(c => c.id == courseId);
                    if (course != null)
                    {
                        course.ServiceDate.Clear();
                        course.Customer.Clear();
                        data.Service.Remove(course);
                    }
                    await data.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        ///     Enrols customer on singe service - requires also adding that service to database
        /// </summary>
        /// <param name="service">Service to add - it rquires cutomer beeing filled</param>
        /// <returns>Success</returns>
        public async Task<bool> EnrolSingleService(DtoService service)
        {
            try
            {
                using (var data = Context)
                {
                    var s = new Service
                    {
                        serviceTypeID = service.ServiceTypeId,
                        employeeID = service.InstructorId
                    };
                    data.Service.Add(s);
                    var custId = service.Customers.First().Id;
                    var cust = await data.Customer.FirstOrDefaultAsync(c => c.id == custId);
                    s.Customer.Add(cust);
                    var date = service.Dates.First();
                    s.ServiceDate.Add(new ServiceDate {serviceID = s.id, date = date});
                    return data.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}