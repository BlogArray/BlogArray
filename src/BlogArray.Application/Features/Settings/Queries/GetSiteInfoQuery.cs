using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using MediatR;

namespace BlogArray.Application.Features.Settings.Queries;

public class GetSiteInfoQuery() : IRequest<SiteInfo?>
{

}

public class GetSiteInfoQueryHandler(IAppOptionsRepository repository) : IRequestHandler<GetSiteInfoQuery, SiteInfo?>
{
    public async Task<SiteInfo?> Handle(GetSiteInfoQuery request, CancellationToken cancellationToken)
    {
        return await repository.TryGetOption<SiteInfo>("SiteInfo");
    }
}
