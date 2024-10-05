using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace BlogArray.Application.Features.Users.Commands;

public class EditUserValidator : AbstractValidator<EditUserInfo>
{
    public EditUserValidator()
    {
        // Validate Password only when ChangePassword is true
        When(u => u.ChangePassword, () =>
        {
            RuleFor(x => x.Password)
                .NotNull().WithMessage("Password is required when changing the password.")
                .NotEmpty().WithMessage("Password cannot be empty when changing the password.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        });

        // Validate Email
        RuleFor(x => x.Email)
            .NotNull().WithMessage("Email is required.")
            .NotEmpty().WithMessage("Email cannot be empty.")
            .EmailAddress().WithMessage("Please enter a valid email address.")
            .MaximumLength(256).WithMessage("Email must not exceed 256 characters.");

        // Validate DisplayName
        RuleFor(x => x.DisplayName)
            .NotNull().WithMessage("Display name is required.")
            .NotEmpty().WithMessage("Display name cannot be empty.")
            .MinimumLength(8).WithMessage("Display name must be at least 8 characters long.")
            .MaximumLength(64).WithMessage("Display name must not exceed 64 characters.");

        // Validate Bio
        RuleFor(x => x.Bio)
            .MaximumLength(512).WithMessage("Bio must not exceed 512 characters.");

        // Validate RoleId
        RuleFor(x => x.RoleId)
            .GreaterThan(0).WithMessage("A valid role ID is required.");

        // Validate LockoutEnd only when LockoutEnabled is true
        When(u => u.LockoutEnabled, () =>
        {
            RuleFor(x => x.LockoutEnd)
                .NotNull().WithMessage("Lockout end date is required when lockout is enabled.")
                .NotEmpty().WithMessage("Lockout end date cannot be empty when lockout is enabled.");
        });
    }
}

public class EditUserCommand(EditUserInfo model, int userIdToUpdate, int loggedInUser) : IRequest<ReturnResult<int>>
{
    public EditUserInfo Model { get; set; } = model;

    public int LoggedInUserId { get; set; } = loggedInUser;

    public int UserIdToUpdate { get; set; } = userIdToUpdate;
}

internal class EditUserCommandHandler(IUserRepository userRepository) : IRequestHandler<EditUserCommand, ReturnResult<int>>
{
    public async Task<ReturnResult<int>> Handle(EditUserCommand request, CancellationToken cancellationToken)
    {
        return await userRepository.EditUserAsync(request.Model, request.UserIdToUpdate, request.LoggedInUserId);
    }
}
