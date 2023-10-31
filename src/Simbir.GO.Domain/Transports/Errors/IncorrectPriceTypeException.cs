using System.Net;
using Simbir.GO.Domain.Common;

namespace Simbir.GO.Domain.Transports.Errors;

public class IncorrectTransportTypeException : Exception, IApplicationException
{
    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    public string ErrorMessage => "Incorrect transport type";
    public string ProblemDetails => "Incorrect transport type";
}