using DataAccess;
using Shared.DtoObjects;

namespace BuisnessLogic.Converters
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
        public static ServiceType DtoToDataAccess(DtoServiceType st)
        {
            return new ServiceType
            {
                id = st.Id,
                sportTypeID = st.SportTypeId,
                name = st.Name,
                description = st.Description,
                durationInMinutes = st.DurationInMinutes,
                price = st.Price,
                isCourse = st.IsCourse
            };
        }

        /// <summary>
        ///     Data access to business object conversion
        /// </summary>
        /// <param name="serviceType">Data access service type</param>
        /// <returns>Business object service type</returns>
        public static DtoServiceType DataAccsessToDto(ServiceType serviceType)
        {
            return new DtoServiceType
            {
                Id = serviceType.id,
                SportTypeId = serviceType.sportTypeID,
                Name = serviceType.name,
                Description = serviceType.description,
                DurationInMinutes = serviceType.durationInMinutes,
                Price = serviceType.price,
                SportTypeName = serviceType.SportType.name
            };
        }
    }
}