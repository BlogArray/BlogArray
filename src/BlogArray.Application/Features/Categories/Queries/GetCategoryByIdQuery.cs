using BlogArray.Domain.DTOs;
using BlogArray.Domain.Enums;
using BlogArray.Domain.Interfaces;
using MediatR;

namespace BlogArray.Application.Features.Categories.Queries;

public class GetCategoryByIdQuery(int Id, TermType termType) : IRequest<TermInfo?>
{
    public int Id { get; } = Id;
    public TermType TermType { get; set; } = termType;
}

public class GetCategoryByIdQueryHandler(ITermRepository categoryRepository) : IRequestHandler<GetCategoryByIdQuery, TermInfo?>
{
    public async Task<TermInfo?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        return await categoryRepository.GetTermAsync(request.Id, request.TermType);
    }
}
