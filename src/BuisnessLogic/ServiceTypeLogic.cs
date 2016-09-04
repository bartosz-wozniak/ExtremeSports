using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using BuisnessLogic.Converters;
using DataAccess;
using Shared.DtoObjects;
using Shared.Filters;

namespace BuisnessLogic
{
    /// <summary>
    ///     Class containing Business Logic for ServiceTypes
    /// </summary>
    public class ServiceTypeLogic
    {
        /// <summary>
        ///     Number of records to take
        /// </summary>
        private const int TakeTop = 10;

        public ServiceTypeLogic()
        {
            Context = new ExtremeSportDBEntities();
        }

        /// <summary>
        ///     Context
        /// </summary>
        public ExtremeSportDBEntities Context { get; set; }

        /// <summary>
        ///     Method returns filtered ServiceTypes
        /// </summary>
        /// <param name="filter">Filter to select specific service types</param>
        /// <returns>Collection of service types</returns>
        public async Task<ObservableCollection<DtoServiceType>> GetServiceTypes(ServiceTypeFilter filter)
        {
            var ret = new ObservableCollection<DtoServiceType>();
            try
            {
                using (var data = Context)
                {
                    foreach (var st in await data.ServiceType
                        .Where(st =>
                            (filter.IsCourse == null || st.isCourse == filter.IsCourse)
                            && (filter.SportId == null || st.SportType.id == filter.SportId))
                        .Take(TakeTop).ToListAsync())
                    {
                        ret.Add(ServiceTypeConverter.DataAccsessToDto(st));
                    }
                }
            }
            catch (Exception)
            {
                ret = null;
            }
            return ret;
        }

        /// <summary>
        ///     Method adding new service type
        /// </summary>
        /// <param name="serviceType">Dto service type to add</param>
        /// <returns>Success</returns>
        public async Task<bool> AddServiceType(DtoServiceType serviceType)
        {
            try
            {
                using (var data = Context)
                {
                    var sport = await data.SportType.FirstOrDefaultAsync(s => s.id == serviceType.SportTypeId);
                    if (sport == null)
                        return false;
                    var dbObject = ServiceTypeConverter.DtoToDataAccess(serviceType);
                    dbObject.SportType = sport;
                    data.ServiceType.Add(dbObject);
                    data.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}