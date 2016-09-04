using System;
using System.Collections.Generic;

namespace Shared.DtoObjects
{
    public class DtoService
    {
        public DtoService()
        {
            Dates = new List<DateTime>();
            Customers = new List<DtoCustomer>();
        }

        public int Id { get; set; }
        public int ServiceTypeId { get; set; }
        public string ServiceTypeName { get; set; }
        public int InstructorId { get; set; }
        public string InstructorFullName { get; set; }
        public string SportName { get; set; }
        public IList<DateTime> Dates { get; set; }
        public IList<DtoCustomer> Customers { get; set; }
        public int AvailibleVacancies => MaxVacancies - Customers.Count;
        public int MaxVacancies => 5;
    }
}