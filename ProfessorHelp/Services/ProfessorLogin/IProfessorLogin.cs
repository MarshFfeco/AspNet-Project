using ProfessorHelp.Models.Entity;

namespace ProfessorHelp.Services.ProfessorLogin;

public interface IProfessorLogin
{
    Task<Professor> IsLogin();
}
