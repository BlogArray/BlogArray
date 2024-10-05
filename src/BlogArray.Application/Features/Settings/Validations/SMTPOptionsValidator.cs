using BlogArray.Domain.DTOs;
using FluentValidation;

namespace BlogArray.Application.Features.Settings.Validations;


public class SMTPOptionsValidator : AbstractValidator<EmailSettings>
{
    public SMTPOptionsValidator()
    {
        // Validate that Username is not null or empty
        RuleFor(x => x.Username)
            .NotNull().WithMessage("SMTP username is required.")
            .NotEmpty().WithMessage("SMTP username cannot be empty.");

        // Validate that Password is not null or empty
        RuleFor(x => x.Password)
            .NotNull().WithMessage("SMTP password is required.")
            .NotEmpty().WithMessage("SMTP password cannot be empty.");

        // Validate that Host is not null or empty
        RuleFor(x => x.Host)
            .NotNull().WithMessage("SMTP host is required.")
            .NotEmpty().WithMessage("SMTP host cannot be empty.");
    }
}
