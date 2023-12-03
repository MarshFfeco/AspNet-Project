using ProfessorHelp.Shared.Comunication.Request.Professor;
using ProfessorHelp.Shared.Comunication.Response.Professor;

namespace ProfessorHelp.Validator.Professor.Interfaces;

public interface ISignUpProfessorUseCase
{
    Task<ResponseLoginProfessor> Execute(RequestSignUpProfessor request);
}
