using BlogArray.Domain.DTOs;
using BlogArray.Domain.Entities;
using BlogArray.Domain.Enums;

namespace BlogArray.Domain.Interfaces;

public interface IPostRepository
{
    Task<Post?> GetPostByIdAsync(int postId);
    
    Task<int?> GetPostAuthorByIdAsync(int postId);

    Task<ReturnResult<int>> AddPostAsync(CreatePostDTO post, int loggedInUserId, bool canPublish);

    Task<ReturnResult<int>> EditPostAsync(int postId, EditPostDTO post, int loggedInUserId, bool canPublish);

    Task AddPostRevisionAsync(int postId, string content, int loggedInUserId);

    Task ChangeStatus(int postId, PostStatus postStatus);

    Task<ReturnResult<int>> DeletePostAsync(int postId);
}
