using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using MediatR;

namespace BlogArray.Application.Users.Commands;

public class AuthenticateCommand(LoginRequest model) : IRequest<LoginResult>
{
    public LoginRequest Model { get; set; } = model;
}

internal class AuthenticateCommandHandler(IAccountRepository accountRepository) : IRequestHandler<AuthenticateCommand, LoginResult>
{
    public async Task<LoginResult> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
    {
        return await accountRepository.Authenticate(request.Model);
    }
}
