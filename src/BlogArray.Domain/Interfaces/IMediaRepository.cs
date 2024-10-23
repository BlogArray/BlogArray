using BlogArray.Domain.DTOs;
using BlogArray.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace BlogArray.Domain.Interfaces;

public interface IMediaRepository
{
    Task<PagedResult<MediaInfo>> GetPaginatedAsync(int pageNumber, int pageSize, string? searchTerm, AssetType? assetType);

    string[] ValidateFiles(List<IFormFile> files);

    Task<ReturnResult<string[]>> Upload(List<IFormFile> files, int LoggedInUserId);

    Task<ReturnResult<int>> Delete(List<int> files, bool isPermanent);
}
