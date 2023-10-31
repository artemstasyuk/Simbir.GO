using System.Net;
using Simbir.GO.Domain.Common;

namespace Simbir.GO.Domain.Rents.Errors;

public class InvalidDateTimeFormatException : Exception, IApplicationException
{
    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    public string ErrorMessage => "Invalid DateTime Format";
    public string ProblemDetails => "Invalid DateTime Format";
}