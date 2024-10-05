using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace BlogArray.Application.Features.Users.Commands;

public class UpdateUserProfileValidator : AbstractValidator<UserProfile>
{
    public UpdateUserProfileValidator()
    {
        // Validate Password only when ChangePassword is true
        When(u => u.ChangePassword, () =>
        {
            RuleFor(x => x.Password)
                .NotNull().WithMessage("Password is required when changing the password.")
                .NotEmpty().WithMessage("Password cannot be empty when changing the password.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        });

        // Validate Username
        RuleFor(x => x.Username)
            .NotNull().WithMessage("Username is required.")
            .NotEmpty().WithMessage("Username cannot be empty.")
            .MinimumLength(4).WithMessage("Username must be at least 4 characters long.")
            .MaximumLength(64).WithMessage("Username must not exceed 64 characters.");

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
            .MinimumLength(4).WithMessage("Display name must be at least 4 characters long.")
            .MaximumLength(64).WithMessage("Display name must not exceed 64 characters.");

        // Validate Bio
        RuleFor(x => x.Bio)
            .MaximumLength(512).WithMessage("Bio must not exceed 512 characters.");
    }
}

public class UpdateUserProfileCommand(UserProfile model, int loggedInUser) : IRequest<ReturnResult<int>>
{
    public UserProfile Model { get; set; } = model;

    public int LoggedInUserId { get; set; } = loggedInUser;
}

internal class UpdateUserProfileCommandHandler(IUserRepository userRepository) : IRequestHandler<UpdateUserProfileCommand, ReturnResult<int>>
{
    public async Task<ReturnResult<int>> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        return await userRepository.UpdateProfileAsync(request.Model, request.LoggedInUserId);
    }
}
