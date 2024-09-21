using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace BlogArray.Application.Features.Users.Commands;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Username).NotNull().NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotNull().NotEmpty();
    }
}

public class AuthenticateCommand(LoginRequest model) : IRequest<LoginResult>
{
    public LoginRequest Model { get; set; } = model;
}

internal class AuthenticateCommandHandler(IAccountRepository accountRepository) : IRequestHandler<AuthenticateCommand, LoginResult>
{
    public async Task<LoginResult> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
    {
        return await accountRepository.Authenticate(request.Model);
    }
}
