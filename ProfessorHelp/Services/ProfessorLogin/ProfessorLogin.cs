using ProfessorHelp.Interfaces;
using ProfessorHelp.Models.Entity;
using ProfessorHelp.Services.Token;

namespace ProfessorHelp.Services.ProfessorLogin;

public class ProfessorLogin : IProfessorLogin
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly TokenController _tokenController;
    private readonly IProfessorReadOnlyRepository _professor;

    public ProfessorLogin(IHttpContextAccessor httpContextAccessor, TokenController tokenController, IProfessorReadOnlyRepository professor)
    {
        _httpContextAccessor = httpContextAccessor;
        _tokenController = tokenController;
        _professor = professor;
    }

    public async Task<Professor> IsLogin()
    {
        string auth = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

        string token = auth["Bearer".Length..].Trim();

        var email = _tokenController.RecoverEmailProfessor(token);

        var entity = await _professor.FindByEmail(email);

        return entity;
    }
}
