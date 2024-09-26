using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using MediatR;

namespace BlogArray.Application.Features.Settings.Queries;

public class GetOptionByKeyQuery(string key) : IRequest<AppOptionsBase?>
{
    public string Key { get; } = key;
}

public class GetOptionByKeyQueryHandler(IAppOptionsRepository appOptionsRepository) : IRequestHandler<GetOptionByKeyQuery, AppOptionsBase?>
{
    public async Task<AppOptionsBase?> Handle(GetOptionByKeyQuery request, CancellationToken cancellationToken)
    {
        return await appOptionsRepository.GetOption(request.Key);
    }
}
