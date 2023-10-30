using System.Net;
using Simbir.GO.Server.Domain.Common;

namespace Simbir.GO.Server.Domain.Rents.Errors;

public class InvalidCredentialsRentException : Exception, IApplicationException
{
    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    public string ErrorMessage => "Invalid credentials";
    public string ProblemDetails => "Invalid credentials";
}