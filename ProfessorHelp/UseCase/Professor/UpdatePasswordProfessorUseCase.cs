using ProfessorHelp.Interfaces;
using ProfessorHelp.Services.Criptografia;
using ProfessorHelp.Services.ProfessorLogin;
using ProfessorHelp.Shared.Comunication.Request.Professor;
using ProfessorHelp.Shared.Exception;
using ProfessorHelp.Shared.Exception.ExceptionBase;
using ProfessorHelp.UseCase.Professor.Interfaces;
using ProfessorHelp.UseCase.Professor.validation;

namespace ProfessorHelp.UseCase.Professor;

public class UpdatePasswordProfessorUseCase : IUpdatePasswordProfessorUseCase
{
    private readonly IProfessorLogin _login;
    private readonly IProfessorReadOnlyRepository _professorR;
    private readonly IProfessorWriteOnlyRepository _professorW;
    private readonly EncryptPassword _encryptPassword;

    public UpdatePasswordProfessorUseCase(IProfessorReadOnlyRepository professor, IProfessorWriteOnlyRepository professorW, IProfessorLogin login, EncryptPassword encryptPassword)
    {
        _professorR = professor;
        _professorW = professorW;
        _login = login;
        _encryptPassword = encryptPassword;
    }

    public async Task Execute(RequestUpdatePasswordProfessor request)
    {
        var loginProfessor = await _login.IsLogin();

        var professor = await _professorR.FindById(loginProfessor.Id);

        Validar(request, professor);

        professor.Password = _encryptPassword.Encrypt(request.NewPassword);

        _professorW.Udpate(professor);
    }

    private void Validar(RequestUpdatePasswordProfessor request, Models.Entity.Professor prof)
    {
        var validator = new UpdatePasswordProfessorValidator();
        var result = validator.Validate(request);

        var currentPassword = _encryptPassword.Encrypt(request.CurrentPassword);

        if(!prof.Password.Equals(currentPassword))
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure("Senha atual", ResourceErrorMessage.CURRENT_PASSWORD_INVALID));
        }

        if(!result.IsValid) 
        {
            var msgErrors = result.Errors.Select(err => err.ErrorMessage).ToList();
            throw new ValidationErroException(msgErrors);
        }
    }
}
