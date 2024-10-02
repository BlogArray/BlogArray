using BlogArray.Domain.DTOs;
using BlogArray.Domain.Enums;
using BlogArray.Domain.Interfaces;
using MediatR;

namespace BlogArray.Application.Features.Terms.Queries;

public class GetTermByIdQuery(int Id, TermType termType) : IRequest<TermInfo?>
{
    public int Id { get; } = Id;
    public TermType TermType { get; set; } = termType;
}

public class GetTermByIdQueryHandler(ITermRepository categoryRepository) : IRequestHandler<GetTermByIdQuery, TermInfo?>
{
    public async Task<TermInfo?> Handle(GetTermByIdQuery request, CancellationToken cancellationToken)
    {
        return await categoryRepository.GetTermAsync(request.Id, request.TermType);
    }
}
