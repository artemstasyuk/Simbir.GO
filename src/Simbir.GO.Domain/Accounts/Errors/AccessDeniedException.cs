using System.Net;
using Simbir.GO.Domain.Common;

namespace Simbir.GO.Domain.Accounts.Errors;

public class AccessDeniedException : Exception, IApplicationException
{
    public HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;
    public string ErrorMessage => "AccessDenied";
    public string ProblemDetails => "AccessDenied";
}
