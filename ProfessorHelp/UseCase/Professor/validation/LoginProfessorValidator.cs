using FluentValidation;
using ProfessorHelp.Shared.Comunication.Request.Professor;
using ProfessorHelp.Shared.Exception;

namespace ProfessorHelp.UseCase.Professor.validation;

public class LoginProfessorValidator : AbstractValidator<RequestLoginProfessor>
{
    public LoginProfessorValidator()
    {
        RuleFor(p => p.Email).NotEmpty().WithMessage(ResourceErrorMessage.EMAIL_EMPTY);
        When(p => !string.IsNullOrWhiteSpace(p.Email), () =>
        {
            RuleFor(p => p.Email).EmailAddress().WithMessage(ResourceErrorMessage.EMAIL_INVALID);
        });

        RuleFor(p => p.Password).NotEmpty().WithMessage(ResourceErrorMessage.PASSWORD_EMPTY);
        When(p => !string.IsNullOrWhiteSpace(p.Password), () =>
        {
            RuleFor(p => p.Password.Length).GreaterThanOrEqualTo(8).WithMessage(ResourceErrorMessage.PASSWORD_EMPTY);
        });
    }
}
