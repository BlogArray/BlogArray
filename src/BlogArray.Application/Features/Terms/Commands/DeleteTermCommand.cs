using BlogArray.Domain.DTOs;
using BlogArray.Domain.Enums;
using BlogArray.Domain.Interfaces;
using MediatR;

namespace BlogArray.Application.Features.Terms.Commands;

public class DeleteTermCommand(int id, TermType termType) : IRequest<ReturnResult<int>>
{
    public int Id { get; set; } = id;
    public TermType TermType { get; set; } = termType;
}

internal class DeleteTermCommandHandler(ITermRepository termRepository) : IRequestHandler<DeleteTermCommand, ReturnResult<int>>
{
    public async Task<ReturnResult<int>> Handle(DeleteTermCommand request, CancellationToken cancellationToken)
    {
        return await termRepository.DeleteTermAsync(request.Id, request.TermType);
    }
}
