using ProfessorHelp.Shared.Comunication.Request.Matter;
using ProfessorHelp.Shared.Comunication.Response.Matter;

namespace ProfessorHelp.UseCase.Matter.Interfaces;

public interface ICreateMatterUseCase
{
    Task<ResponseMatterCreate> Execute(RequestMatterCreate request);
}
