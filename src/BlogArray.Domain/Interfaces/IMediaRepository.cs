using BlogArray.Domain.DTOs;

namespace BlogArray.Domain.Interfaces;

public interface IMediaRepository
{
    Task<PagedResult<MediaInfo>> GetPaginatedAsync(int pageNumber, int pageSize, string? searchTerm);

}
