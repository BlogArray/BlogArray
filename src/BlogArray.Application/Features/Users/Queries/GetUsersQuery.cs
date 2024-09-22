using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using MediatR;

namespace BlogArray.Application.Features.Users.Queries;

public class GetUsersQuery(int pageNumber, int pageSize, string? searchTerm) : IRequest<PagedResult<BasicUserInfoRole>>
{
    public int PageNumber { get; } = pageNumber;
    public int PageSize { get; } = pageSize;
    public string? SearchTerm { get; } = searchTerm;
}

public class GetUsersQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUsersQuery, PagedResult<BasicUserInfoRole>>
{
    public async Task<PagedResult<BasicUserInfoRole>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await userRepository.GetPaginatedUsersAsync(request.PageNumber, request.PageSize, request.SearchTerm);
    }
}
