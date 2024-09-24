using BlogArray.Domain.DTOs;
using BlogArray.Domain.Enums;

namespace BlogArray.Domain.Interfaces;

public interface IMediaRepository
{
    Task<PagedResult<MediaInfo>> GetPaginatedAsync(int pageNumber, int pageSize, string? searchTerm, AssetType? assetType);

}
