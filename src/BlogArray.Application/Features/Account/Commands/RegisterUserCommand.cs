using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace BlogArray.Application.Features.Account.Commands;

public class RegisterUserValidator : AbstractValidator<RegisterRequest>
{
    public RegisterUserValidator()
    {

        // Validate that Username is not null, not empty, has a minimum length of 4, and a maximum length of 64
        RuleFor(x => x.Username)
            .NotNull().WithMessage("Username is required.")
            .NotEmpty().WithMessage("Username cannot be empty.")
            .MinimumLength(4).WithMessage("Username must be at least 4 characters long.")
            .MaximumLength(64).WithMessage("Username must not exceed 64 characters.");

        // Validate that Email is not null, not empty, is a valid email address, and has a maximum length of 256
        RuleFor(x => x.Email)
            .NotNull().WithMessage("Email is required.")
            .NotEmpty().WithMessage("Email cannot be empty.")
            .EmailAddress().WithMessage("Please enter a valid email address.")
            .MaximumLength(256).WithMessage("Email must not exceed 256 characters.");

        // Validate that Password is not null, not empty, and has a minimum length of 8
        RuleFor(x => x.Password)
            .NotNull().WithMessage("Password is required.")
            .NotEmpty().WithMessage("Password cannot be empty.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
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
