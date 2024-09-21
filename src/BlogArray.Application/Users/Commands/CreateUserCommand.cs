using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using MediatR;

namespace BlogArray.Application.Users.Commands;


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
