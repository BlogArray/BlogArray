using BlogArray.Domain.DTOs;
using BlogArray.Domain.Enums;
using BlogArray.Domain.Interfaces;
using MediatR;

namespace BlogArray.Application.Features.Categories.Queries;

public class GetCategoryBySlugQuery(string slug, TermType termType) : IRequest<TermInfo?>
{
    public string Slug { get; } = slug;
    public TermType TermType { get; set; } = termType;
}

public class GetCategoryBySlugQueryHandler(ITermRepository categoryRepository) : IRequestHandler<GetCategoryBySlugQuery, TermInfo?>
{
    public async Task<TermInfo?> Handle(GetCategoryBySlugQuery request, CancellationToken cancellationToken)
    {
        return await categoryRepository.GetTermAsync(request.Slug, request.TermType);
    }
}
