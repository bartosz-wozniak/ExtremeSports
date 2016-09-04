using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DesktopClientLogic.Converters;
using DesktopClientLogic.ViewModelObjects;
using Shared.DtoObjects;
using Shared.Filters;
using Shared.RequestsResponses;

namespace DesktopClientLogic.Proxy
{
    public class ServicesProxy : WebApiProxy
    {
        public ServicesProxy() : base("Services")
        {
        }

        public async Task<ObservableCollection<Course>> GetCourses(ServiceFilter filter)
        {
            filter.IsCourse = true;
            var ret = new ObservableCollection<Course>();
            foreach (var c in await Post<ServiceFilter, ObservableCollection<DtoService>>("GetServices", filter))
                ret.Add(ServiceConverter.DtoToCourseViewModel(c));
            return ret;
        }

        public async Task<ObservableCollection<SingleService>> GetSingleServices(ServiceFilter filter)
        {
            filter.IsCourse = false;
            var ret = new ObservableCollection<SingleService>();
            foreach (var c in await Post<ServiceFilter, ObservableCollection<DtoService>>("GetServices", filter))
                ret.Add(ServiceConverter.DtoToViewModelSingleService(c));
            return ret;
        }

        public async Task<Course> GetCourse(int id)
        {
            return ServiceConverter.DtoToCourseViewModel(await Post<int, DtoService>("GetService", id));
        }

        public async Task<bool> EditCourse(Course course)
        {
            return await Post<DtoService, bool>("EditCourse", ServiceConverter.ViewModelCourseToDto(course));
        }

        public async Task<bool> AddCourse(Course course)
        {
            return await Post<DtoService, bool>("AddCourse", ServiceConverter.ViewModelCourseToDto(course));
        }

        public async Task<bool> RemoveCourse(int id)
        {
            return await Post<int, bool>("RemoveCourse", id);
        }

        public async Task<bool> EnrolCourse(int customerId, int courseId)
        {
            return
                await
                    Post<EnrolRequest, bool>("EnrolCourse",
                        new EnrolRequest {CustomerId = customerId, ServiceId = courseId});
        }

        public async Task<bool> EnrolSingleService(SingleService s)
        {
            return await Post<DtoService, bool>("EnrolSingleService", ServiceConverter.ViewModelSingleServiceToDto(s));
        }
    }
}