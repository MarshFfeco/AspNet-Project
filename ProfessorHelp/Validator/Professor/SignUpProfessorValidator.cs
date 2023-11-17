using FluentValidation;
using ProfessorHelp.Shared.Comunication.Request;
using ProfessorHelp.Shared.Exception;

namespace ProfessorHelp.Validator.Professor;

public class SignUpProfessorValidator : AbstractValidator<RequestSignUpProfessor>
{
    public SignUpProfessorValidator()
    {
        RuleFor(p => p.First_Name).NotEmpty().WithMessage(ResourceErrorMessage.FIRST_NAME_EMPTY);

        RuleFor(p => p.Last_Name).NotEmpty().WithMessage(ResourceErrorMessage.LAST_NAME_EMPTY);

        RuleFor(p => p.Sex).NotEmpty().WithMessage(ResourceErrorMessage.SEX_EMPTY);
        When(p => !string.IsNullOrEmpty(p.Sex), () =>
        {
            RuleFor(p => p.Sex).Must(x => x.Equals("m") ^ x.Equals("f")).WithMessage(ResourceErrorMessage.SEX_INVALID);
        });

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
