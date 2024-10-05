using BlogArray.Domain.DTOs;
using FluentValidation;

namespace BlogArray.Application.Features.Settings.Validations;

public class ContentSettingsValidator : AbstractValidator<ContentSettings>
{
    private readonly List<string> pageTypes =
    [
        "posts","page"
    ];

    public ContentSettingsValidator()
    {
        // Validate HomePageContentType - must not be null/empty and must be one of the valid page types
        RuleFor(x => x.HomePageContentType)
            .NotNull().WithMessage("Home page content type is required.")
            .NotEmpty().WithMessage("Home page content type cannot be empty.")
            .Must(type => pageTypes.Contains(type))
            .WithMessage("Select a valid home page content type. Valid options are 'posts' or 'page'.");

        // Conditional validation for StaticHomePageUrl when HomePageContentType is 'page'
        When(x => x.HomePageContentType == "page", () =>
        {
            RuleFor(x => x.StaticHomePageUrl)
                .NotNull().WithMessage("Static home page URL is required when 'page' is selected.")
                .NotEmpty().WithMessage("Static home page URL cannot be empty when 'page' is selected.");
        });
    }
}
