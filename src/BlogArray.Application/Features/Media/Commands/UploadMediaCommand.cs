using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BlogArray.Application.Features.Media.Commands;

public class UploadMediaCommand(List<IFormFile> files, int loggedInUser) : IRequest<ReturnResult<string[]>>
{
    public List<IFormFile> Files { get; set; } = files;
    public int LoggedInUserId { get; set; } = loggedInUser;
}

internal class UploadMediaCommandHandler(IMediaRepository mediaRepository) : IRequestHandler<UploadMediaCommand, ReturnResult<string[]>>
{
    public async Task<ReturnResult<string[]>> Handle(UploadMediaCommand request, CancellationToken cancellationToken)
    {
        string[] fileValidation = mediaRepository.ValidateFiles(request.Files);

        return fileValidation.Length > 0
            ? new ReturnResult<string[]>
            {
                Message = "One or more invalid files.",
                Code = StatusCodes.Status400BadRequest,
                Title = "Media.Invalid",
                Result = fileValidation
            }
            : await mediaRepository.Upload(request.Files, request.LoggedInUserId);
    }
}
