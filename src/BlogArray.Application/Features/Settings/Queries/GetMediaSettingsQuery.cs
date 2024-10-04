using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using MediatR;

namespace BlogArray.Application.Features.Settings.Queries;

public class GetMediaSettingsQuery() : IRequest<MediaSettings?>
{

}

public class GetMediaSettingsQueryHandler(IAppOptionsRepository repository) : IRequestHandler<GetMediaSettingsQuery, MediaSettings?>
{
    public async Task<MediaSettings?> Handle(GetMediaSettingsQuery request, CancellationToken cancellationToken)
    {
        return await repository.TryGetOption<MediaSettings>("Media");
    }
}
