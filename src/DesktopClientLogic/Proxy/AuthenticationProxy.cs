using System.Threading.Tasks;
using DesktopClientLogic.ViewModelObjects;
using Shared.DtoObjects;
using Shared.RequestsResponses;

namespace DesktopClientLogic.Proxy
{
    public class AuthenticationProxy : WebApiProxy
    {
        public AuthenticationProxy() : base("Authentication")
        {
        }

        public async Task<SignInResponse> Authenticate(SignIn signInObject)
        {
            return await PostUnsafe<DtoSignIn, SignInResponse>("Authenticate",
                new DtoSignIn {EMail = signInObject.EMail, Password = signInObject.Password});
        }

        public async Task<SignInResponse> AuthenticateCustomer(SignIn signInObject)
        {
            return await PostUnsafe<DtoSignIn, SignInResponse>("AuthenticateCustomer",
                new DtoSignIn {EMail = signInObject.EMail, Password = signInObject.Password});
        }
    }
}