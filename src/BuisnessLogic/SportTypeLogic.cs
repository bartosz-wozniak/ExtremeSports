using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using BuisnessLogic.Converters;
using DataAccess;
using Shared.DtoObjects;

namespace BuisnessLogic
{
    /// <summary>
    ///     Static class containing Business Logic for Sport Type
    /// </summary>
    public class SportTypeLogic
    {
        public SportTypeLogic()
        {
            Context = new ExtremeSportDBEntities();
        }

        /// <summary>
        ///     Data Context
        /// </summary>
        public ExtremeSportDBEntities Context { get; set; }

        /// <summary>
        ///     Method returns all Sport Types From Database
        /// </summary>
        /// <returns>Collection of Sport Types</returns>
        public async Task<ObservableCollection<DtoSportType>> GetAllSportTypes()
        {
            var ret = new ObservableCollection<DtoSportType>();
            using (var data = Context)
                foreach (var item in await (from item in data.SportType select item).ToListAsync())
                    ret.Add(SportTypeConverter.DataAccsessToDto(item));
            return ret;
        }

        /// <summary>
        ///     Returns SportType with specific ID
        /// </summary>
        /// <param name="sportTypeId">SportType ID</param>
        /// <returns>SportType Business Object</returns>
        public async Task<DtoSportType> GetSportType(int sportTypeId)
        {
            try
            {
                using (var data = Context)
                    return
                        SportTypeConverter.DataAccsessToDto(
                            await
                                (from item in data.SportType where item.id == sportTypeId select item)
                                    .FirstOrDefaultAsync());
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        ///     Adding or Updating Sport Type in a database
        /// </summary>
        /// <param name="sportType">Business Object Sport Type</param>
        /// <returns>True if succeeded and false otherwise</returns>
        public async Task<bool> SaveSportType(DtoSportType sportType)
        {
            try
            {
                using (var data = Context)
                {
                    var st =
                        await
                            (from item in data.SportType where sportType.Id == item.id select item).FirstOrDefaultAsync();
                    // Updating Sport Type
                    if (st != null)
                    {
                        st.name = sportType.Name;
                        st.description = sportType.Description;
                    }
                    // Adding new Sport Type
                    else
                    {
                        if (await (from item in data.SportType where sportType.Name == item.name select item).AnyAsync())
                            return false;
                        data.SportType.Add(SportTypeConverter.DtoToDataAccess(sportType));
                    }
                    await data.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        ///     Removes sport type with specific ID
        /// </summary>
        /// <param name="sportTypeId">Sport Type ID</param>
        /// <returns>True if Succeeded and false otherwise</returns>
        public async Task<bool> RemoveSportType(int sportTypeId)
        {
            try
            {
                using (var data = Context)
                {
                    var st = await (from item in data.SportType where item.id == sportTypeId select item).FirstAsync();
                    if (st.Employee.Count != 0 || st.ServiceType.Count != 0)
                        return false;
                    data.SportType.Remove(st);
                    await data.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}