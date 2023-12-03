using FluentValidation;
using ProfessorHelp.Shared.Comunication.Request.Professor;
using ProfessorHelp.Shared.Exception;

namespace ProfessorHelp.UseCase.Professor.validation;

public class UpdatePasswordProfessorValidator : AbstractValidator<RequestUpdatePasswordProfessor>
{
    public UpdatePasswordProfessorValidator()
    {
        RuleFor(p => p.NewPassword).NotEmpty().WithMessage(ResourceErrorMessage.PASSWORD_EMPTY);
        When(p => !string.IsNullOrWhiteSpace(p.NewPassword), () =>
        {
            RuleFor(p => p.NewPassword.Length).GreaterThanOrEqualTo(8).WithMessage(ResourceErrorMessage.PASSWORD_EMPTY);
        });

        RuleFor(p => p.CurrentPassword).NotEmpty().WithMessage(ResourceErrorMessage.PASSWORD_EMPTY);
        When(p => !string.IsNullOrWhiteSpace(p.CurrentPassword), () =>
        {
            RuleFor(p => p.CurrentPassword.Length).GreaterThanOrEqualTo(8).WithMessage(ResourceErrorMessage.PASSWORD_EMPTY);
        });
    }
}
