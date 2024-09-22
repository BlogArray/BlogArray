using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace BlogArray.Application.Features.Users.Commands;

public class UpdateUserProfileValidator : AbstractValidator<UserProfile>
{
    public UpdateUserProfileValidator()
    {
        When(u => u.ChangePassword, () =>
        {
            RuleFor(x => x.Password).NotNull().NotEmpty().MinimumLength(8);
        });

        RuleFor(x => x.Username).NotNull().NotEmpty().MinimumLength(4).MaximumLength(64);
        RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress().MaximumLength(256);
        RuleFor(x => x.DisplayName).NotNull().NotEmpty().MinimumLength(4).MaximumLength(64);
        RuleFor(x => x.Bio).MaximumLength(512);
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
