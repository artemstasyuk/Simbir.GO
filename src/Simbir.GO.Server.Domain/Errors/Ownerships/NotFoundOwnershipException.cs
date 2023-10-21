using System.Net;

namespace Simbir.GO.Server.Domain.Errors.Ownerships;

public class NotFoundOwnershipException : Exception, IApplicationException
{
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    public string ErrorMessage => "Not found ownership";
    public string ProblemDetails => "Not found ownership in db.";
}