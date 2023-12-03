using ProfessorHelp.Interfaces;
using ProfessorHelp.Services.Criptografia;
using ProfessorHelp.Services.Token;
using ProfessorHelp.Shared.Comunication.Request.Professor;
using ProfessorHelp.Shared.Comunication.Response.Professor;
using ProfessorHelp.Shared.Exception.ExceptionBase;
using ProfessorHelp.UseCase.Professor.validation;
using ProfessorHelp.Validator.Professor.Interfaces;

namespace ProfessorHelp.Validator.Professor;

public class LoginProfessorUseCase : ILoginProfessorUseCase
{
    private readonly IProfessorReadOnlyRepository _professorReadOnlyRepo;
    private readonly EncryptPassword _encriptPassword;
    private readonly TokenController _tokenController;

    public LoginProfessorUseCase(IProfessorReadOnlyRepository professorReadOnlyRepo, EncryptPassword encriptPassword, TokenController tokenController)
    {
        _professorReadOnlyRepo = professorReadOnlyRepo;
        _encriptPassword = encriptPassword;
        _tokenController = tokenController;
    }

    public async Task<ResponseProfessorLogin> Execute(RequestLoginProfessor request)
    {
        await Validator(request);

        var criptPassword = _encriptPassword.Encrypt(request.Password);

        var entity = await _professorReadOnlyRepo.Login(request.Email, criptPassword);

        if(entity is null) 
        {
            throw new LoginInvalidException();
        }

        return new ResponseProfessorLogin
        {
            Name = entity.First_Name,
            Token = _tokenController.GenerateToken(entity.Email),
        };
    }

    private async Task Validator(RequestLoginProfessor request)
    {
        var validator = new LoginProfessorValidator();
        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var messageError = result.Errors.Select(err => err.ErrorMessage).ToList();
            throw new ValidationErroException(messageError);
        }
    }
}
