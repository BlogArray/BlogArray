﻿using BlogArray.Domain.DTOs;
using BlogArray.Domain.Entities;
using BlogArray.Domain.Enums;
using BlogArray.Domain.Interfaces;
using BlogArray.Persistence;
using BlogArray.Shared.Extensions;
using BlogArray.Shared.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NetCore.AutoRegisterDi;

namespace BlogArray.Infrastructure.Repositories;

[RegisterAsScoped]
public class PostRepository(AppDbContext db) : IPostRepository
{
    private readonly int _maxPostRevisions = 5;

    public async Task<Post?> GetPostByIdAsync(int postId)
    {
        return await db.Posts.Include(p => p.PostRevisions)
            .FirstOrDefaultAsync(p => p.Id == postId);
    }

    public async Task<ReturnResult<int>> AddPostWithRevisionAsync(CreatePostDTO post, int loggedInUserId)
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

        db.Posts.Add(newPost);

        await db.SaveChangesAsync();

        var revision = new PostRevision
        {
            Content = post.Content,
            IsLatest = true,
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

    public async Task<ReturnResult<int>> EditPostWithRevisionAsync(int postId, EditPostDTO post, int loggedInUserId)
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
            IsLatest = true,
            PostId = postId,
            CreatedOn = DateTime.UtcNow,
            CreatedUserId = loggedInUserId,
            EditorType = EditorType.Html
        };

        // Set the previous revision's IsLatest to false
        await db.PostRevisions.Where(r => r.PostId == postId && r.IsLatest).ExecuteUpdateAsync(setters => setters.SetProperty(b => b.IsLatest, false)); ;

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
