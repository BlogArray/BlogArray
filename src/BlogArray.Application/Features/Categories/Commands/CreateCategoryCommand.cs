using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace BlogArray.Application.Features.Categories.Commands;

public class CreateCategoryValidator : AbstractValidator<CategoryInfoDescription>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(180);
        RuleFor(x => x.Slug).NotNull().NotEmpty().MaximumLength(180);
        RuleFor(x => x.Description).MaximumLength(255);
    }
}

public class CreateCategoryCommand(CategoryInfoDescription model) : IRequest<ReturnResult<int>>
{
    public CategoryInfoDescription Model { get; set; } = model;
}

internal class CreateCategoryCommandHandler(ICategoryRepository categoryRepository) : IRequestHandler<CreateCategoryCommand, ReturnResult<int>>
{
    public async Task<ReturnResult<int>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        return await categoryRepository.CreateCategoryAsync(request.Model);
    }
}
