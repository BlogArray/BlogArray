using BlogArray.Domain.DTOs;
using BlogArray.Domain.Entities;
using BlogArray.Domain.Enums;
using BlogArray.Domain.Interfaces;
using BlogArray.Infrastructure.Extensions;
using BlogArray.Persistence;
using BlogArray.Shared.Extensions;
using BlogArray.Shared.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NetCore.AutoRegisterDi;

namespace BlogArray.Infrastructure.Repositories;

[RegisterAsScoped]
public class PostRepository(AppDbContext db, ITermRepository termRepository) : IPostRepository
{
    //TODO: max post revision from settings
    private readonly int _maxPostRevisions = 5;

    public async Task<PagedResult<PostListDTO>> GetPaginatedPostsAsync(int pageNumber, int pageSize, int? authorId, string? searchTerm, PostStatus postStatus)
    {
        IQueryable<Post> query = db.Posts.Where(t => t.PostStatus == postStatus);

        if (authorId.HasValue && authorId > 0)
        {
            query = query.Where(u => u.CreatedUserId == authorId);
        }

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query
                .Where(u => u.Slug.Contains(searchTerm) ||
                u.Description.Contains(searchTerm) ||
                u.Title.Contains(searchTerm));
        }

        PagedResult<PostListDTO> pagedResult = await query
            .OrderByDescending(p => p.CreatedOn) // Order by Id
            .Select(p => new PostListDTO
            {
                Id = p.Id,
                Title = p.Title,
                Slug = p.Slug,
                Description = p.Description,
                Cover = p.Cover,
                AuthorInfo = new BasicAuthorInfoDTO
                {
                    Id = p.CreatedUser.Id,
                    Username = p.CreatedUser.Username,
                    DisplayName = p.CreatedUser.DisplayName,
                    ProfilePicture = p.CreatedUser.ProfilePicture
                },
                Categories = p.Terms
                .Where(t => t.Term.TermType == TermType.Category)
                .Select(s => new BasicTermInfo
                {
                    Id = s.Term.Id,
                    Slug = s.Term.Slug,
                    Name = s.Term.Name
                }).ToList(),
                Tags = p.Terms
                .Where(t => t.Term.TermType == TermType.Tag)
                .Select(s => new BasicTermInfo
                {
                    Id = s.Term.Id,
                    Slug = s.Term.Slug,
                    Name = s.Term.Name
                }).ToList()
            }).ToPagedListAsync(pageNumber, pageSize);

