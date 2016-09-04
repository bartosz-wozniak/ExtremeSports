using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BuisnessLogic;
using Shared;
using Shared.DtoObjects;

namespace WebApi.Controllers
{
    [RequireHttps]
    public class EmployeesController : ApiController
    {
        [RequireHttps]
        [HttpPost]
        public async Task<HttpResponseMessage> GetEmployees(Request<string> filter)
        {
            if (!AuthenticationLogic.IsTokenCorrect(filter.Token, filter.Login))
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            var response = Request.CreateResponse(HttpStatusCode.OK,
                await new EmployeeLogic().GetEmployees(filter.Content));
            return response;
        }

        [RequireHttps]
        [HttpPost]
        public async Task<HttpResponseMessage> RemoveEmployee(Request<int> id)
        {
            if (!AuthenticationLogic.IsTokenCorrect(id.Token, id.Login))
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            var response = Request.CreateResponse(HttpStatusCode.OK,
                await new EmployeeLogic().RemoveEmployee(id.Content));
            return response;
        }

        [RequireHttps]
        [HttpPost]
        public async Task<HttpResponseMessage> GetEmployee(Request<int> id)
        {
            if (!AuthenticationLogic.IsTokenCorrect(id.Token, id.Login))
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            var response = Request.CreateResponse(HttpStatusCode.OK, await new EmployeeLogic().GetEmployee(id.Content));
            return response;
        }

        [RequireHttps]
        [HttpPost]
        public async Task<HttpResponseMessage> SaveEmployee(Request<DtoEmployee> e)
        {
            if (!AuthenticationLogic.IsTokenCorrect(e.Token, e.Login))
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            var response = Request.CreateResponse(HttpStatusCode.OK, await new EmployeeLogic().SaveEmployee(e.Content));
            return response;
        }
    }
}