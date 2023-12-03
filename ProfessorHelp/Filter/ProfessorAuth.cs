using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using ProfessorHelp.Interfaces;
using ProfessorHelp.Services.Token;
using ProfessorHelp.Shared.Comunication.Response.Professor;
using ProfessorHelp.Shared.Exception;

namespace ProfessorHelp.Filter;

public class ProfessorAuth : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    private readonly TokenController _tokenController;
    private readonly IProfessorReadOnlyRepository _professor;

    public ProfessorAuth(TokenController tokenController, IProfessorReadOnlyRepository professor)
    {
        _tokenController = tokenController;
        _professor = professor;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            string token = TokenResquest(context);
            string email = _tokenController.RecoverEmailProfessor(token);

            var entity = await _professor.FindByEmail(email);

            if (entity is null)
            {
                throw new Exception();
            }
        } 
        catch (SecurityTokenExpiredException)
        {
            TokenExpired(context);
        } 
        catch 
        {
            ProfessorUnauthorized(context);
        }
    }

    private string TokenResquest(AuthorizationFilterContext context)
    {
        string auth = context.HttpContext.Request.Headers["Authorization"].ToString();

        if(string.IsNullOrWhiteSpace(auth))
        {
            throw new Exception();
        }

        return auth["Bearer".Length..].Trim();
    }

    private void TokenExpired(AuthorizationFilterContext context)
    {
        context.Result = new UnauthorizedObjectResult(new ResponseErro(ResourceErrorMessage.TOKEN_EXPIRED));
    }

    private void ProfessorUnauthorized(AuthorizationFilterContext context)
    {
        context.Result = new UnauthorizedObjectResult(new ResponseErro(ResourceErrorMessage.PROFESSOR_UNAUTHORIZED));
    }
}
