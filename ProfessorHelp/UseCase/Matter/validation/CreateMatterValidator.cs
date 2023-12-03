using FluentValidation;
using ProfessorHelp.Shared.Comunication.Request.Matter;
using ProfessorHelp.Shared.Exception;

namespace ProfessorHelp.UseCase.Matter.validation;

public class CreateMatterValidato : AbstractValidator<RequestMatterCreate>
{
    public CreateMatterValidato()
    {
        RuleFor(m => m.Title).NotEmpty();

        When(m => !string.IsNullOrWhiteSpace(m.Classroom), () =>
        {
            RuleFor(m => m.Classroom!.Length).GreaterThanOrEqualTo(15);
        });

        When(m => !string.IsNullOrWhiteSpace(m.Code), () =>
        {
            RuleFor(m => m.Code!.Length).GreaterThan(7);
        });
    }
}
