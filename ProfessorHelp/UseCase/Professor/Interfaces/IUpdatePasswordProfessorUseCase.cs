using ProfessorHelp.Shared.Comunication.Request.Professor;

namespace ProfessorHelp.UseCase.Professor.Interfaces;

public interface IUpdatePasswordProfessorUseCase
{
    Task Execute(RequestUpdatePasswordProfessor request);
}
