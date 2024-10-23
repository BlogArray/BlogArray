using BlogArray.Domain.Constants;
using BlogArray.Domain.DTOs;
using BlogArray.Domain.Entities;
using BlogArray.Domain.Enums;
using BlogArray.Domain.Interfaces;
using BlogArray.Infrastructure.Extensions;
using BlogArray.Persistence;
using Microsoft.AspNetCore.Http;
using NetCore.AutoRegisterDi;

namespace BlogArray.Infrastructure.Repositories;

[RegisterAsScoped]
public class MediaRepository(AppDbContext db) : IMediaRepository
{
    private const int FiveMegaBytes = 5 * 1024 * 1024;
    private const string AcceptedFileSizeMessage = "Accepted file size was 5mb.";

    public async Task<PagedResult<MediaInfo>> GetPaginatedAsync(int pageNumber, int pageSize, string? searchTerm, AssetType? assetType)
    {
        IQueryable<Storage> query = db.Storages;

        if (assetType.HasValue)
        {
            query = query.Where(u => u.AssetType == assetType);
        }

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(u => u.Slug.Contains(searchTerm) ||
                                     u.Name.Contains(searchTerm));
        }

        PagedResult<MediaInfo> pagedResult = await query
            .OrderByDescending(u => u.CreatedOn)
            .Select(u => new MediaInfo
            {
                Id = u.Id,
                Name = u.Name,
                Slug = u.Slug,
                AssetType = u.AssetType,
                ContentType = u.ContentType,
                DeletedAt = u.DeletedAt,
                IsDeleted = u.IsDeleted,
                Length = u.Length,
                Path = u.Path,
                Description = u.Description
            })
            .ToPagedListAsync(pageNumber, pageSize);

        return pagedResult;
    }

    public string[] ValidateFiles(List<IFormFile> files)
    {
        List<string>? invalidFiles = files.Where(file => InvalidFileType(file.FileName)).Select(file => $"Invalid file type: {file.FileName}.").ToList();

        List<string>? invalidSizedFiles = files.Where(file => InvalidFileType(file.FileName)).Select(file => $"File {file.FileName} was too large. Accepted file size was 5mb.").ToList();

        if (invalidFiles?.Count == 0) invalidFiles = [];

        if (invalidSizedFiles?.Count == 0) invalidSizedFiles = [];

        return [.. invalidFiles, .. invalidSizedFiles];
    }

    private static bool InvalidFileType(string fileName)
    {
        return !AppConstants.FileExtensions.Any(ext => fileName.EndsWith(ext, StringComparison.OrdinalIgnoreCase));
    }
}
