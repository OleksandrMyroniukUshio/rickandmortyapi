namespace RickAndMortyAPI.Middleware
{
    public class HeaderAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        public HeaderAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            const string headerKey = "X-SecretKey";
            const string headerValue = "PickleRick";
            if(context.Request.Headers.TryGetValue(headerKey, out var value) && value == headerValue) {
                await _next.Invoke(context);
            }
            else
            {
                context.Response.StatusCode = 401;
            }
        }
    }
}
