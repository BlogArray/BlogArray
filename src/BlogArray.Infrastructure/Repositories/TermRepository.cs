using BlogArray.Domain.DTOs;
using BlogArray.Domain.Entities;
using BlogArray.Domain.Enums;
using BlogArray.Domain.Interfaces;
using BlogArray.Infrastructure.Extensions;
using BlogArray.Persistence;
using BlogArray.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NetCore.AutoRegisterDi;

namespace BlogArray.Infrastructure.Repositories;

[RegisterAsScoped]
public class TermRepository(AppDbContext db) : ITermRepository
{
    public async Task<PagedResult<TermInfo>> GetPaginatedTermsAsync(int pageNumber, int pageSize, TermType termType, string? searchTerm)
    {
        IQueryable<Term> query = db.Terms.Where(t => t.TermType == termType);

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(u => u.Slug.Contains(searchTerm) ||
                                     u.Name.Contains(searchTerm));
        }

        PagedResult<TermInfo> pagedResult = await query
            .OrderByDescending(u => u.Id) // Order by Id
            .Select(u => new TermInfo
            {
                Id = u.Id,
                Name = u.Name,
                Slug = u.Slug,
                Description = u.Description,
                PostsCount = u.PostTerms.Count
            })
            .ToPagedListAsync(pageNumber, pageSize);

        return pagedResult;
    }

    public Task<TermInfo?> GetTermAsync(int id, TermType termType)
    {
        return db.Terms.Where(t => t.TermType == termType)
            .Select(c => new TermInfo
            {
                Id = c.Id,
                Name = c.Name,
                Slug = c.Slug,
                Description = c.Description,
                PostsCount = c.PostTerms.Count
            }).FirstOrDefaultAsync(c => c.Id == id);
    }

    public Task<TermInfo?> GetTermAsync(string slug, TermType termType)
    {
        return db.Terms.Where(t => t.TermType == termType)
            .Select(c => new TermInfo
            {
                Id = c.Id,
                Name = c.Name,
                Slug = c.Slug,
                Description = c.Description,
                PostsCount = c.PostTerms.Count
            }).FirstOrDefaultAsync(c => c.Slug == slug);
    }

    public async Task<ReturnResult<int>> CreateTermAsync(TermInfoDescription term, TermType termType)
    {
        string slug = term.Slug.ToSlug();

        if (await db.Terms.Where(p => p.Slug == slug && p.TermType == termType).AnyAsync())
        {
            return new ReturnResult<int>
            {
                Code = StatusCodes.Status400BadRequest,
                Title = $"{(termType.Equals(TermType.Category) ? "Category" : "Tag")}.Exists",
                Message = $"There is already a {(termType.Equals(TermType.Category) ? "category" : "tag")} with the name '{slug}', try another name."
            };
        }

        Term newTerm = new()
        {
            Name = term.Name,
            Slug = slug,
            TermType = termType,
            Description = term.Description,
        };

        await db.Terms.AddAsync(newTerm);
        await db.SaveChangesAsync();

        return new ReturnResult<int>
        {
            Code = StatusCodes.Status200OK,
            Title = "Category.Created",
            Message = $"{(termType.Equals(TermType.Category) ? "Category" : "Tag")} '{newTerm.Name}' was successfully created.",
            Result = newTerm.Id
        };
    }

    public async Task<ReturnResult<int>> EditTermAsync(int id, TermInfoDescription term, TermType termType)
    {
        string slug = term.Slug.ToSlug();

        if (await db.Terms.Where(p => p.Slug == slug && p.TermType == termType && p.Id != id).AnyAsync())
        {
            return new ReturnResult<int>
            {
                Code = StatusCodes.Status400BadRequest,
                Title = $"{(termType.Equals(TermType.Category) ? "Category" : "Tag")}.Exists",
                Message = $"There is already a {(termType.Equals(TermType.Category) ? "category" : "tag")} with the name '{term.Slug}', try another name."
            };
        }

        Term? exTerm = await db.Terms.FirstOrDefaultAsync(a => a.Id == id && a.TermType == termType);

        if (exTerm == null)
        {
            return new ReturnResult<int>
            {
                Code = StatusCodes.Status404NotFound,
                Title = $"{(termType.Equals(TermType.Category) ? "Category" : "Tag")}.NotFound",
                Message = $"The {(termType.Equals(TermType.Category) ? "category" : "tag")} with the name '{term.Slug}' could not be found in the system."
            };
        }

        exTerm.Name = term.Name;
        exTerm.Slug = slug;
        exTerm.Description = term.Description;

        await db.SaveChangesAsync();

        return new ReturnResult<int>
        {
            Code = StatusCodes.Status200OK,
            Title = $"{(termType.Equals(TermType.Category) ? "Category" : "Tag")}.Updated",
            Message = $"{(termType.Equals(TermType.Category) ? "Category" : "Tag")} '{exTerm.Name}' was successfully updated.",
            Result = exTerm.Id
        };
    }

    public async Task<ReturnResult<int>> DeleteTermAsync(int id, TermType termType)
    {
        Term? term = await db.Terms.Where(t => t.TermType == termType).FirstOrDefaultAsync(a => a.Id == id);

        if (term == null)
        {
            return new ReturnResult<int>
            {
                Code = StatusCodes.Status404NotFound,
                Title = $"{(termType.Equals(TermType.Category) ? "Category" : "Tag")}.NotFound",
                Message = $"The {(termType.Equals(TermType.Category) ? "category" : "tag")} with the id '{id}' could not be found in the system."
            };
        }

        //TODO Check for default category
        //if (term.Id == AppConstants.PageOptions.DefaultCategory)
        //{
        //    return new ReturnResult<int>
        //    {
        //        Code = StatusCodes.Status404NotFound,
        //        Title = "Category.NotFound",
        //        Message = $"You cannot deleted the default category."
        //    };
        //}

        //await db.Terms.Where(p => p.Id == id).ExecuteDeleteAsync();

        db.Terms.Remove(term);

        await db.SaveChangesAsync();

        return new ReturnResult<int>
        {
            Code = StatusCodes.Status200OK,
            Title = $"{(termType.Equals(TermType.Category) ? "Category" : "Tag")}.Deleted",
            Message = $"{(termType.Equals(TermType.Category) ? "Category" : "Tag")} '{term.Name}' was successfully deleted.",
            Result = term.Id
        };
    }

    public async Task<List<Term>> GetTermsByIdsAsync(List<int> termIds)
    {
        return await db.Terms.Where(t => termIds.Contains(t.Id)).ToListAsync();
    }
}
