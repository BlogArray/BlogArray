using BlogArray.Domain.DTOs;
using FluentValidation;

namespace BlogArray.Application.Features.Settings.Validations;

public class SiteInfoValidator : AbstractValidator<SiteInfo>
{
    public SiteInfoValidator()
    {
        RuleFor(x => x.Title).NotNull().NotEmpty().MaximumLength(60);
        RuleFor(x => x.Tagline).NotNull().NotEmpty().MaximumLength(150);
        RuleFor(x => x.Description).MinimumLength(60).MaximumLength(220);

    }
}
