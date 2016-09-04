using DesktopClientLogic.ViewModelObjects;
using Shared.DtoObjects;

namespace DesktopClientLogic.Converters
{
    /// <summary>
    ///     Static Class Position Converter
    /// </summary>
    /// <remarks>
    ///     Enables conversion between DTO and View Model
    /// </remarks>
    public static class PositionConverter
    {
        /// <summary>
        ///     Conversion from View Model to DTO Method
        /// </summary>
        /// <param name="d">View Model Position</param>
        /// <returns>DTO Position</returns>
        public static DtoPosition ViewModelToDto(Position d)
        {
            return new DtoPosition
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description
            };
        }

        /// <summary>
        ///     Conversion from DTO to View Model Method
        /// </summary>
        /// <param name="position">DTO Position</param>
        /// <returns>View Model Position</returns>
        public static Position DtoToViewModel(DtoPosition position)
        {
            return new Position
            {
                Id = position.Id,
                Name = position.Name,
                Description = position.Description
            };
        }
    }
}