using DataAccess;
using Shared.DtoObjects;

namespace BuisnessLogic.Converters
{
    /// <summary>
    ///     Static Class Position Converter
    /// </summary>
    /// <remarks>
    ///     Enables conversion between data and dto objects
    /// </remarks>
    public static class PositionConverter
    {
        /// <summary>
        ///     Conversion from Data to dto Method
        /// </summary>
        /// <param name="d">Data Access Position</param>
        /// <returns>dto Position</returns>
        public static DtoPosition DataAccsessToDto(Position d)
        {
            return new DtoPosition
            {
                Id = d.id,
                Name = d.name,
                Description = d.description
            };
        }

        /// <summary>
        ///     Conversion from dto to Data Access Method
        /// </summary>
        /// <param name="position">dto Position</param>
        /// <returns>Data Access Position</returns>
        public static Position DtoToDataAccess(DtoPosition position)
        {
            return new Position
            {
                id = position.Id,
                name = position.Name,
                description = position.Description
            };
        }
    }
}