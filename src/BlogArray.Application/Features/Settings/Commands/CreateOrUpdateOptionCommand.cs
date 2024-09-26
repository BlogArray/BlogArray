using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace BlogArray.Application.Features.Settings.Commands;

public class CreateOptionValidator : AbstractValidator<AppOptionsBase>
{
    public CreateOptionValidator()
    {
        RuleFor(x => x.Key).NotNull().NotEmpty().MaximumLength(256);
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
