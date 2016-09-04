using System;

namespace DesktopClientLogic.ViewModelObjects
{
    public class SingleService : Service
    {
        public DateTime Date { get; set; }
        public Customer Customer { get; set; }
    }
}