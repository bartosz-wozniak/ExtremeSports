using DesktopClientLogic.ViewModelObjects;
using Shared.DtoObjects;

namespace DesktopClientLogic.Converters
{
    /// <summary>
    ///     ServiceType Conversion class between data and business Objects
    /// </summary>
    public class ServiceTypeConverter
    {
        /// <summary>
        ///     Business object to data access conversion
        /// </summary>
        /// <param name="st">Business object service type</param>
        /// <returns>Data access service object</returns>
        public static ServiceType DtoToViewModel(DtoServiceType st)
        {
            return new ServiceType
            {
                Id = st.Id,
                SportTypeId = st.SportTypeId,
                Name = st.Name,
                Description = st.Description,
                DurationInMinutes = st.DurationInMinutes,
                Price = st.Price,
                IsCourse = st.IsCourse,
                SportTypeName = st.SportTypeName
            };
        }

        /// <summary>
        ///     Data access to business object conversion
        /// </summary>
        /// <param name="serviceType">Data access service type</param>
        /// <returns>Business object service type</returns>
        public static DtoServiceType ViewModelToDto(ServiceType serviceType)
        {
            return new DtoServiceType
            {
                Id = serviceType.Id,
                SportTypeId = serviceType.SportTypeId,
                Name = serviceType.Name,
                Description = serviceType.Description,
                DurationInMinutes = serviceType.DurationInMinutes,
                Price = serviceType.Price,
                SportTypeName = serviceType.SportTypeName
            };
        }
    }
}