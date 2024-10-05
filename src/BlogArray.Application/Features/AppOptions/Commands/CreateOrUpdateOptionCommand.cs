using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace BlogArray.Application.Features.AppOptions.Commands;

public class CreateOptionValidator : AbstractValidator<AppOptionsBase>
{
    public CreateOptionValidator()
    {
        // Validate that Key is not null, not empty, and has a maximum length of 256 characters
        RuleFor(x => x.Key)
            .NotNull().WithMessage("Key is required.")
            .NotEmpty().WithMessage("Key cannot be empty.")
            .MaximumLength(256).WithMessage("Key must not exceed 256 characters.");
    }
}

public class CreateOrUpdateOptionCommand(AppOptionsBase model) : IRequest<ReturnResult<int>>
{
    public AppOptionsBase Model { get; set; } = model;
}

internal class CreateOptionCommandHandler(IAppOptionsRepository appOptionsRepository) : IRequestHandler<CreateOrUpdateOptionCommand, ReturnResult<int>>
{
    public async Task<ReturnResult<int>> Handle(CreateOrUpdateOptionCommand request, CancellationToken cancellationToken)
    {
        return await appOptionsRepository.CreateOrUpdateAsync(request.Model);
    }
}
