﻿using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace BlogArray.Application.Features.Posts.Commands;

public class EditPostValidator : AbstractValidator<EditPostDTO>
{
    public EditPostValidator()
    {
        RuleFor(p => p.Title).NotEmpty().Length(1, 160);
        RuleFor(p => p.Slug).NotEmpty().Length(1, 160);
        RuleFor(p => p.Description).NotEmpty().Length(1, 450);
        RuleFor(p => p.RawContent).NotEmpty();
    }
}

public class EditPostCommand(EditPostDTO model, int loggedInUser) : IRequest<ReturnResult<int>>
{
    public EditPostDTO Model { get; set; } = model;

    public int LoggedInUserId { get; set; } = loggedInUser;
}

internal class EditPostCommandHandler(IPostRepository postRepository) : IRequestHandler<EditPostCommand, ReturnResult<int>>
{
    public async Task<ReturnResult<int>> Handle(EditPostCommand request, CancellationToken cancellationToken)
    {
        return await postRepository.EditPostWithRevisionAsync(request.Model, request.LoggedInUserId);
    }
}
