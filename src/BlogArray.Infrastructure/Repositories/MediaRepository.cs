using BlogArray.Domain.DTOs;
using BlogArray.Domain.Entities;
using BlogArray.Domain.Enums;
using BlogArray.Domain.Interfaces;
using BlogArray.Infrastructure.Extensions;
using BlogArray.Persistence;
using NetCore.AutoRegisterDi;

namespace BlogArray.Infrastructure.Repositories;

[RegisterAsScoped]
public class MediaRepository(AppDbContext db) : IMediaRepository
{
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

}