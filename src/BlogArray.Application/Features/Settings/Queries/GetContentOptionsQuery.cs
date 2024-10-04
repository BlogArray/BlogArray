using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using MediatR;

namespace BlogArray.Application.Features.Settings.Queries;

public class GetContentOptionsQuery() : IRequest<ContentSettings?>
{

}

public class GetContentOptionsQueryHandler(IAppOptionsRepository repository) : IRequestHandler<GetContentOptionsQuery, ContentSettings?>
{
    public async Task<ContentSettings?> Handle(GetContentOptionsQuery request, CancellationToken cancellationToken)
    {
        return await repository.TryGetOption<ContentSettings>("Content");
    }
}
