namespace Shared.DtoObjects
{
    public class DtoServiceType
    {
        public int Id { get; set; }
        public int SportTypeId { get; set; }
        public string SportTypeName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DurationInMinutes { get; set; }
        public decimal Price { get; set; }
        public bool IsCourse { get; set; }
    }
}