using System.Net;
using Simbir.GO.Domain.Common;

namespace Simbir.GO.Domain.Accounts.Errors;

public class NotFoundAccountException : Exception, IApplicationException
{
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    public string ErrorMessage => "Not found account";
    public string ProblemDetails => "Not found account in db.";
}