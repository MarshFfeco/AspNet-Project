using ProfessorHelp.Models.Entity;

namespace ProfessorHelp.Interfaces;

public interface IProfessorReadOnlyRepository
{
    Task<bool> isExistProfessorWithEmail(string email);
    Task<Professor> Login(string email, string password);
    Task<Professor> FindByEmail(string email);

    Task<Professor> FindById(long id);
}
