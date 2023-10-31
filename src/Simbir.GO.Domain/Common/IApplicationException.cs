using System.Net;

namespace Simbir.GO.Domain.Common;

public interface IApplicationException
{
    public HttpStatusCode StatusCode { get; }
    public string ErrorMessage { get; }
    public string ProblemDetails { get; }
}