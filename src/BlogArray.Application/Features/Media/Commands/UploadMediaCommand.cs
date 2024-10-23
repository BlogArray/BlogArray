using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BlogArray.Application.Features.Media.Commands;

public class UploadMediaCommand(List<IFormFile> files) : IRequest<ReturnResult<string[]>>
{
    public List<IFormFile> Files { get; set; } = files;
}

internal class UploadMediaCommandHandler(IMediaRepository mediaRepository) : IRequestHandler<UploadMediaCommand, ReturnResult<string[]>>
{
    public async Task<ReturnResult<string[]>> Handle(UploadMediaCommand request, CancellationToken cancellationToken)
    {
        var fileValidation = mediaRepository.ValidateFiles(request.Files);

        if (fileValidation.Length > 0)
        {
            return new ReturnResult<string[]>
            {
                Message = "One or more invalid files.",
                Code = StatusCodes.Status400BadRequest,
                Title = "Media.Invalid",
                Result = fileValidation
            };
        }
        else
        {
            return new ReturnResult<string[]>
            {
                Message = "One or more invalid files.",
                Code = StatusCodes.Status200OK,
                Title = "Media.Valid",
                Result = fileValidation
            };
        }
    }
}
