using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using MediatR;

namespace BlogArray.Application.Features.Posts.Queries;

public class GetPostForPublicQuery(string postSlug, bool isAuthenticated) : IRequest<PostDTO?>
{
    public string PostSlug { get; } = postSlug;

    public bool IsAuthenticated { get; } = isAuthenticated;

}

public class GetPostForPublicQueryHandler(IPostRepository postRepository) : IRequestHandler<GetPostForPublicQuery, PostDTO?>
{
    public async Task<PostDTO?> Handle(GetPostForPublicQuery request, CancellationToken cancellationToken)
    {
        return await postRepository.GetPostBySlugAsync(request.PostSlug);
    }
}