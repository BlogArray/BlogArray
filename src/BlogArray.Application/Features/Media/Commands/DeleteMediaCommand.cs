using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using MediatR;

namespace BlogArray.Application.Features.Media.Commands;

public class DeleteMediaCommand(List<int> files, int loggedInUser, bool isPermanent = false) : IRequest<ReturnResult<int>>
{
    public List<int> Files { get; set; } = files;

    public int LoggedInUserId { get; set; } = loggedInUser;

    public bool IsPermanent { get; set; } = isPermanent;
}

internal class DeleteMediaCommandHandler(IMediaRepository mediaRepository) : IRequestHandler<DeleteMediaCommand, ReturnResult<int>>
{
    public async Task<ReturnResult<int>> Handle(DeleteMediaCommand request, CancellationToken cancellationToken)
    {
        return await mediaRepository.Delete(request.Files, request.IsPermanent);
    }
}
