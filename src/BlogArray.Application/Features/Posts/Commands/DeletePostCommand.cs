﻿using BlogArray.Application.Authorization;
using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BlogArray.Application.Features.Posts.Commands;

public class DeletePostCommand(int postId, int loggedInUser, ClaimsPrincipal user) : IRequest<ReturnResult<int>>
{
    public int PostId { get; set; } = postId;

    public int LoggedInUserId { get; set; } = loggedInUser;

    public ClaimsPrincipal User { get; set; } = user;
}

internal class DeletePostCommandHandler(IPostRepository postRepository, IAuthorizationService authorizationService) : IRequestHandler<DeletePostCommand, ReturnResult<int>>
{
    public async Task<ReturnResult<int>> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var post = await postRepository.GetPostByIdAsync(request.PostId);

        var authorizationResult = await authorizationService.AuthorizeAsync(request.User, post, new EditPostRequirement());

        if (!authorizationResult.Succeeded)
        {
            return new ReturnResult<int>
            {
                Code = StatusCodes.Status403Forbidden,
                Message = ""
            };
        }

        return await postRepository.DeletePostAsync(request.PostId);
    }
}
