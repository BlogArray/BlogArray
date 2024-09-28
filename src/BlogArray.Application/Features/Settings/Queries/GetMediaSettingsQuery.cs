using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using MediatR;

namespace BlogArray.Application.Features.Settings.Queries;

public class GetMediaSettingsQuery() : IRequest<MediaOptions?>
{

}

public class GetMediaSettingsQueryHandler(IAppOptionsRepository repository) : IRequestHandler<GetMediaSettingsQuery, MediaOptions?>
{
    public async Task<MediaOptions?> Handle(GetMediaSettingsQuery request, CancellationToken cancellationToken)
    {
        return await repository.TryGetOption<MediaOptions>("MediaOptions");
    }
}
