namespace Shared.Filters
{
    public class ServiceFilter
    {
        public bool IsCourse { get; set; }
        public string Customer { get; set; }
        public string Instructor { get; set; }
        public int? SportId { get; set; }
        public int? ServiceTypeId { get; set; }
        public string CustomerEmail { get; set; }
    }
}