using System.Net;
using Simbir.GO.Domain.Common;

namespace Simbir.GO.Domain.Accounts.Errors;

public class InvalidCredentialsAccountException : Exception, IApplicationException
{
    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    public string ErrorMessage => "Invalid credentials";
    public string ProblemDetails => "Invalid credentials";
}