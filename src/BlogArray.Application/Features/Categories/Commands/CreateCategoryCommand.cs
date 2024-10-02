using BlogArray.Domain.DTOs;
using BlogArray.Domain.Enums;
using BlogArray.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace BlogArray.Application.Features.Categories.Commands;

public class CreateCategoryValidator : AbstractValidator<TermInfoDescription>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(180);
        RuleFor(x => x.Slug).NotNull().NotEmpty().MaximumLength(180);
        RuleFor(x => x.Description).MaximumLength(255);
    }
}

public class CreateCategoryCommand(TermInfoDescription model, TermType termType) : IRequest<ReturnResult<int>>
{
    public TermInfoDescription Model { get; set; } = model;
    public TermType TermType { get; set; } = termType;
}

internal class CreateCategoryCommandHandler(ITermRepository categoryRepository) : IRequestHandler<CreateCategoryCommand, ReturnResult<int>>
{
    public async Task<ReturnResult<int>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        return await categoryRepository.CreateTermAsync(request.Model, request.TermType);
    }
}
