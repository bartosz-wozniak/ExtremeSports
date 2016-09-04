using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DesktopClientLogic.Converters;
using DesktopClientLogic.ViewModelObjects;
using Shared.DtoObjects;

namespace DesktopClientLogic.Proxy
{
    public class SportTypeProxy : WebApiProxy
    {
        public SportTypeProxy() : base("SportTypes")
        {
        }

        public async Task<ObservableCollection<SportType>> GetSportTypes()
        {
            var ret = new ObservableCollection<SportType>();
            foreach (var s in await Post<int, ObservableCollection<DtoSportType>>("GetSports", -1))
                ret.Add(SportTypeConverter.DtoToViewModel(s));
            return ret;
        }

        public async Task<SportType> GetSportType(int id)
        {
            return SportTypeConverter.DtoToViewModel(await Post<int, DtoSportType>("GetSportType", id));
        }

        public async Task<bool> RemoveSportType(int id)
        {
            return await Post<int, bool>("RemoveSportType", id);
        }

        public async Task<bool> SaveSportType(SportType st)
        {
            return await Post<DtoSportType, bool>("SaveSportType", SportTypeConverter.ViewModelToDto(st));
        }
    }
}