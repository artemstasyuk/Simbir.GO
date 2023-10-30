using System.Net;
using Simbir.GO.Server.Domain.Common;

namespace Simbir.GO.Server.Domain.Rents.Errors;

public class IncorrectPriceTypeException : Exception, IApplicationException
{
    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    public string ErrorMessage => "Incorrect price type";
    public string ProblemDetails => "Incorrect price type";
}