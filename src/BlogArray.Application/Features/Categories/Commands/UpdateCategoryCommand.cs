using BlogArray.Domain.DTOs;
using BlogArray.Domain.Enums;
using BlogArray.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace BlogArray.Application.Features.Categories.Commands;

public class UpdateCategoryValidator : AbstractValidator<TermInfoDescription>
{
    public UpdateCategoryValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(180);
        RuleFor(x => x.Slug).NotNull().NotEmpty().MaximumLength(180);
        RuleFor(x => x.Description).MaximumLength(255);
    }
}

public class UpdateCategoryCommand(TermInfoDescription model, int idToUpdate, TermType termType) : IRequest<ReturnResult<int>>
{
    public TermInfoDescription Model { get; set; } = model;
    public int IdToUpdate { get; set; } = idToUpdate;
    public TermType TermType { get; set; } = termType;
}

internal class UpdateCategoryCommandHandler(ITermRepository categoryRepository) : IRequestHandler<UpdateCategoryCommand, ReturnResult<int>>
{
    public async Task<ReturnResult<int>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        return await categoryRepository.EditTermAsync(request.IdToUpdate, request.Model);
    }
}
