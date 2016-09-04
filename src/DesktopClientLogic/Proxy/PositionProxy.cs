using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DesktopClientLogic.Converters;
using DesktopClientLogic.ViewModelObjects;
using Shared.DtoObjects;

namespace DesktopClientLogic.Proxy
{
    public class PositionProxy : WebApiProxy
    {
        public PositionProxy() : base("Positions")
        {
        }

        public async Task<ObservableCollection<Position>> GetPositions()
        {
            var ret = new ObservableCollection<Position>();
            foreach (var p in await Post<int, ObservableCollection<DtoPosition>>("GetPositions", -1))
                ret.Add(PositionConverter.DtoToViewModel(p));
            return ret;
        }

        public async Task<Position> GetPosition(int id)
        {
            return PositionConverter.DtoToViewModel(await Post<int, DtoPosition>("GetPosition", id));
        }

        public async Task<bool> RemovePosition(int id)
        {
            return await Post<int, bool>("RemovePosition", id);
        }

        public async Task<bool> SavePosition(Position p)
        {
            return await Post<DtoPosition, bool>("SavePosition", PositionConverter.ViewModelToDto(p));
        }
    }
}