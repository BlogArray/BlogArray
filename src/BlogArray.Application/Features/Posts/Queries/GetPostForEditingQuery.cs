using BlogArray.Application.Authorization;
using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BlogArray.Application.Features.Posts.Queries;

public class GetPostForEditingQuery(int postId, ClaimsPrincipal user, int loggedInUser) : IRequest<EditPostDTO?>
{
    public int PostId { get; } = postId;

    public ClaimsPrincipal User { get; set; } = user;

    public int LoggedInUserId { get; set; } = loggedInUser;
}

public class GetPostForEditingQueryHandler(IPostRepository postRepository, IAuthorizationService authorizationService) : IRequestHandler<GetPostForEditingQuery, EditPostDTO?>
{
    public async Task<EditPostDTO?> Handle(GetPostForEditingQuery request, CancellationToken cancellationToken)
    {
        int? createdUserId = await postRepository.GetPostAuthorByIdAsync(request.PostId);

        if (createdUserId == null)
        {
            return default;
        }

        AuthorizationResult authorizationResult = await authorizationService.AuthorizeAsync(request.User, createdUserId.Value, new EditPostRequirement());

        return !authorizationResult.Succeeded ? default : await postRepository.GetPostForEditingByIdAsync(request.PostId);
    }
}