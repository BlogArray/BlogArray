using BlogArray.Domain.DTOs;
using FluentValidation;

namespace BlogArray.Application.Features.Settings.Validations;

public class MediaOptionsValidator : AbstractValidator<MediaSettings>
{
    public MediaOptionsValidator()
    {
        // Validate that OptimizedQuality is between 10 and 100 (inclusive)
        RuleFor(x => x.OptimizedQuality)
            .GreaterThanOrEqualTo(10)
            .WithMessage("Optimized quality must be at least 10.")
            .LessThanOrEqualTo(100)
            .WithMessage("Optimized quality must not exceed 100.");
    }
}
