using System.Net;
using Simbir.GO.Server.ApplicationCore.Interfaces.Authentication;

namespace Simbir.GO.API.Middlewares;

public class AccessTokeValidationMiddleware
{
    private readonly RequestDelegate _next;

    public AccessTokeValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, ITokenizer tokenizer)
    {
        if (context.Request.Headers.TryGetValue("Authorization", out var header))
        {
            var authorizationHeader = header.ToString();
            var accessToken = authorizationHeader.Replace("bearer ", string.Empty);
            
            if (!tokenizer.IsValid(accessToken))
            { 
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Unauthorized. The token has been revoked.");
                return;
            }
        }

        await _next(context);
    }
}