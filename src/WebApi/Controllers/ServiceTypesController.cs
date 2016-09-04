using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BuisnessLogic;
using Shared;
using Shared.DtoObjects;
using Shared.Filters;

namespace WebApi.Controllers
{
    [RequireHttps]
    public class ServiceTypesController : ApiController
    {
        [RequireHttps]
        [HttpPost]
        public async Task<HttpResponseMessage> GetServiceTypes(Request<ServiceTypeFilter> request)
        {
            if (!AuthenticationLogic.IsTokenCorrect(request.Token, request.Login))
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            var response = Request.CreateResponse(HttpStatusCode.OK,
                await new ServiceTypeLogic().GetServiceTypes(request.Content));
            return response;
        }

        [RequireHttps]
        [HttpPost]
        public async Task<HttpResponseMessage> AddServiceType(Request<DtoServiceType> s)
        {
            if (!AuthenticationLogic.IsTokenCorrect(s.Token, s.Login))
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            var response = Request.CreateResponse(HttpStatusCode.OK,
                await new ServiceTypeLogic().AddServiceType(s.Content));
            return response;
        }
    }
}