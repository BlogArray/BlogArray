using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using MediatR;

namespace BlogArray.Application.Features.AppOptions.Commands;

public class DeleteOptionCommand(string key) : IRequest<ReturnResult<int>>
{
    public string Key { get; set; } = key;
}

internal class DeleteOptionCommandHandler(IAppOptionsRepository appOptionsRepository) : IRequestHandler<DeleteOptionCommand, ReturnResult<int>>
{
    public async Task<ReturnResult<int>> Handle(DeleteOptionCommand request, CancellationToken cancellationToken)
    {
        return await appOptionsRepository.DeleteAsync(request.Key);
    }
}
