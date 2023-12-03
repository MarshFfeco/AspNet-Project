using AutoMapper;
using ProfessorHelp.Interfaces;
using ProfessorHelp.Services.ProfessorLogin;
using ProfessorHelp.Shared.Comunication.Request.Matter;
using ProfessorHelp.Shared.Comunication.Response.Matter;
using ProfessorHelp.Shared.Exception.ExceptionBase;
using ProfessorHelp.UseCase.Matter.Interfaces;
using ProfessorHelp.UseCase.Matter.validation;

namespace ProfessorHelp.UseCase.Matter;

public class CreateMatterUseCase : ICreateMatterUseCase
{
    private readonly IMatterWriteOnlyRepository _repoWrite;
    private IMapper _mapper;
    private readonly IProfessorLogin _professorLogin;

    public CreateMatterUseCase(IMatterWriteOnlyRepository repoWrite, IMapper mapper, IProfessorLogin professorLogin)
    {
        _repoWrite = repoWrite;
        _mapper = mapper;
        _professorLogin = professorLogin;
    }

    public async Task<ResponseMatterCreate> Execute(RequestMatterCreate request)
    {
        Validator(request);

        var professorLogin = await _professorLogin.IsLogin();

        var matter = _mapper.Map<Models.Entity.Matter>(request);
        matter.Professor_Id = professorLogin.Id;

        await _repoWrite.CreateMatter(matter);

        return _mapper.Map<ResponseMatterCreate>(matter);
    }

    private void Validator(RequestMatterCreate request)
    {
        var validator = new CreateMatterValidato();
        var resultado = validator.Validate(request);

        if(!resultado.IsValid)
        {
            List<string> msgError = resultado.Errors.Select(c => c.ErrorMessage).ToList();
            throw new ValidationErroException(msgError);
        }
    }
}
