using System.Net;
using HQ.Application.Dtos;
using HQ.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HQ.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is ExceptionBase)
        {
            HandlerProjectException(context);
        }
        else
        {
            ThrowUnknowException(context);
        }
    }
    
    private void ThrowUnknowException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new ResponseErrorJson("Erro desconhecido !"));
    }
    private void HandlerProjectException(ExceptionContext context)
    {
        if (context.Exception is ValidationErrorException)
        {
            var exception = context.Exception as ValidationErrorException;
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new BadRequestObjectResult(new ResponseErrorJson(exception.GetErrorMessages()));
        }
        
        // if (context.Exception is InvalidLoginException)
        // {
        //     context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        //     context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(context.Exception.Message));
        // }
        // else if (context.Exception is ValidationErrorException)
        // {
        //     var exception = context.Exception as ValidationErrorException;
        //     context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        //     context.Result = new BadRequestObjectResult(new ResponseErrorJson(exception.GetErrorMessages()));
        // }
        // else if (context.Exception is NotFoundException)
        // {
        //     context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
        //     context.Result = new NotFoundObjectResult(new ResponseErrorJson(context.Exception.Message));
        // }
    }

    
}