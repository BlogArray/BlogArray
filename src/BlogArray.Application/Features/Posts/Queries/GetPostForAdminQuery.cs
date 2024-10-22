using BlogArray.Domain.Constants;
using BlogArray.Domain.DTOs;
using BlogArray.Domain.Enums;
using BlogArray.Domain.Interfaces;
using MediatR;
using System.Security.Claims;

namespace BlogArray.Application.Features.Posts.Queries;

public class GetPostForAdminQuery(int pageNumber, int pageSize, string? searchTerm, ClaimsPrincipal user, PostStatus postStatus, int loggedInUser) : IRequest<PagedResult<PostListDTO>>
{
    public int PageNumber { get; } = pageNumber;

    public int PageSize { get; } = pageSize;

    public string? SearchTerm { get; } = searchTerm;

    public ClaimsPrincipal User { get; set; } = user;

    public PostStatus PostStatus { get; set; } = postStatus;

    public int LoggedInUserId { get; set; } = loggedInUser;
}

public class GetPostForAdminQueryHandler(IPostRepository postRepository) : IRequestHandler<GetPostForAdminQuery, PagedResult<PostListDTO>>
{
    public async Task<PagedResult<PostListDTO>> Handle(GetPostForAdminQuery request, CancellationToken cancellationToken)
    {
        if (request.User.IsInRole(RoleConstants.Admin) || request.User.IsInRole(RoleConstants.Editor))
        {
            return await postRepository.GetPaginatedPostsAsync(request.PageNumber, request.PageSize, null, request.SearchTerm, request.PostStatus);
        }
        else if (request.User.IsInRole(RoleConstants.Author) || request.User.IsInRole(RoleConstants.Contributor))
        {
            return await postRepository.GetPaginatedPostsAsync(request.PageNumber, request.PageSize, request.LoggedInUserId, request.SearchTerm, request.PostStatus);
        }

        return new PagedResult<PostListDTO>();
    }
}