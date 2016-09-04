namespace Shared
{
    public class Request<TRequest>
    {
        public Request(TRequest content, string token, string login)
        {
            Content = content;
            Token = token;
            Login = login;
        }

        public TRequest Content { get; set; }
        public string Token { get; set; }
        public string Login { get; set; }
    }
}