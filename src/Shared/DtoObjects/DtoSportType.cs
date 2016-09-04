namespace Shared.DtoObjects
{
    /// <summary>
    ///     Business Class Sport Type
    /// </summary>
    /// <remarks>
    ///     Referenced by Foreign Key with Employee and Service Type
    /// </remarks>
    public class DtoSportType
    {
        /// <summary>
        ///     Sport Type ID (Unique, Not Null, Identity)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Sport Type Name (Unique, Not Null)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Sport Type Description
        /// </summary>
        public string Description { get; set; }
    }
}