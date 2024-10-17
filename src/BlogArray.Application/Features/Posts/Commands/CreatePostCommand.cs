using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace BlogArray.Application.Features.Posts.Commands;

public class CreatePostValidator : AbstractValidator<CreatePostDTO>
{
    public CreatePostValidator()
    {
        RuleFor(p => p.Title).NotEmpty().Length(1, 160);
        RuleFor(p => p.Description).NotEmpty().Length(1, 450);
        RuleFor(p => p.Content).NotEmpty();
    }
}

public class CreatePostCommand(CreatePostDTO model, int loggedInUser, bool canPublish) : IRequest<ReturnResult<int>>
{
    public CreatePostDTO Model { get; set; } = model;

    public int LoggedInUserId { get; set; } = loggedInUser;

    public bool CanPublish { get; set; } = canPublish;
}

internal class CreatePostCommandHandler(IPostRepository postRepository) : IRequestHandler<CreatePostCommand, ReturnResult<int>>
{
    public async Task<ReturnResult<int>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        return await postRepository.AddPostAsync(request.Model, request.LoggedInUserId, request.CanPublish);
    }
}
