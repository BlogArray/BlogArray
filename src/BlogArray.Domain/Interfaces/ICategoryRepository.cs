using BlogArray.Domain.DTOs;

namespace BlogArray.Domain.Interfaces;

public interface ICategoryRepository
{
    Task<CategoryInfo?> GetCategoryAsync(int id);

    Task<CategoryInfo?> GetCategoryAsync(string slug);

    Task<PagedResult<CategoryInfo>> GetPaginatedCategoryAsync(int pageNumber, int pageSize, string? searchTerm);

    Task<ReturnResult<int>> CreateCategoryAsync(CategoryInfoDescription category);

    Task<ReturnResult<int>> EditCategoryAsync(int id, CategoryInfoDescription category);

}
