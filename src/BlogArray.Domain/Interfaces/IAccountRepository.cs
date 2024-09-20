using BlogArray.Domain.DTOs;

namespace BlogArray.Domain.Interfaces;

public interface IAccountRepository
{
    Task<LoginResult> Authenticate(LoginRequest loginRequest);

    Task<UserInfo?> GetUser(int userId);
}
