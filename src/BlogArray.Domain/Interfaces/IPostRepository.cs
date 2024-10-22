using BlogArray.Domain.DTOs;
using BlogArray.Domain.Entities;
using BlogArray.Domain.Enums;

namespace BlogArray.Domain.Interfaces;

public interface IPostRepository
{
    Task<PagedResult<PostListDTO>> GetPaginatedPostsAsync(int pageNumber, int pageSize, int? authorId, string? searchTerm, PostStatus postStatus);

    Task<Post?> GetPostByIdAsync(int postId);

    Task<int?> GetPostAuthorByIdAsync(int postId);

    Task<ReturnResult<int>> AddPostAsync(CreatePostDTO post, int loggedInUserId, bool canPublish);

    Task<ReturnResult<int>> EditPostAsync(int postId, EditPostDTO post, int loggedInUserId, bool canPublish);

    Task AddPostRevisionAsync(int postId, string content, int loggedInUserId);

    Task ChangeStatus(int postId, PostStatus postStatus);

    Task<ReturnResult<int>> DeletePostAsync(int postId);

    Task<EditPostDTO?> GetPostForEditingByIdAsync(int postId);

    /// <summary>
    /// Retrieves a post based on the given slug and post status.
    /// </summary>
    /// <param name="postSlug">The unique slug of the post to be retrieved.</param>
    /// <param name="postStatus">The status of the post (e.g., Published, Draft). Defaults to Published.</param>
    /// <returns>
    /// A <see cref="PostDTO"/> object representing the post if found; otherwise, <c>null</c>.
    /// </returns>
    /// <remarks>
    /// This method fetches the post based on the provided slug and post status. If the post with the specified
    /// slug does not exist or does not match the required status, <c>null</c> will be returned.
    /// </remarks>
    Task<PostDTO?> GetPostBySlugAsync(string postSlug, PostStatus postStatus = PostStatus.Published);
}
