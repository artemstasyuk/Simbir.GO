using System.Net;
using Simbir.GO.Domain.Common;

namespace Simbir.GO.Domain.Accounts.Errors;

public class UnauthorizedException: Exception, IApplicationException
{
    public HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;
    public string ErrorMessage => "Unauthorized";
    public string ProblemDetails => "Unauthorized";
}