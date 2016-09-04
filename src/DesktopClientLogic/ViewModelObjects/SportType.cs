namespace DesktopClientLogic.ViewModelObjects
{
    /// <summary>
    ///     Business Class Sport Type
    /// </summary>
    /// <remarks>
    ///     Referenced by Foreign Key with Employee and Service Type
    /// </remarks>
    public class SportType
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

        /// <summary>
        ///     Sport Type To String Method
        /// </summary>
        /// <returns>Sport Type Name</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}