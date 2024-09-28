using BlogArray.Domain.DTOs;
using FluentValidation;

namespace BlogArray.Application.Features.Settings.Validations;


public class SMTPOptionsValidator : AbstractValidator<SMTPOptions>
{
    public SMTPOptionsValidator()
    {
        RuleFor(x => x.Username).NotNull().NotEmpty().MaximumLength(60);
        RuleFor(x => x.Password).NotNull().NotEmpty().MaximumLength(150);
        RuleFor(x => x.Host).MinimumLength(60).MaximumLength(220);
        RuleFor(x => x.Host).MinimumLength(60).MaximumLength(220);
    }
}
