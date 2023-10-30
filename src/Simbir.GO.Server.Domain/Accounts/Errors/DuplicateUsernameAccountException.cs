using System.Net;
using Simbir.GO.Server.Domain.Common;

namespace Simbir.GO.Server.Domain.Accounts.Errors;

public class DuplicateUsernameAccountException : Exception, IApplicationException
{
    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;
    public string ErrorMessage => "Duplicate username";
    public string ProblemDetails => "A user with the same name already exists";
}
