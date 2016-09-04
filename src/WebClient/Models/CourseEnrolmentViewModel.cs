using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shared.DtoObjects;

namespace WebClient.Models
{
    /// <summary>
    /// cOurse enroment view model
    /// </summary>
    public class CourseEnrolmentViewModel
    {
        /// <summary>
        /// Courses
        /// </summary>
        public IEnumerable<DtoService> Courses { get; set; }
    }
}