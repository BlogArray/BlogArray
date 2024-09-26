using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using MediatR;

namespace BlogArray.Application.Features.Settings.Queries;

public class GetOptionsQuery(int pageNumber, int pageSize, bool onlyAutoload) : IRequest<PagedResult<AppOptionsBase>>
{
    public int PageNumber { get; } = pageNumber;
    public int PageSize { get; } = pageSize;
    public bool IncludeOnlyAutoLoad { get; } = onlyAutoload;
}

public class GetOptionsQueryHandler(IAppOptionsRepository appOptionsRepository) : IRequestHandler<GetOptionsQuery, PagedResult<AppOptionsBase>>
{
    public async Task<PagedResult<AppOptionsBase>> Handle(GetOptionsQuery request, CancellationToken cancellationToken)
    {
        return await appOptionsRepository.GetOptions(request.PageNumber, request.PageSize, request.IncludeOnlyAutoLoad);
    }
}
