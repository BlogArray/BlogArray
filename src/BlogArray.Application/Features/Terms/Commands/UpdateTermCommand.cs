using BlogArray.Domain.DTOs;
using BlogArray.Domain.Enums;
using BlogArray.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace BlogArray.Application.Features.Terms.Commands;

public class UpdateTermValidator : AbstractValidator<TermInfoDescription>
{
    public UpdateTermValidator()
    {
        // Validate that Name is not null, not empty, and has a maximum length of 180 characters
        RuleFor(x => x.Name)
            .NotNull().WithMessage("Term name is required.")
            .NotEmpty().WithMessage("Term name cannot be empty.")
            .MaximumLength(180).WithMessage("Term name must not exceed 180 characters.");

        // Validate that Slug is not null, not empty, and has a maximum length of 180 characters
        RuleFor(x => x.Slug)
            .NotNull().WithMessage("Slug is required.")
            .NotEmpty().WithMessage("Slug cannot be empty.")
            .MaximumLength(180).WithMessage("Slug must not exceed 180 characters.");

        // Validate that Description has a maximum length of 255 characters
        RuleFor(x => x.Description)
            .MaximumLength(255).WithMessage("Description must not exceed 255 characters.");
    }
}

public class UpdateTermCommand(TermInfoDescription model, int idToUpdate, TermType termType) : IRequest<ReturnResult<int>>
{
    public TermInfoDescription Model { get; set; } = model;
    public int IdToUpdate { get; set; } = idToUpdate;
    public TermType TermType { get; set; } = termType;
}

internal class UpdateTermCommandHandler(ITermRepository termRepository) : IRequestHandler<UpdateTermCommand, ReturnResult<int>>
{
    public async Task<ReturnResult<int>> Handle(UpdateTermCommand request, CancellationToken cancellationToken)
    {
        return await termRepository.EditTermAsync(request.IdToUpdate, request.Model, request.TermType);
    }
}
