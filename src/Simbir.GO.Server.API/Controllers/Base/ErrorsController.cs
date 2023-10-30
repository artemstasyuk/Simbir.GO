using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Serilog;
using Simbir.GO.Server.Domain.Common;

namespace Simbir.GO.Server.API.Controllers.Base;

[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        //catch the validation error
        if (exception?.GetType() == typeof(ValidationException))
        {
            var validationException = (ValidationException)exception;
            return ValidationProblem(validationException.Errors);
        }

        var (statusCode, message, details) = exception switch
        {
            IApplicationException appException => ((int)appException.StatusCode, appException.ErrorMessage,
                appException.ProblemDetails),
            _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred", "")
        };

        Log.Error("Status Code {StatusCode}: {Message} ({Details})", statusCode, message, details);
        return Problem(statusCode: statusCode, title: message, detail: details);
    }

    private IActionResult ValidationProblem(IEnumerable<ValidationFailure> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();

        //all validation errors
        foreach (var failure in errors)
            modelStateDictionary.AddModelError(failure.PropertyName, failure.ErrorMessage);
        
        return ValidationProblem(modelStateDictionary);
    }
}
