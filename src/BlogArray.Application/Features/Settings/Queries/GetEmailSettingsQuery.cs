using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using MediatR;

namespace BlogArray.Application.Features.Settings.Queries;

public class GetEmailSettingsQuery() : IRequest<SMTPOptions?>
{

}

public class GetEmailSettingsQueryHandler(IAppOptionsRepository repository) : IRequestHandler<GetEmailSettingsQuery, SMTPOptions?>
{
    public async Task<SMTPOptions?> Handle(GetEmailSettingsQuery request, CancellationToken cancellationToken)
    {
        return await repository.TryGetOption<SMTPOptions>("SMTP");
    }
}
