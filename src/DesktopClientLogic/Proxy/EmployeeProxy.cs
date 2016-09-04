using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DesktopClientLogic.Converters;
using DesktopClientLogic.ViewModelObjects;
using Shared.DtoObjects;

namespace DesktopClientLogic.Proxy
{
    public class EmployeeProxy : WebApiProxy
    {
        public EmployeeProxy() : base("Employees")
        {
        }

        public async Task<ObservableCollection<Employee>> GetEmployees(string filter)
        {
            var ret = new ObservableCollection<Employee>();
            foreach (var e in await Post<string, ObservableCollection<DtoEmployee>>("GetEmployees", filter))
                ret.Add(EmployeeConverter.DtoToViewModel(e));
            return ret;
        }

        public async Task<Employee> GetEmployee(int id)
        {
            return EmployeeConverter.DtoToViewModel(await Post<int, DtoEmployee>("GetEmployee", id));
        }

        public async Task<bool> RemoveEmployee(int id)
        {
            return await Post<int, bool>("RemoveEmployee", id);
        }

        public async Task<bool> SaveEmployee(Employee e)
        {
            return await Post<DtoEmployee, bool>("SaveEmployee", EmployeeConverter.ViewModelToDto(e));
        }
    }
}