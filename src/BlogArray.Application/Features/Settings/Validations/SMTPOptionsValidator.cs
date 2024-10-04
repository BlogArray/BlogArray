using BlogArray.Domain.DTOs;
using FluentValidation;

namespace BlogArray.Application.Features.Settings.Validations;


public class SMTPOptionsValidator : AbstractValidator<EmailSettings>
{
    public SMTPOptionsValidator()
    {
        RuleFor(x => x.Username).NotNull().NotEmpty();
        RuleFor(x => x.Password).NotNull().NotEmpty();
        RuleFor(x => x.Host).NotNull().NotEmpty();
    }
}
