using System.Net;
using Simbir.GO.Server.Domain.Common;

namespace Simbir.GO.Server.Domain.Transports.Errors;

public class NotFoundTransportException : Exception, IApplicationException
{
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    public string ErrorMessage => "Not found transport";
    public string ProblemDetails => "Not found transport in db.";
}