        return pagedResult;
    }

    public async Task<Post?> GetPostByIdAsync(int postId)
    {
        return await db.Posts.FirstOrDefaultAsync(p => p.Id == postId);
    }

    public async Task<int?> GetPostAuthorByIdAsync(int postId)
    {
        return (await db.Posts.FirstOrDefaultAsync(p => p.Id == postId))?.CreatedUserId;
    }

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
    public async Task<PostDTO?> GetPostBySlugAsync(string postSlug, PostStatus postStatus = PostStatus.Published)
    {
        return await db.Posts.Where(p => p.Slug == postSlug && p.PostStatus == postStatus).Select(p => new PostDTO
        {
            Id = p.Id,
            Slug = p.Slug,
            Title = p.Title,
            Description = p.Description,
            Cover = p.Cover,
            Content = p.Content,
            PostType = p.PostType,
            DisplayAuthorInfo = p.DisplayAuthorInfo,
            DisplayCoverImage = p.DisplayCoverImage,
            DisplayPostTitle = p.DisplayPostTitle,
            EnableComments = p.EnableComments,
            EnableContactForm = p.EnableContactForm,
            EnableSocialSharing = p.EnableSocialSharing,
            EnableTableOfContents = p.EnableTableOfContents,
            IsFeatured = p.IsFeatured,
            IsFullWidth = p.IsFullWidth,
            AuthorInfo = new BasicAuthorInfoDTO
            {
                Id = p.CreatedUser.Id,
                Username = p.CreatedUser.Username,
                DisplayName = p.CreatedUser.DisplayName,
                ProfilePicture = p.CreatedUser.ProfilePicture
            },
            Categories = p.Terms
            .Where(t => t.Term.TermType == TermType.Category)
            .Select(s => new BasicTermInfo
            {
                Id = s.Term.Id,
                Slug = s.Term.Slug,
                Name = s.Term.Name
            }).ToList(),
            Tags = p.Terms
            .Where(t => t.Term.TermType == TermType.Tag)
            .Select(s => new BasicTermInfo
            {
                Id = s.Term.Id,
                Slug = s.Term.Slug,
                Name = s.Term.Name
            }).ToList()
        }).AsSplitQuery().FirstOrDefaultAsync();
    }

    public async Task<ReturnResult<int>> AddPostAsync(CreatePostDTO post, int loggedInUserId, bool canPublish)
    {
        var newPost = new Post
        {
            Title = post.Title,
            Slug = await GetPostSlugFromTitle(post.Title),
            Cover = post.Cover,
            Description = post.Description,
            Content = post.Content,
            PostType = post.PostType,
            PostStatus = post.PostStatus,
            IsFeatured = post.IsFeatured,
            IsFullWidth = post.IsFullWidth,
            EnableContactForm = post.EnableContactForm,
            DisplayPostTitle = post.DisplayPostTitle,
            DisplayAuthorInfo = post.DisplayAuthorInfo,
            EnableSocialSharing = post.EnableSocialSharing,
            EnableComments = post.EnableComments,
            DisplayCoverImage = post.DisplayCoverImage,
            EnableTableOfContents = post.EnableTableOfContents,
            ReadingTimeEstimate = ReadTimeCalculator.CalculateReadingTime(post.Content),
            PublishedOn = post.PostStatus == PostStatus.Published ? DateTime.UtcNow : null,
            CreatedUserId = loggedInUserId,
            CreatedOn = DateTime.UtcNow,
        };

        if (!canPublish)
        {
            newPost.PostStatus = PostStatus.Draft;
        }

        if (post.Categories?.Count == 0) post.Categories = [];

        if (post.Tags?.Count == 0) post.Tags = [];

        var allTerms = post.Categories?.Concat(post.Tags).ToList();

        if (allTerms?.Count > 0)
        {
            var terms = await termRepository.GetTermsByIdsAsync(allTerms);

            newPost.Terms = terms.Select(t => new PostTerm { TermId = t.Id }).ToList();
        }

        db.Posts.Add(newPost);

        await db.SaveChangesAsync();

        var revision = new PostRevision
        {
            Content = post.Content,
            CreatedOn = DateTime.UtcNow,
            CreatedUserId = loggedInUserId,
            EditorType = EditorType.Html,
            PostId = newPost.Id
        };

        db.PostRevisions.Add(revision);

        await db.SaveChangesAsync();

        return new ReturnResult<int>
        {
            Code = StatusCodes.Status200OK,
            Title = "Post.Created",
            Message = $"Post '{post.Title}' was successfully created.",
            Result = newPost.Id
        };
    }

    public async Task<ReturnResult<int>> EditPostAsync(int postId, EditPostDTO post, int loggedInUserId, bool canPublish)
    {
        if (await db.Posts.AnyAsync(a => a.Slug == post.Slug && a.Id != postId))
        {
            return new ReturnResult<int>
            {
                Code = StatusCodes.Status400BadRequest,
                Title = "Post.SlugExists",
                Message = $"The slug '{post.Slug}' is already exists with another post."
            };
        }

        Post? existingPost = await db.Posts.FirstOrDefaultAsync(a => a.Id == postId);

        if (existingPost == null)
        {
            return new ReturnResult<int>
            {
                Code = StatusCodes.Status404NotFound,
                Title = "Post.NotFound",
                Message = $"The post with title '{post.Title}' could not be found in the system."
            };
        }

        existingPost.Title = post.Title;
        existingPost.Slug = post.Slug;
        existingPost.Cover = post.Cover;
        existingPost.Description = post.Description;
        existingPost.Content = post.Content;
        existingPost.IsFeatured = post.IsFeatured;
        existingPost.IsFullWidth = post.IsFullWidth;
        existingPost.EnableContactForm = post.EnableContactForm;
        existingPost.DisplayPostTitle = post.DisplayPostTitle;
        existingPost.DisplayAuthorInfo = post.DisplayAuthorInfo;
        existingPost.EnableSocialSharing = post.EnableSocialSharing;
        existingPost.EnableComments = post.EnableComments;
        existingPost.DisplayCoverImage = post.DisplayCoverImage;
        existingPost.EnableTableOfContents = post.EnableTableOfContents;
        existingPost.ReadingTimeEstimate = ReadTimeCalculator.CalculateReadingTime(post.Content);
        existingPost.UpdatedOn = DateTime.UtcNow;
        existingPost.UpdatedUserId = loggedInUserId;

        if (!canPublish)
        {
            existingPost.PostStatus = PostStatus.Draft;
        }

        if (post.Categories?.Count == 0) post.Categories = [];

        if (post.Tags?.Count == 0) post.Tags = [];

        var allTerms = post.Categories?.Concat(post.Tags).ToList();

        if (allTerms?.Count > 0)
        {
            await db.PostTerms.Where(t => t.PostId == existingPost.Id).ExecuteDeleteAsync();

            var terms = await termRepository.GetTermsByIdsAsync(allTerms);

            existingPost.Terms = terms.Select(t => new PostTerm { TermId = t.Id }).ToList();
        }

        await AddPostRevisionAsync(existingPost.Id, post.Content, loggedInUserId);

        await db.SaveChangesAsync();

        return new ReturnResult<int>
        {
            Code = StatusCodes.Status200OK,
            Title = "Post.Updated",
            Message = $"Post '{post.Title}' was successfully updated.",
            Result = post.Id
        };
    }

    public async Task AddPostRevisionAsync(int postId, string content, int loggedInUserId)
    {
        var revision = new PostRevision
        {
            Content = content,
            PostId = postId,
            CreatedOn = DateTime.UtcNow,
            CreatedUserId = loggedInUserId,
            EditorType = EditorType.Html
        };

        // Set the previous revision's IsLatest to false
        //await db.PostRevisions.Where(r => r.PostId == postId && r.IsLatest).ExecuteUpdateAsync(setters => setters.SetProperty(b => b.IsLatest, false)); ;

        db.PostRevisions.Add(revision);

        // Limit revisions if needed
        if (_maxPostRevisions > 0)
        {
            await db.PostRevisions
                            .Where(r => r.PostId == postId)
                            .OrderByDescending(r => r.CreatedOn)
                            .Skip(_maxPostRevisions).ExecuteDeleteAsync();

        }

        await db.SaveChangesAsync();
    }

    public async Task ChangeStatus(int postId, PostStatus postStatus)
    {
        await db.Posts.Where(p => p.Id == postId).ExecuteUpdateAsync(setters => setters.SetProperty(b => b.PostStatus, postStatus));
    }

    public async Task<ReturnResult<int>> DeletePostAsync(int postId)
    {
        Post? post = await db.Posts.FirstOrDefaultAsync(a => a.Id == postId);

        if (post == null)
        {
            return new ReturnResult<int>
            {
                Code = StatusCodes.Status404NotFound,
                Title = "Post.NotFound",
                Message = $"The post with the id '{postId}' could not be found in the system."
            };
        }

        db.Posts.Remove(post);

        await db.Statistics.Where(p => p.PostId == postId).ExecuteUpdateAsync(setter => setter.SetProperty(b => b.PostId, (int?)null));

        await db.SaveChangesAsync();

        return new ReturnResult<int>
        {
            Code = StatusCodes.Status200OK,
            Title = $"Post.Deleted",
            Message = $"Post '{post.Title}' was successfully deleted.",
            Result = postId
        };
    }

    public async Task<EditPostDTO?> GetPostForEditingByIdAsync(int postId)
    {
        return await db.Posts.Select(p => new EditPostDTO
        {
            Id = p.Id,
            Slug = p.Slug,
            Title = p.Title,
            Description = p.Description,
            Cover = p.Cover,
            Content = p.Content,
            PostStatus = p.PostStatus,
            PostType = p.PostType,
            DisplayAuthorInfo = p.DisplayAuthorInfo,
            DisplayCoverImage = p.DisplayCoverImage,
            DisplayPostTitle = p.DisplayPostTitle,
            EnableComments = p.EnableComments,
            EnableContactForm = p.EnableContactForm,
            EnableSocialSharing = p.EnableSocialSharing,
            EnableTableOfContents = p.EnableTableOfContents,
            IsFeatured = p.IsFeatured,
            IsFullWidth = p.IsFullWidth,
            Categories = p.Terms.Where(t => t.Term.TermType == TermType.Category).Select(s => s.TermId).ToList(),
            Tags = p.Terms.Where(t => t.Term.TermType == TermType.Tag).Select(s => s.TermId).ToList(),
        }).AsSplitQuery().FirstOrDefaultAsync(p => p.Id == postId);
    }

    private async Task<string> GetPostSlugFromTitle(string title)
    {
        string baseSlug = title.ToSlug();
        string slug = baseSlug;

        for (int i = 1; i < 100; i++)
        {
            if (!await db.Posts.AnyAsync(p => p.Slug == slug))
            {
                return slug;
            }
            slug = $"{baseSlug}-{i}";
        }

        throw new InvalidOperationException("Failed to generate a unique slug after 99 attempts.");
    }
}
