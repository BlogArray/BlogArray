using BlogArray.Domain.DTOs;

namespace BlogArray.Domain.Interfaces;

public interface ICategoryRepository
{
    Task<CategoryInfo?> GetCategoryAsync(int id);

    Task<PagedResult<CategoryInfoCount>> GetPaginatedCategoryAsync(int pageNumber, int pageSize, string? searchTerm);

    Task<ReturnResult<int>> CreateCategoryAsync(CategoryInfo category);

    Task<ReturnResult<int>> EditCategoryAsync(int id, CategoryInfo category);

}
