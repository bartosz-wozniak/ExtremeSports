using System;
using System.Collections.ObjectModel;

namespace DesktopClientLogic.ViewModelObjects
{
    public class Course : Service
    {
        public Course()
        {
            Dates = new ObservableCollection<DateTime>();
            Customers = new ObservableCollection<Customer>();
        }

        public ObservableCollection<DateTime> Dates { get; set; }
        public ObservableCollection<Customer> Customers { get; set; }
    }
}