using System.Net;
using Simbir.GO.Domain.Common;

namespace Simbir.GO.Domain.Transports.Errors;

public class NotFoundTransportException : Exception, IApplicationException
{
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    public string ErrorMessage => "Not found transport";
    public string ProblemDetails => "Not found transport in db.";
}