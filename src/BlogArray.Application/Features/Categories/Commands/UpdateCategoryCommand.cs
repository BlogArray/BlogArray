using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace BlogArray.Application.Features.Categories.Commands;

public class UpdateCategoryValidator : AbstractValidator<CategoryInfoDescription>
{
    public UpdateCategoryValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(180);
        RuleFor(x => x.Slug).NotNull().NotEmpty().MaximumLength(180);
        RuleFor(x => x.Description).MaximumLength(255);
    }
}

public class UpdateCategoryCommand(CategoryInfoDescription model, int idToUpdate) : IRequest<ReturnResult<int>>
{
    public CategoryInfoDescription Model { get; set; } = model;
    public int IdToUpdate { get; set; } = idToUpdate;
}

internal class UpdateCategoryCommandHandler(ICategoryRepository categoryRepository) : IRequestHandler<UpdateCategoryCommand, ReturnResult<int>>
{
    public async Task<ReturnResult<int>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        return await categoryRepository.EditCategoryAsync(request.IdToUpdate, request.Model);
    }
}
