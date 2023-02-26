﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OnlineStore.Domain.Exceptions;
using OnlineStore.HttpModels.Responses;

namespace OnlineStore.WebApi.FilterExceptions;

public class CentralizedExceptionHandlingFilter : Attribute , IExceptionFilter
{
    private readonly ILogger<CentralizedExceptionHandlingFilter> _logger;

    public CentralizedExceptionHandlingFilter(ILogger<CentralizedExceptionHandlingFilter> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    public void OnException(ExceptionContext context)
    {
        var message = TryGetMessageFromException(context);
        if (message != null)
        {
            context.Result = new ObjectResult(new ErrorResponse(message));
            context.ExceptionHandled = true;
        }
    }


    private string? TryGetMessageFromException(ExceptionContext context)
    {
        return context.Exception switch
        {
            EmailAlreadyExistsException => ("This Email has already exists"),
            EmailNotFoundException => ("This Email was not found"),
            WrongPasswordException => ("Invalid Password"),
            _ =>null
        };
    }
}



