using BlogArray.Domain.DTOs;

namespace BlogArray.Domain.Interfaces;

public interface IAccountRepository
{
    Task<SignInResult> Authenticate(SignIn signIn);
}
