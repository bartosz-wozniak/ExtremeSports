using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BuisnessLogic;
using Shared;
using Shared.DtoObjects;

namespace WebApi.Controllers
{
    /// <summary>
    ///     Customers controller
    /// </summary>
    [RequireHttps]
    public class CustomersController : ApiController
    {
        /// <summary>
        ///     Get filtered customers
        /// </summary>
        /// <param name="filter">filter</param>
        /// <returns>Customers</returns>
        [RequireHttps]
        [HttpPost]
        public async Task<HttpResponseMessage> GetCustomers(Request<string> filter)
        {
            var customerLogic = new CustomerLogic();
            if (!AuthenticationLogic.IsTokenCorrect(filter.Token, filter.Login))
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            var response = Request.CreateResponse(HttpStatusCode.OK, await customerLogic.GetCustomers(filter.Content));
            return response;
        }

        /// <summary>
        ///     Remove customers
        /// </summary>
        /// <param name="id">Cutomer id</param>
        /// <returns>Customer</returns>
        [RequireHttps]
        [HttpPost]
        public async Task<HttpResponseMessage> RemoveCustomer(Request<int> id)
        {
            if (!AuthenticationLogic.IsTokenCorrect(id.Token, id.Login))
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            var response = Request.CreateResponse(HttpStatusCode.OK,
                await new CustomerLogic().RemoveCustomer(id.Content));
            return response;
        }

        /// <summary>
        ///     Get customer
        /// </summary>
        /// <param name="id">Customer id</param>
        /// <returns>Customer</returns>
        [RequireHttps]
        [HttpPost]
        public async Task<HttpResponseMessage> GetCustomer(Request<int> id)
        {
            if (!AuthenticationLogic.IsTokenCorrect(id.Token, id.Login))
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            var response = Request.CreateResponse(HttpStatusCode.OK, await new CustomerLogic().GetCustomer(id.Content));
            return response;
        }

        /// <summary>
        ///     Save customer
        /// </summary>
        /// <param name="c">Customer</param>
        /// <returns>Bool is success</returns>
        [RequireHttps]
        [HttpPost]
        public async Task<HttpResponseMessage> SaveCustomer(Request<DtoCustomer> c)
        {
            if (!AuthenticationLogic.IsTokenCorrect(c.Token, c.Login))
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            var response = Request.CreateResponse(HttpStatusCode.OK, await new CustomerLogic().SaveCustomer(c.Content));
            return response;
        }
    }
}