using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using MediatR;

namespace BlogArray.Application.Features.Settings.Queries;

public class GetEmailSettingsQuery() : IRequest<EmailSettings?>
{

}

public class GetEmailSettingsQueryHandler(IAppOptionsRepository repository) : IRequestHandler<GetEmailSettingsQuery, EmailSettings?>
{
    public async Task<EmailSettings?> Handle(GetEmailSettingsQuery request, CancellationToken cancellationToken)
    {
        return await repository.TryGetOption<EmailSettings>("SMTP");
    }
}
