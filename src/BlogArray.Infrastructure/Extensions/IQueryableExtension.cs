using BlogArray.Domain.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BlogArray.Infrastructure.Extensions;

/// <summary>
/// Provides extension methods for IQueryable to enable pagination.
/// </summary>
public static class QueryableExtensions
{
    /// <summary>
    /// Paginates the queryable collection of items and returns a <see cref="PagedResult{T}"/> object.
    /// </summary>
    /// <typeparam name="T">The type of the items in the queryable list.</typeparam>
    /// <param name="query">The queryable collection of items.</param>
    /// <param name="pageNumber">The index of the page to retrieve (1-based).</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>A <see cref="PagedResult{T}"/> object containing the paginated items.</returns>
    public static async Task<PagedResult<T>> ToPagedListAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize)
    {
        // Ensure pageNumber is at least 1
        pageNumber = Math.Max(1, pageNumber);

        // Total number of items
        int totalItemCount = query.Count();

        // Calculate total number of pages
        int totalPages = (int)Math.Ceiling(totalItemCount / (double)pageSize);

        // Adjust pageNumber if it's greater than the total number of pages
        if (totalPages > 0 && pageNumber > totalPages)
        {
            pageNumber = totalPages;
        }

        // Calculate the index of the first item on the current page
        int skipCount = (pageNumber - 1) * pageSize;

        // Get the items for the current page
        List<T> pagedItems = totalItemCount == 0
            ? []
            : await query.Skip(skipCount).Take(pageSize).ToListAsync();

        // Create the PagedResult object with all the pagination information
        PagedResult<T> result = new()
        {
            Items = pagedItems,
            TotalItemsCount = totalItemCount,
            ItemsPerPage = pageSize,
            CurrentPage = pageNumber,
            TotalPageCount = totalPages
        };

        return result;
    }
}
