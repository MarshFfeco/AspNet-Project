using ProfessorHelp.Shared.Comunication.Request;
using ProfessorHelp.Shared.Comunication.Response;

namespace ProfessorHelp.Validator.Professor.Interfaces;

public interface ISignUpProfessorUseCase
{
    Task<ResponseSignUpProfessor> Execute(RequestSignUpProfessor request);
}
