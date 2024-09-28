using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using MediatR;

namespace BlogArray.Application.Features.Settings.Queries;

public class GetContentOptionsQuery() : IRequest<ContentOptions?>
{

}

public class GetContentOptionsQueryHandler(IAppOptionsRepository repository) : IRequestHandler<GetContentOptionsQuery, ContentOptions?>
{
    public async Task<ContentOptions?> Handle(GetContentOptionsQuery request, CancellationToken cancellationToken)
    {
        return await repository.TryGetOption<ContentOptions>("Content");
    }
}
