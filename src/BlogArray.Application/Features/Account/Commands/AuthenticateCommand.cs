using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace BlogArray.Application.Features.Account.Commands;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        // Validate that Username is not null, not empty, and is a valid email address
        RuleFor(x => x.Username)
            .NotNull().WithMessage("Username is required.")
            .NotEmpty().WithMessage("Username cannot be empty.")
            .EmailAddress().WithMessage("Please enter a valid email address.");

        // Validate that Password is not null and not empty
        RuleFor(x => x.Password)
            .NotNull().WithMessage("Password is required.")
            .NotEmpty().WithMessage("Password cannot be empty.");
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
        return await accountRepository.AuthenticateAsync(request.Model);
    }
}
