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
        RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(180);
        RuleFor(x => x.Slug).NotNull().NotEmpty().MaximumLength(180);
        RuleFor(x => x.Description).MaximumLength(255);
    }
}

public class UpdateTermCommand(TermInfoDescription model, int idToUpdate, TermType termType) : IRequest<ReturnResult<int>>
{
    public TermInfoDescription Model { get; set; } = model;
    public int IdToUpdate { get; set; } = idToUpdate;
    public TermType TermType { get; set; } = termType;
}

internal class UpdateTermCommandHandler(ITermRepository categoryRepository) : IRequestHandler<UpdateTermCommand, ReturnResult<int>>
{
    public async Task<ReturnResult<int>> Handle(UpdateTermCommand request, CancellationToken cancellationToken)
    {
        return await categoryRepository.EditTermAsync(request.IdToUpdate, request.Model);
    }
}
