using BlogArray.Domain.DTOs;
using BlogArray.Domain.Enums;
using BlogArray.Domain.Interfaces;
using MediatR;

namespace BlogArray.Application.Features.Categories.Commands;

public class DeleteCategoryCommand(int id, TermType termType) : IRequest<ReturnResult<int>>
{
    public int Id { get; set; } = id;
    public TermType TermType { get; set; } = termType;
}

internal class DeleteCategoryCommandHandler(ITermRepository categoryRepository) : IRequestHandler<DeleteCategoryCommand, ReturnResult<int>>
{
    public async Task<ReturnResult<int>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        return await categoryRepository.DeleteTermAsync(request.Id, request.TermType);
    }
}
