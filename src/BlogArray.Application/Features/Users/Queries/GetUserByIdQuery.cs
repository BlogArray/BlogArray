using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using MediatR;

namespace BlogArray.Application.Features.Users.Queries;

public class GetUserByIdQuery(int Id) : IRequest<UserInfo?>
{
    public int Id { get; } = Id;
}

public class GetUserByIdQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserByIdQuery, UserInfo?>
{
    public async Task<UserInfo?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        return await userRepository.GetUserAsync(request.Id);
    }
}
