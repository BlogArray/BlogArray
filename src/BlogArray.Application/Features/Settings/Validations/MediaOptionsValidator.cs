using BlogArray.Domain.DTOs;
using FluentValidation;

namespace BlogArray.Application.Features.Settings.Validations;

public class MediaOptionsValidator : AbstractValidator<MediaOptions>
{
    public MediaOptionsValidator()
    {
        RuleFor(x => x.OptimizedQuality).GreaterThanOrEqualTo(10).LessThanOrEqualTo(100);
    }
}
