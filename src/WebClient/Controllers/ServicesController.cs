using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BuisnessLogic;
using Shared.Filters;
using WebClient.Models;

namespace WebClient.Controllers
{
    /// <summary>
    /// Controller for services
    /// </summary>
    public class ServicesController : Controller
    {
        private ServiceLogic Logic
        {
            get { return new ServiceLogic(); }
        }

        /// <summary>
        /// Constructor for my courses
        /// </summary>
        /// <returns>My courses view</returns>
        public async Task<ActionResult> MyCourses()
        {
            return View(await GetMyCourses());
        }

        private string CustomerEmail => System.Web.HttpContext.Current.User.Identity.Name;

        /// <summary>
        /// Check out of course
        /// </summary>
        /// <param name="id">id of course</param>
        /// <returns>View</returns>
        [HttpGet]
        public async Task<ActionResult> CheckOutOfCourse(int id)
        {
            var email = System.Web.HttpContext.Current.User.Identity.Name;
            bool success = await Logic.RemoveCustomerFromService(id, email);
            return View("MyCourses", await GetMyCourses());
        }

        private async Task<MyCoursesViewModel> GetMyCourses()
        {
            var courses = await Logic.GetServices(new ServiceFilter() { IsCourse = true, CustomerEmail = CustomerEmail });
            return new MyCoursesViewModel() { Courses = courses };
        }

        /// <summary>
        /// Course enrolment 
        /// </summary>
        /// <returns>Course enrolment view</returns>
        public async Task<ActionResult> CourseEnrolment()
        {
            return View(await GetCoursesToEnrol());
        }


        /// <summary>
        /// Enrol course
        /// </summary>
        /// <param name="id"></param>
        /// <returns>course id</returns>
        [HttpGet]
        public async Task<ActionResult> EnrolCourse(int id)
        {
            bool success = await Logic.EnrolCourse(CustomerEmail, id);
            return View("MyCourses", await GetMyCourses());
        }


        private async Task<CourseEnrolmentViewModel> GetCoursesToEnrol()
        {
            var courses = await Logic.GetCoursesWithoutCustomer(CustomerEmail);
            return new CourseEnrolmentViewModel() { Courses = courses };
        }

        /// <summary>
        /// Offered courses
        /// </summary>
        /// <returns>Offered courses view</returns>
        public async Task<ActionResult> OfferedCourses()
        {
            return View(await GetAllCourses());
        }

        private async Task<OfferedCoursesViewModel> GetAllCourses()
        {
            var courses = await Logic.GetServices(new ServiceFilter() { IsCourse = true });
            return new OfferedCoursesViewModel() { Courses = courses };
        }


    }
}