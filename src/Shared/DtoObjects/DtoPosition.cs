namespace Shared.DtoObjects
{
    /// <summary>
    ///     Business Class Position
    /// </summary>
    /// <remarks>
    ///     Referenced by Foreign Key with Employee
    /// </remarks>
    public class DtoPosition
    {
        /// <summary>
        ///     Position ID (Unique, Not Null, Identity)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Position Name (Unique, Not Null)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Position Description
        /// </summary>
        public string Description { get; set; }
    }
}