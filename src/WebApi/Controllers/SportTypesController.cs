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
    public class SportTypesController : ApiController
    {
        [RequireHttps]
        [HttpPost]
        public async Task<HttpResponseMessage> GetSports(Request<int> r)
        {
            if (!AuthenticationLogic.IsTokenCorrect(r.Token, r.Login))
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            var response = Request.CreateResponse(HttpStatusCode.OK, await new SportTypeLogic().GetAllSportTypes());
            return response;
        }

        [RequireHttps]
        [HttpPost]
        public async Task<HttpResponseMessage> RemoveSportType(Request<int> id)
        {
            if (!AuthenticationLogic.IsTokenCorrect(id.Token, id.Login))
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            var response = Request.CreateResponse(HttpStatusCode.OK,
                await new SportTypeLogic().RemoveSportType(id.Content));
            return response;
        }

        [RequireHttps]
        [HttpPost]
        public async Task<HttpResponseMessage> GetSportType(Request<int> id)
        {
            if (!AuthenticationLogic.IsTokenCorrect(id.Token, id.Login))
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            var response = Request.CreateResponse(HttpStatusCode.OK, await new SportTypeLogic().GetSportType(id.Content));
            return response;
        }

        [RequireHttps]
        [HttpPost]
        public async Task<HttpResponseMessage> SaveSportType(Request<DtoSportType> st)
        {
            if (!AuthenticationLogic.IsTokenCorrect(st.Token, st.Login))
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            var response = Request.CreateResponse(HttpStatusCode.OK,
                await new SportTypeLogic().SaveSportType(st.Content));
            return response;
        }
    }
}