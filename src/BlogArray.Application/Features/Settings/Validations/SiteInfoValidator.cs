using BlogArray.Domain.DTOs;
using FluentValidation;

namespace BlogArray.Application.Features.Settings.Validations;

public class SiteInfoValidator : AbstractValidator<SiteInfo>
{
    public SiteInfoValidator()
    {
        // Title should not be null or empty and should have a max length of 60 characters
        RuleFor(x => x.Title)
            .NotNull().WithMessage("Site title is required.")
            .NotEmpty().WithMessage("Site title cannot be empty.")
            .MaximumLength(60).WithMessage("Site title must not exceed 60 characters.");

        // Tagline should not be null or empty and should have a max length of 150 characters
        RuleFor(x => x.Tagline)
            .NotNull().WithMessage("Site tagline is required.")
            .NotEmpty().WithMessage("Site tagline cannot be empty.")
            .MaximumLength(150).WithMessage("Site tagline must not exceed 150 characters.");

        // Description should be between 60 and 220 characters
        RuleFor(x => x.Description)
            .MinimumLength(60).WithMessage("Description must be at least 60 characters long.")
            .MaximumLength(220).WithMessage("Description must not exceed 220 characters.");

        // AdminEmail should be a valid email address
        RuleFor(x => x.AdminEmail)
            .EmailAddress().WithMessage("Enter a valid admin email address.");

        // DefaultUserRole should be greater than 0 (indicating a valid role)
        RuleFor(x => x.DefaultUserRole)
            .GreaterThan(0).WithMessage("Select a valid user role.");
    }
}
