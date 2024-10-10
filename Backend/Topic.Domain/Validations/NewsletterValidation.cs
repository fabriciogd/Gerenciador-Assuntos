using FluentValidation;
using Topic.Domain.Entities;

namespace Topic.Domain.Validations;

internal sealed class NewsletterValidation : AbstractValidator<Newsletter>
{
    public NewsletterValidation()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Necessário informar um titulo")
            .MaximumLength(50).WithMessage("Permitido informar até {MaxLength} caracteres");

        RuleFor(x => x.Keywords)
            .NotEmpty().WithMessage("Necessário informar ao menos uma palavra chave");
    }
}
