using BlogArray.Domain.DTOs;
using BlogArray.Domain.Entities;
using BlogArray.Domain.Interfaces;
using BlogArray.Infrastructure.Extensions;
using BlogArray.Persistence;
using BlogArray.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BlogArray.Infrastructure.Repositories;

public class CategoryRepository(AppDbContext db) : ICategoryRepository
{
    public Task<CategoryInfo?> GetCategoryAsync(int id)
    {
        return db.Terms.Select(c => new CategoryInfo
        {
            Id = c.Id,
            Name = c.Name,
            Slug = c.Slug,
            Description = c.Description,
            Count = c.PostTerms.Count
        }).FirstOrDefaultAsync();
    }

    public async Task<PagedResult<CategoryInfoCount>> GetPaginatedCategoryAsync(int pageNumber, int pageSize, string? searchTerm)
    {
        IQueryable<Term> query = db.Terms;

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(u => u.Slug.Contains(searchTerm) ||
                                     u.Name.Contains(searchTerm));
        }

        PagedResult<CategoryInfoCount> pagedResult = await query
            .OrderBy(u => u.Id) // Order by Id
            .Select(u => new CategoryInfoCount
            {
                Id = u.Id,
                Name = u.Name,
                Slug = u.Slug,
                Count = u.PostTerms.Count
            })
            .ToPagedListAsync(pageNumber, pageSize);

        return pagedResult;
    }

    public async Task<ReturnResult<int>> CreateCategoryAsync(CategoryInfo category)
    {
        string slug = category.Slug.ToSlug();

        if (await db.Terms.Where(p => p.Slug == slug).AnyAsync())
        {
            return new ReturnResult<int>
            {
                Code = StatusCodes.Status400BadRequest,
                Title = "Category.Exists",
                Message = $"There is already a category with the name '{category.Name}', try another name."
            };
        }

        Term term = new()
        {
            Name = category.Name,
            Slug = slug,
            TermType = Domain.Enums.TermType.Category,
            Description = category.Description,
        };

        await db.Terms.AddAsync(term);
        await db.SaveChangesAsync();

        return new ReturnResult<int>
        {
            Code = StatusCodes.Status200OK,
            Title = "Category.Created",
            Message = $"Category '{category.Name}' was successfully created.",
            Result = term.Id
        };
    }

    public async Task<ReturnResult<int>> EditCategoryAsync(int id, CategoryInfo category)
    {
        string slug = category.Slug.ToSlug();

        if (await db.Terms.Where(p => p.Slug == slug).AnyAsync())
        {
            return new ReturnResult<int>
            {
                Code = StatusCodes.Status400BadRequest,
                Title = "Category.Exists",
                Message = $"There is already a category with the name '{category.Name}', try another name."
            };
        }

        Term? term = await db.Terms.FirstOrDefaultAsync(a => a.Id == id);

        if (term == null)
        {
            return new ReturnResult<int>
            {
                Code = StatusCodes.Status404NotFound,
                Title = "Category.NotFound",
                Message = $"The category with the name '{category.Name}' could not be found in the system."
            };
        }

        term.Name = category.Name;
        term.Slug = slug;
        term.Description = category.Description;

        await db.SaveChangesAsync();

        return new ReturnResult<int>
        {
            Code = StatusCodes.Status200OK,
            Title = "Category.Updated",
            Message = $"Category '{category.Name}' was successfully updated.",
            Result = term.Id
        };
    }

}