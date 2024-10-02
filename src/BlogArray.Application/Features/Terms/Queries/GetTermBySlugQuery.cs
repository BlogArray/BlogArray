using BlogArray.Domain.DTOs;
using BlogArray.Domain.Enums;
using BlogArray.Domain.Interfaces;
using MediatR;

namespace BlogArray.Application.Features.Terms.Queries;

public class GetTermBySlugQuery(string slug, TermType termType) : IRequest<TermInfo?>
{
    public string Slug { get; } = slug;
    public TermType TermType { get; set; } = termType;
}

public class GetTermBySlugQueryHandler(ITermRepository categoryRepository) : IRequestHandler<GetTermBySlugQuery, TermInfo?>
{
    public async Task<TermInfo?> Handle(GetTermBySlugQuery request, CancellationToken cancellationToken)
    {
        return await categoryRepository.GetTermAsync(request.Slug, request.TermType);
    }
}
