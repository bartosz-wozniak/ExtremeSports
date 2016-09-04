using DesktopClientLogic.ViewModelObjects;
using Shared.DtoObjects;

namespace DesktopClientLogic.Converters
{
    /// <summary>
    ///     Static Class Sport Type Converter
    /// </summary>
    /// <remarks>
    ///     Enables conversion between DTO and View Model
    /// </remarks>
    public static class SportTypeConverter
    {
        /// <summary>
        ///     Conversion from DTO to View Model Method
        /// </summary>
        /// <param name="d">View Model Sport Type Object</param>
        /// <returns>DTO Sport Type</returns>
        public static DtoSportType ViewModelToDto(SportType d)
        {
            return new DtoSportType
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description
            };
        }

        /// <summary>
        ///     Conversion from DTO to View Model
        /// </summary>
        /// <param name="sportType">DTO Sport Type Object</param>
        /// <returns>View Model Sport Type Object</returns>
        public static SportType DtoToViewModel(DtoSportType sportType)
        {
            return new SportType
            {
                Id = sportType.Id,
                Name = sportType.Name,
                Description = sportType.Description
            };
        }
    }
}