using BlogArray.Domain.DTOs;
using BlogArray.Domain.Enums;
using BlogArray.Domain.Interfaces;
using MediatR;

namespace BlogArray.Application.Features.Categories.Queries;

public class GetCategorysQuery(int pageNumber, int pageSize, TermType termType, string? searchTerm) : IRequest<PagedResult<TermInfo>>
{
    public int PageNumber { get; } = pageNumber;
    public int PageSize { get; } = pageSize;
    public TermType TermType { get; set; } = termType;
    public string? SearchTerm { get; } = searchTerm;
}

public class GetCategorysQueryHandler(ITermRepository categoryRepository) : IRequestHandler<GetCategorysQuery, PagedResult<TermInfo>>
{
    public async Task<PagedResult<TermInfo>> Handle(GetCategorysQuery request, CancellationToken cancellationToken)
    {
        return await categoryRepository.GetPaginatedTermsAsync(request.PageNumber, request.PageSize, request.TermType, request.SearchTerm);
    }
}
