using System.Net;
using Simbir.GO.Domain.Common;

namespace Simbir.GO.Domain.Rents.Errors;

public class NotFoundRentException : Exception, IApplicationException
{
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    public string ErrorMessage => "Not found rent";
    public string ProblemDetails => "Not found rent in db.";
}