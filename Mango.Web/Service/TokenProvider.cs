using Mango.Web.Service.IService;
using Mango.Web.Utility;

namespace Mango.Web.Service
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public TokenProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
        }

        public void ClearToken()
        {
            var context = _contextAccessor.HttpContext;
            if (context == null)
            {
                throw new InvalidOperationException("HTTP context is not available.");
            }

            context.Response.Cookies.Delete(SD.TokenCookie);
        }

        public string? GetToken()
        {
            var context = _contextAccessor.HttpContext;
            if (context == null)
            {
                throw new InvalidOperationException("HTTP context is not available.");
            }

            context.Request.Cookies.TryGetValue(SD.TokenCookie, out var token);
            return token;
        }

        public void SetToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Token cannot be null or empty.", nameof(token));
            }

            var context = _contextAccessor.HttpContext;
            if (context == null)
            {
                throw new InvalidOperationException("HTTP context is not available.");
            }

            context.Response.Cookies.Append(SD.TokenCookie, token);
        }
    }
}
