using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shared.DtoObjects;

namespace WebClient.Models
{
    public class OfferedCoursesViewModel
    {
        public IEnumerable<DtoService> Courses { get; set; }

    }
}