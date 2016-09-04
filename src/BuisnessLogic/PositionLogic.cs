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
    ///     Static class containing Business Logic for Position
    /// </summary>
    public class PositionLogic
    {
        public PositionLogic()
        {
            Context = new ExtremeSportDBEntities();
        }

        /// <summary>
        ///     Data Context
        /// </summary>
        public ExtremeSportDBEntities Context { get; set; }

        /// <summary>
        ///     Method returns all Positions From Database
        /// </summary>
        /// <returns>Collection of Positions</returns>
        public async Task<ObservableCollection<DtoPosition>> GetAllPositions()
        {
            var ret = new ObservableCollection<DtoPosition>();
            using (var data = Context)
                foreach (var item in await (from item in data.Position select item).ToListAsync())
                    ret.Add(PositionConverter.DataAccsessToDto(item));
            return ret;
        }

        /// <summary>
        ///     Returns Position with specific ID
        /// </summary>
        /// <param name="positionId">Position ID</param>
        /// <returns>Position Business Object</returns>
        public async Task<DtoPosition> GetPosition(int positionId)
        {
            try
            {
                using (var data = Context)
                    return
                        PositionConverter.DataAccsessToDto(
                            await
                                (from item in data.Position where item.id == positionId select item)
                                    .FirstOrDefaultAsync());
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        ///     Adding or Updating Position in a database
        /// </summary>
        /// <param name="position">Business Object Position</param>
        /// <returns>True if succeeded and false otherwise</returns>
        public async Task<bool> SavePosition(DtoPosition position)
        {
            try
            {
                using (var data = Context)
                {
                    var p =
                        await
                            (from item in data.Position where position.Id == item.id select item).FirstOrDefaultAsync();
                    // Updating Position
                    if (p != null)
                    {
                        p.name = position.Name;
                        p.description = position.Description;
                    }
                    // Adding new Position
                    else
                    {
                        if (await (from item in data.Position where position.Name == item.name select item).AnyAsync())
                            return false;
                        data.Position.Add(PositionConverter.DtoToDataAccess(position));
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
        ///     Removes position with specific ID
        /// </summary>
        /// <param name="positionId">Position ID</param>
        /// <returns>True if Succeeded and false otherwise</returns>
        public async Task<bool> RemovePosition(int positionId)
        {
            try
            {
                using (var data = Context)
                {
                    var p = await (from item in data.Position where item.id == positionId select item).FirstAsync();
                    if (p.Employee.Count != 0)
                        return false;
                    data.Position.Remove(p);
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