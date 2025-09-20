using Application.Common.Dtos;
using Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.UserManagment.Filters;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;
    public ApiExceptionFilterAttribute()
    {
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
        {
             { typeof(ValidatorException), HandleValidationException }
        };
    }

    public override void OnException(ExceptionContext context)
    {
        HandleException(context);
                
        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        Type type = context.Exception.GetType();
        if (_exceptionHandlers.TryGetValue(type, out var value))
        {
            value.Invoke(context);
            return;
        }

        if (!context.ModelState.IsValid)
        {
            HandleInvalidModelStateException(context);
            return;
        }
        HandleDefaultException(context);
    }

    private static void HandleInvalidModelStateException(ExceptionContext context)
    {
        var details = new ValidationProblemDetails(context.ModelState);
        var response = new ResponseDto
        {
            ErrorCode = StatusCodes.Status400BadRequest,
            ErrorDescription = string.Join(" - ", details.Errors.Values.SelectMany(x => x))
        };
        context.Result = new BadRequestObjectResult(response);

        context.ExceptionHandled = true;
    }

    private void HandleValidationException(ExceptionContext context)
    {
        var exception = (ValidatorException)context.Exception;

        var response = new ResponseDto
        {
            ErrorCode = StatusCodes.Status400BadRequest,
            ErrorDescription = string.Join(" - ", exception.Errors!.Values.SelectMany(x => x))
        };

        context.Result = new BadRequestObjectResult(response);
        context.ExceptionHandled = true;
    }

    private static void HandleDefaultException(ExceptionContext context)
    {
        var exception = context.Exception;
        var response = new ResponseDto
        {
            ErrorCode = StatusCodes.Status500InternalServerError,
            ErrorDescription = exception.Message
        };

        context.Result = new ObjectResult(response)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
        context.ExceptionHandled = true;
    }
}
