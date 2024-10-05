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
        RuleFor(x => x.HomePageContentType).NotNull().NotEmpty().Must(type => pageTypes.Contains(type)).WithMessage("Selest a valid home page type.");

        When(u => u.HomePageContentType == "page", () =>
        {
            RuleFor(x => x.StaticHomePageUrl).NotNull().NotEmpty();
        });
    }
}
