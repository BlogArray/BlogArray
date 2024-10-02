using BlogArray.Domain.DTOs;
using BlogArray.Domain.Enums;
using BlogArray.Domain.Interfaces;
using MediatR;

namespace BlogArray.Application.Features.Terms.Queries;

public class GetTermsQuery(int pageNumber, int pageSize, TermType termType, string? searchTerm) : IRequest<PagedResult<TermInfo>>
{
    public int PageNumber { get; } = pageNumber;
    public int PageSize { get; } = pageSize;
    public TermType TermType { get; set; } = termType;
    public string? SearchTerm { get; } = searchTerm;
}

public class GetTermsQueryHandler(ITermRepository categoryRepository) : IRequestHandler<GetTermsQuery, PagedResult<TermInfo>>
{
    public async Task<PagedResult<TermInfo>> Handle(GetTermsQuery request, CancellationToken cancellationToken)
    {
        return await categoryRepository.GetPaginatedTermsAsync(request.PageNumber, request.PageSize, request.TermType, request.SearchTerm);
    }
}
