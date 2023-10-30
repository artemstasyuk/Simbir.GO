using System.Net;
using Simbir.GO.Server.Domain.Common;

namespace Simbir.GO.Server.Domain.Accounts.Errors;

public class AccessDeniedAccountException : Exception, IApplicationException
{
    public HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;
    public string ErrorMessage => "AccessDenied";
    public string ProblemDetails => "AccessDenied";
}
