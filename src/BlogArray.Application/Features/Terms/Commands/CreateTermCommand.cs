using BlogArray.Domain.DTOs;
using BlogArray.Domain.Enums;
using BlogArray.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace BlogArray.Application.Features.Terms.Commands;

public class CreateTermValidator : AbstractValidator<TermInfoDescription>
{
    public CreateTermValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(180);
        RuleFor(x => x.Slug).NotNull().NotEmpty().MaximumLength(180);
        RuleFor(x => x.Description).MaximumLength(255);
    }
}

public class CreateTermCommand(TermInfoDescription model, TermType termType) : IRequest<ReturnResult<int>>
{
    public TermInfoDescription Model { get; set; } = model;
    public TermType TermType { get; set; } = termType;
}

internal class CreateTermCommandHandler(ITermRepository termRepository) : IRequestHandler<CreateTermCommand, ReturnResult<int>>
{
    public async Task<ReturnResult<int>> Handle(CreateTermCommand request, CancellationToken cancellationToken)
    {
        return await termRepository.CreateTermAsync(request.Model, request.TermType);
    }
}
