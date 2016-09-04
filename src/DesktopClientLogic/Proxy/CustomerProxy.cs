using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DesktopClientLogic.Converters;
using DesktopClientLogic.ViewModelObjects;
using Shared.DtoObjects;

namespace DesktopClientLogic.Proxy
{
    public class CustomerProxy : WebApiProxy
    {
        public CustomerProxy() : base("Customers")
        {
        }

        public async Task<ObservableCollection<Customer>> GetCustomers(string filter)
        {
            var ret = new ObservableCollection<Customer>();
            foreach (var c in await Post<string, ObservableCollection<DtoCustomer>>("GetCustomers", filter))
                ret.Add(CustomerConverter.DtoToViewModel(c));
            return ret;
        }

        public async Task<Customer> GetCustomer(int id)
        {
            return CustomerConverter.DtoToViewModel(await Post<int, DtoCustomer>("GetCustomer", id));
        }

        public async Task<bool> RemoveCustomer(int id)
        {
            return await Post<int, bool>("RemoveCustomer", id);
        }

        public async Task<bool> SaveCustomer(Customer c)
        {
            return await Post<DtoCustomer, bool>("SaveCustomer", CustomerConverter.ViewModelToDto(c));
        }
    }
}