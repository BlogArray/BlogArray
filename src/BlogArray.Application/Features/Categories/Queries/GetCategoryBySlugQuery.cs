using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using MediatR;

namespace BlogArray.Application.Features.Categories.Queries;

public class GetCategoryBySlugQuery(string slug) : IRequest<CategoryInfo?>
{
    public string Slug { get; } = slug;
}

public class GetCategoryBySlugQueryHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetCategoryBySlugQuery, CategoryInfo?>
{
    public async Task<CategoryInfo?> Handle(GetCategoryBySlugQuery request, CancellationToken cancellationToken)
    {
        return await categoryRepository.GetCategoryAsync(request.Slug);
    }
}
