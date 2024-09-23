using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using MediatR;

namespace BlogArray.Application.Features.Categories.Queries;

public class GetCategorysQuery(int pageNumber, int pageSize, string? searchTerm) : IRequest<PagedResult<CategoryInfo>>
{
    public int PageNumber { get; } = pageNumber;
    public int PageSize { get; } = pageSize;
    public string? SearchTerm { get; } = searchTerm;
}

public class GetCategorysQueryHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetCategorysQuery, PagedResult<CategoryInfo>>
{
    public async Task<PagedResult<CategoryInfo>> Handle(GetCategorysQuery request, CancellationToken cancellationToken)
    {
        return await categoryRepository.GetPaginatedCategoryAsync(request.PageNumber, request.PageSize, request.SearchTerm);
    }
}
