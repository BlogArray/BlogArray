using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace BlogArray.Application.Features.Users.Commands;

public class CreateUserValidator : AbstractValidator<CreateUser>
{
    public CreateUserValidator()
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

        // Validate that DisplayName is not null, not empty, has a minimum length of 4, and a maximum length of 64
        RuleFor(x => x.DisplayName)
            .NotNull().WithMessage("Display name is required.")
            .NotEmpty().WithMessage("Display name cannot be empty.")
            .MinimumLength(4).WithMessage("Display name must be at least 4 characters long.")
            .MaximumLength(64).WithMessage("Display name must not exceed 64 characters.");

        // Validate that Bio has a maximum length of 512
        RuleFor(x => x.Bio)
            .MaximumLength(512).WithMessage("Bio must not exceed 512 characters.");

        // Validate that RoleId is greater than 0
        RuleFor(x => x.RoleId)
            .GreaterThan(0).WithMessage("A valid role ID is required.");
    }
}

public class CreateUserCommand(CreateUser model, int loggedInUser) : IRequest<ReturnResult<int>>
{
    public CreateUser Model { get; set; } = model;

    public int LoggedInUserId { get; set; } = loggedInUser;
}

internal class CreateUserCommandHandler(IUserRepository userRepository) : IRequestHandler<CreateUserCommand, ReturnResult<int>>
{
    public async Task<ReturnResult<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        return await userRepository.CreateUserAsync(request.Model, request.LoggedInUserId);
    }
}
