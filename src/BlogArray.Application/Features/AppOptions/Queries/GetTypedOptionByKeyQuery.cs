using BlogArray.Domain.Interfaces;
using MediatR;

namespace BlogArray.Application.Features.AppOptions.Queries;

public class GetTypedOptionByKeyQuery<T>(string key) : IRequest<T?>
{
    public string Key { get; } = key;

}

public class GetTypedOptionByKeyQueryHandler<T>(IAppOptionsRepository repository) : IRequestHandler<GetTypedOptionByKeyQuery<T>, T?>
{
    public async Task<T?> Handle(GetTypedOptionByKeyQuery<T> request, CancellationToken cancellationToken)
    {
        return await repository.TryGetOption<T>(request.Key);
    }
}
