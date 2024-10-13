using BlogArray.Domain.DTOs;
using BlogArray.Domain.Enums;

namespace BlogArray.Domain.Interfaces;

public interface IPostRepository
{
    Task<int> AddPostWithRevisionAsync(CreatePostDTO post, int loggedInUserId);

    Task AddPostRevisionAsync(int postId, string rawContent, int loggedInUserId);

    Task ChangeStatus(int postId, PostStatus postStatus);
}
