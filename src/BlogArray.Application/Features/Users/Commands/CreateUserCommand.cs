using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace BlogArray.Application.Features.Users.Commands;

public class CreateUserValidator : AbstractValidator<CreateUser>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Username).NotNull().NotEmpty().MinimumLength(4).MaximumLength(64);
        RuleFor(x => x.Password).NotNull().NotEmpty().MinimumLength(8);
        RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress().MaximumLength(256);
        RuleFor(x => x.DisplayName).NotNull().NotEmpty().MinimumLength(8).MaximumLength(64);
        RuleFor(x => x.Bio).MaximumLength(512);
        RuleFor(x => x.RoleId).GreaterThan(0);
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
        return await userRepository.CreateUser(request.Model, request.LoggedInUserId);
    }
}
