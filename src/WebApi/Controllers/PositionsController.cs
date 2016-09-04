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
    public class PositionsController : ApiController
    {
        [RequireHttps]
        [HttpPost]
        public async Task<HttpResponseMessage> GetPositions(Request<int> r)
        {
            if (!AuthenticationLogic.IsTokenCorrect(r.Token, r.Login))
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            var response = Request.CreateResponse(HttpStatusCode.OK, await new PositionLogic().GetAllPositions());
            return response;
        }

        [RequireHttps]
        [HttpPost]
        public async Task<HttpResponseMessage> RemovePosition(Request<int> id)
        {
            if (!AuthenticationLogic.IsTokenCorrect(id.Token, id.Login))
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            var response = Request.CreateResponse(HttpStatusCode.OK,
                await new PositionLogic().RemovePosition(id.Content));
            return response;
        }

        [RequireHttps]
        [HttpPost]
        public async Task<HttpResponseMessage> GetPosition(Request<int> id)
        {
            if (!AuthenticationLogic.IsTokenCorrect(id.Token, id.Login))
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            var response = Request.CreateResponse(HttpStatusCode.OK, await new PositionLogic().GetPosition(id.Content));
            return response;
        }

        [RequireHttps]
        [HttpPost]
        public async Task<HttpResponseMessage> SavePosition(Request<DtoPosition> p)
        {
            if (!AuthenticationLogic.IsTokenCorrect(p.Token, p.Login))
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            var response = Request.CreateResponse(HttpStatusCode.OK, await new PositionLogic().SavePosition(p.Content));
            return response;
        }
    }
}