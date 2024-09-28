using BlogArray.Domain.DTOs;
using FluentValidation;

namespace BlogArray.Application.Features.Settings.Validations;

public class MediaOptionsValidator : AbstractValidator<MediaOptions>
{
    public MediaOptionsValidator()
    {
        RuleFor(x => x.OptimizedQuality).LessThanOrEqualTo(10).GreaterThanOrEqualTo(100);
    }
}
