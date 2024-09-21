using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace BlogArray.Application.Features.Users.Commands;

public class CreateUserValidator : AbstractValidator<CreateUser>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.UserName).NotNull().NotEmpty();
        RuleFor(x => x.Password).NotNull().NotEmpty();
    }
}

public class CreateUserCommand(CreateUser model, int loggedInUser) : IRequest<ReturnResult<int>>
{
    public CreateUser Model { get; set; } = model;

    public int LoggedInUserId { get; set; } = loggedInUser;
}

internal class CreateUserCommandHandler(IAccountRepository accountRepository) : IRequestHandler<CreateUserCommand, ReturnResult<int>>
{
    public async Task<ReturnResult<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        return await accountRepository.CreateUser(request.Model, request.LoggedInUserId);
    }
}
