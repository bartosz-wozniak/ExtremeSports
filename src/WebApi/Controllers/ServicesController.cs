using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BuisnessLogic;
using Shared;
using Shared.DtoObjects;
using Shared.Filters;
using Shared.RequestsResponses;

namespace WebApi.Controllers
{
    /// <summary>
    ///     Services controller
    /// </summary>
    [RequireHttps]
    public class ServicesController : ApiController
    {
        /// <summary>
        ///     Get services
        /// </summary>
        /// <param name="request">Service filter</param>
        /// <returns>Is success</returns>
        [RequireHttps]
        [HttpPost]
        public async Task<HttpResponseMessage> GetServices(Request<ServiceFilter> request)
        {
            if (!AuthenticationLogic.IsTokenCorrect(request.Token, request.Login))
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            var response = Request.CreateResponse(HttpStatusCode.OK,
                await new ServiceLogic().GetServices(request.Content));
            return response;
        }

        /// <summary>
        ///     Get service
        /// </summary>
        /// <param name="id">Service id</param>
        /// <returns>Service</returns>
        [RequireHttps]
        [HttpPost]
        public async Task<HttpResponseMessage> GetService(Request<int> id)
        {
            if (!AuthenticationLogic.IsTokenCorrect(id.Token, id.Login))
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            var response = Request.CreateResponse(HttpStatusCode.OK, await new ServiceLogic().GetCourse(id.Content));
            return response;
        }

        /// <summary>
        ///     Edit course
        /// </summary>
        /// <param name="s">Dto course</param>
        /// <returns>Is success</returns>
        [RequireHttps]
        [HttpPost]
        public async Task<HttpResponseMessage> EditCourse(Request<DtoService> s)
        {
            if (!AuthenticationLogic.IsTokenCorrect(s.Token, s.Login))
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            var response = Request.CreateResponse(HttpStatusCode.OK, await new ServiceLogic().EditCourse(s.Content));
            return response;
        }

        /// <summary>
        ///     Add course
        /// </summary>
        /// <param name="s">Dto course</param>
        /// <returns>Is success</returns>
        [RequireHttps]
        [HttpPost]
        public async Task<HttpResponseMessage> AddCourse(Request<DtoService> s)
        {
            if (!AuthenticationLogic.IsTokenCorrect(s.Token, s.Login))
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            var response = Request.CreateResponse(HttpStatusCode.OK, await new ServiceLogic().AddService(s.Content));
            return response;
        }

        [RequireHttps]
        [HttpPost]
        public async Task<HttpResponseMessage> RemoveCourse(Request<int> id)
        {
            if (!AuthenticationLogic.IsTokenCorrect(id.Token, id.Login))
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            var response = Request.CreateResponse(HttpStatusCode.OK, await new ServiceLogic().RemoveCourse(id.Content));
            return response;
        }

        [RequireHttps]
        [HttpPost]
        public async Task<HttpResponseMessage> EnrolCourse(Request<EnrolRequest> request)
        {
            if (!AuthenticationLogic.IsTokenCorrect(request.Token, request.Login))
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            var response = Request.CreateResponse(HttpStatusCode.OK,
                await new ServiceLogic().EnrolCourse(request.Content.CustomerId, request.Content.ServiceId));
            return response;
        }

        [RequireHttps]
        [HttpPost]
        public async Task<HttpResponseMessage> EnrolSingleService(Request<DtoService> s)
        {
            if (!AuthenticationLogic.IsTokenCorrect(s.Token, s.Login))
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            var response = Request.CreateResponse(HttpStatusCode.OK,
                await new ServiceLogic().EnrolSingleService(s.Content));
            return response;
        }
    }
}