using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OnlineStore.Domain.Exceptions;
using OnlineStore.HttpModels.Responses;

namespace OnlineStore.WebApi.FilterExceptions;

public class CentralizedExceptionHandlingFilter : Attribute, IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var message = TryGetMessageFromException(context);
        if (message == null) return;
        context.Result = new ObjectResult(new ErrorResponse(message))
        {
            StatusCode = 400
        };
        context.ExceptionHandled = true;
    }


    private string? TryGetMessageFromException(ExceptionContext context)
    {
        return context.Exception switch
        {
            EmailAlreadyExistsException => "This Email already exists",
            EmailNotFoundException => "This Email was not found",
            WrongPasswordException => "Invalid Password",
            _ =>null
        };
    }
}



