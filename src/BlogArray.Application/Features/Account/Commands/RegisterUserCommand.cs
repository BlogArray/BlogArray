using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace BlogArray.Application.Features.Account.Commands;

public class RegisterUserValidator : AbstractValidator<RegisterRequest>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Password).NotNull().NotEmpty().MinimumLength(8);
    }
}

public class RegisterUserCommand(RegisterRequest model) : IRequest<ReturnResult<int>>
{
    public RegisterRequest Model { get; set; } = model;
}

internal class RegisterUserCommandHandler(IAccountRepository accountRepository) : IRequestHandler<RegisterUserCommand, ReturnResult<int>>
{
    public async Task<ReturnResult<int>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        return await accountRepository.RegisterUserAsync(request.Model);
    }
}
