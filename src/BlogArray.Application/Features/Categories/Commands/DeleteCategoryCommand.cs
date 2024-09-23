using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using MediatR;

namespace BlogArray.Application.Features.Categories.Commands;

public class DeleteCategoryCommand(int id) : IRequest<ReturnResult<int>>
{
    public int Id { get; set; } = id;
}

internal class DeleteCategoryCommandHandler(ICategoryRepository categoryRepository) : IRequestHandler<DeleteCategoryCommand, ReturnResult<int>>
{
    public async Task<ReturnResult<int>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        return await categoryRepository.DeleteCategoryAsync(request.Id);
    }
}
