using BlogArray.Domain.DTOs;
using BlogArray.Domain.Enums;

namespace BlogArray.Domain.Interfaces;

public interface ITermRepository
{
    Task<PagedResult<TermInfo>> GetPaginatedTermsAsync(int pageNumber, int pageSize, TermType termType, string? searchTerm);

    Task<TermInfo?> GetTermAsync(int id, TermType termType);

    Task<TermInfo?> GetTermAsync(string slug, TermType termType);

    Task<ReturnResult<int>> CreateTermAsync(TermInfoDescription term, TermType termType);

    Task<ReturnResult<int>> EditTermAsync(int id, TermInfoDescription term, TermType termType);

    Task<ReturnResult<int>> DeleteTermAsync(int id, TermType termType);

}
