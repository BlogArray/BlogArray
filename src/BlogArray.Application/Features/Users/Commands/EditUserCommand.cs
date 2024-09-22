using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace BlogArray.Application.Features.Users.Commands;

public class EditUserValidator : AbstractValidator<EditUserInfo>
{
    public EditUserValidator()
    {
        When(u => u.ChangePassword, () =>
        {
            RuleFor(x => x.Password).NotNull().NotEmpty().MinimumLength(8);
        });

        RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress().MaximumLength(256);
        RuleFor(x => x.DisplayName).NotNull().NotEmpty().MinimumLength(8).MaximumLength(64);
        RuleFor(x => x.Bio).MaximumLength(512);
        RuleFor(x => x.RoleId).GreaterThan(0);

        When(u => u.LockoutEnabled, () =>
        {
            RuleFor(x => x.LockoutEnd).NotNull().NotEmpty();
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
