using DataAccess;
using Shared.DtoObjects;

namespace BuisnessLogic.Converters
{
    /// <summary>
    ///     Static Class Sport Type Converter
    /// </summary>
    /// <remarks>
    ///     Enables conversion between dto and Database Objects
    /// </remarks>
    public static class SportTypeConverter
    {
        /// <summary>
        ///     Conversion from Data Access to dto Method
        /// </summary>
        /// <param name="d">Data Access Sport Type Object</param>
        /// <returns>dto Sport Type</returns>
        public static DtoSportType DataAccsessToDto(SportType d)
        {
            return new DtoSportType
            {
                Id = d.id,
                Name = d.name,
                Description = d.description
            };
        }

        /// <summary>
        ///     Conversion from dto to data Object
        /// </summary>
        /// <param name="sportType">dto Sport Type Object</param>
        /// <returns>Data Access Sport Type Object</returns>
        public static SportType DtoToDataAccess(DtoSportType sportType)
        {
            return new SportType
            {
                id = sportType.Id,
                name = sportType.Name,
                description = sportType.Description
            };
        }
    }
}