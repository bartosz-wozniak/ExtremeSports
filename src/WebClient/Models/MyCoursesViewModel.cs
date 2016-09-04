using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shared.DtoObjects;

namespace WebClient.Models
{
    /// <summary>
    /// My courses model
    /// </summary>
    public class MyCoursesViewModel
    {
        /// <summary>
        /// Courses
        /// </summary>
        public IEnumerable<DtoService> Courses {get; set; } 
       
    }
}