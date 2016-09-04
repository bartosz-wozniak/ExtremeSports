using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BuisnessLogic;
using Shared.DtoObjects;
using Shared.RequestsResponses;

namespace WebApi.Controllers
{
    /// <summary>
    ///     Authentication controller
    /// </summary>
    [RequireHttps]
    public class AuthenticationController : ApiController
    {
        /// <summary>
        ///     Authentication
        /// </summary>
        /// <param name="signInData">Sign in data</param>
        /// <returns>Autorization result</returns>
        [RequireHttps]
        [HttpPost]
        public async Task<HttpResponseMessage> Authenticate(DtoSignIn signInData)
        {
            var authResult = await AuthenticationLogic.Authenticate(signInData);
            var token = AuthenticationLogic.GetToken(signInData.EMail);
            var resp = new SignInResponse {AuthorizationResult = authResult, Token = token};
            var response = Request.CreateResponse(HttpStatusCode.OK, resp);
            return response;
        }

        /// <summary>
        ///     Authentication Customer
        /// </summary>
        /// <param name="signInData">Sign in data</param>
        /// <returns>Autorization result</returns>
        [RequireHttps]
        [HttpPost]
        public async Task<HttpResponseMessage> AuthenticateCustomer(DtoSignIn signInData)
        {
            var authResult = await AuthenticationLogic.AuthenticateCustomer(signInData);
            var token = AuthenticationLogic.GetToken(signInData.EMail);
            var resp = new SignInResponse {AuthorizationResult = authResult, Token = token};
            var response = Request.CreateResponse(HttpStatusCode.OK, resp);
            return response;
        }
    }
}