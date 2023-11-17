using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProfessorHelp.Shared.Comunication.Response;
using ProfessorHelp.Shared.Exception.ExceptionBase;
using System.Net;

namespace ProfessorHelp.Shared.Exception.Filter;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is ProfessorHelpException)
        {
            ProcessProfessorHelpException(context);
        } else
        {
            SendUnknownException(context);
        }
    }

    private void ProcessProfessorHelpException(ExceptionContext context)
    {
        if(context.Exception is ValidationErroException)
        {
            ProcessValidationException(context);
        }
    }

    private void ProcessValidationException(ExceptionContext context)
    {
        var errException = context.Exception as ValidationErroException;

        if (errException is null)
        {
            throw new ArgumentException("Without Message!");
        }
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(new ResponseErro(errException.MsgsError));
    }

    private void SendUnknownException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new ResponseErro(ResourceErrorMessage.UNKNOWN_ERRO));
    }
}
