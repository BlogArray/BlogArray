using BlogArray.Application.Authorization;
using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BlogArray.Application.Features.Posts.Commands;

public class EditPostValidator : AbstractValidator<EditPostDTO>
{
    public EditPostValidator()
    {
        RuleFor(p => p.Title).NotEmpty().Length(1, 160);
        RuleFor(p => p.Slug).NotEmpty().Length(1, 160);
        RuleFor(p => p.Description).NotEmpty().Length(1, 450);
        RuleFor(p => p.Content).NotEmpty();
    }
}

public class EditPostCommand(int postId, EditPostDTO model, int loggedInUser, ClaimsPrincipal user, bool canPublish) : IRequest<ReturnResult<int>>
{
    public int PostId { get; set; } = postId;

    public EditPostDTO Model { get; set; } = model;

    public int LoggedInUserId { get; set; } = loggedInUser;

    public ClaimsPrincipal User { get; set; } = user;

    public bool CanPublish { get; set; } = canPublish;
}

internal class EditPostCommandHandler(IPostRepository postRepository, IAuthorizationService authorizationService) : IRequestHandler<EditPostCommand, ReturnResult<int>>
{
    public async Task<ReturnResult<int>> Handle(EditPostCommand request, CancellationToken cancellationToken)
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

        return await postRepository.EditPostAsync(request.PostId, request.Model, request.LoggedInUserId, request.CanPublish);
    }
}
