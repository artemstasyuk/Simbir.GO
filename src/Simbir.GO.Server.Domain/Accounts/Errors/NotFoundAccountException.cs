using System.Net;
using Simbir.GO.Server.Domain.Common;

namespace Simbir.GO.Server.Domain.Accounts.Errors;

public class NotFoundAccountException : Exception, IApplicationException
{
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    public string ErrorMessage => "Not found account";
    public string ProblemDetails => "Not found account in db.";
}