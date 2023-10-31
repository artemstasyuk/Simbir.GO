using System.Net;
using Simbir.GO.Domain.Common;

namespace Simbir.GO.Domain.Transports.Errors;

public class OutOfRangeLocationException: Exception, IApplicationException
{
    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    public string ErrorMessage => "Location out of range";
    public string ProblemDetails => "Location out of range";
}