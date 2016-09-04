using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DesktopClientLogic.Converters;
using DesktopClientLogic.ViewModelObjects;
using Shared.DtoObjects;
using Shared.Filters;

namespace DesktopClientLogic.Proxy
{
    public class ServiceTypeProxy : WebApiProxy
    {
        public ServiceTypeProxy() : base("ServiceTypes")
        {
        }

        public async Task<ObservableCollection<ServiceType>> GetServiceTypes(ServiceTypeFilter serviceTypeFilter)
        {
            var ret = new ObservableCollection<ServiceType>();
            foreach (
                var s in
                    await
                        Post<ServiceTypeFilter, ObservableCollection<DtoServiceType>>("GetServiceTypes",
                            serviceTypeFilter))
                ret.Add(ServiceTypeConverter.DtoToViewModel(s));
            return ret;
        }

        public async Task<bool> AddServiceType(ServiceType st)
        {
            return await Post<DtoServiceType, bool>("AddServiceType", ServiceTypeConverter.ViewModelToDto(st));
        }
    }
}