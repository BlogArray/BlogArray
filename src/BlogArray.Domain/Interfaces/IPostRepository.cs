using BlogArray.Domain.DTOs;
using BlogArray.Domain.Enums;

namespace BlogArray.Domain.Interfaces;

public interface IPostRepository
{
    Task<ReturnResult<int>> AddPostAsync(CreatePostDTO post, int loggedInUserId);

    Task<ReturnResult<int>> EditPostAsync(int postId, EditPostDTO post, int loggedInUserId);

    Task AddPostRevisionAsync(int postId, string content, int loggedInUserId);

    Task ChangeStatus(int postId, PostStatus postStatus);
}
