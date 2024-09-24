using BlogArray.Domain.DTOs;
using BlogArray.Domain.Enums;
using BlogArray.Domain.Interfaces;
using MediatR;

namespace BlogArray.Application.Features.Media.Queries;

public class GetMediaQuery(int pageNumber, int pageSize, string? searchTerm, AssetType? assetType) : IRequest<PagedResult<MediaInfo>>
{
    public int PageNumber { get; } = pageNumber;
    public int PageSize { get; } = pageSize;
    public string? SearchTerm { get; } = searchTerm;
    public AssetType? AssetType { get; } = assetType;
}

public class GetMediaQueryHandler(IMediaRepository mediaRepository) : IRequestHandler<GetMediaQuery, PagedResult<MediaInfo>>
{
    public async Task<PagedResult<MediaInfo>> Handle(GetMediaQuery request, CancellationToken cancellationToken)
    {
        return await mediaRepository.GetPaginatedAsync(request.PageNumber, request.PageSize, request.SearchTerm, request.AssetType);
    }
}
