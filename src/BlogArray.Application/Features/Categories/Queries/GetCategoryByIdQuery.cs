using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using MediatR;

namespace BlogArray.Application.Features.Categories.Queries;

public class GetCategoryByIdQuery(int Id) : IRequest<CategoryInfo?>
{
    public int Id { get; } = Id;
}

public class GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetCategoryByIdQuery, CategoryInfo?>
{
    public async Task<CategoryInfo?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        return await categoryRepository.GetCategoryAsync(request.Id);
    }
}
