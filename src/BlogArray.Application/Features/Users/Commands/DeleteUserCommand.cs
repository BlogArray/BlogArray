using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using MediatR;

namespace BlogArray.Application.Features.Users.Commands;

public class DeleteUserCommand(int id, bool canAttributeTo, int attributeToUser) : IRequest<ReturnResult<int>>
{
    public int Id { get; set; } = id;

    public bool CanAttributeTo { get; set; } = canAttributeTo;

    public int AttributeToUser { get; set; } = attributeToUser;
}

internal class DeleteUserCommandHandler(IUserRepository userRepository) : IRequestHandler<DeleteUserCommand, ReturnResult<int>>
{
    public async Task<ReturnResult<int>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        return await userRepository.DeleteUserAsync(request.Id, request.CanAttributeTo, request.AttributeToUser);
    }
}
