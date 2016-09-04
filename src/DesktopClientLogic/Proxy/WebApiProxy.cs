using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Windows;
using Shared;

namespace DesktopClientLogic.Proxy
{
    public abstract class WebApiProxy
    {
        private readonly MediaTypeFormatter _formatter;
        private readonly HttpClient _httpClient;
        private readonly string _uri = ConfigurationManager.AppSettings["BaseServicesUrl"];

        protected WebApiProxy(string controllerName)
        {
            _httpClient = HttpClientFactory.Create();
            _formatter = new JsonMediaTypeFormatter();
            _httpClient.BaseAddress = new Uri($"{_uri}/{controllerName}/");
        }

        protected Task<TResponse> Post<TRequest, TResponse>(TRequest request)
        {
            return Post<TRequest, TResponse>(string.Empty, request);
        }

        protected async Task<TResponse> Post<TRequest, TResponse>(string method, TRequest request)
        {
            var cookie = Application.GetCookie(new Uri(ConfigurationManager.AppSettings["CookiePath"]));
            if (cookie != null && cookie.Contains(";"))
                cookie = cookie.Split(';')[0];
            var data = cookie?.Split('|');
            var token = data?[0];
            var login = data?[1];
            var r = new Request<TRequest>(request, token, login);
            return await ExtractResponse<TResponse>(await _httpClient.PostAsync(method, r, _formatter));
        }

        protected async Task<TResponse> PostUnsafe<TRequest, TResponse>(string method, TRequest request)
        {
            return await ExtractResponse<TResponse>(await _httpClient.PostAsync(method, request, _formatter));
        }

        protected async Task<TResponse> Get<TResponse>(string uri = "")
        {
            return await ExtractResponse<TResponse>(await _httpClient.GetAsync(uri));
        }

        private async Task<TResponse> ExtractResponse<TResponse>(HttpResponseMessage response)
        {
            await VerifyStatusCode(response);
            return await response.Content.ReadAsAsync<TResponse>(new[] {_formatter});
        }

        private async Task VerifyStatusCode(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                //TODO: Dedicated exception type
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }
    }
}