using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using MediatR;

namespace BlogArray.Application.Features.Settings.Queries;

public class GetCommentSettingsQuery() : IRequest<CommentSettings?>
{

}

public class GetCommentSettingsQueryHandler(IAppOptionsRepository repository) : IRequestHandler<GetCommentSettingsQuery, CommentSettings?>
{
    public async Task<CommentSettings?> Handle(GetCommentSettingsQuery request, CancellationToken cancellationToken)
    {
        return await repository.TryGetOption<CommentSettings>("Comments");
    }
}
