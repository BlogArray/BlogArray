using BlogArray.Domain.Interfaces;
using BlogArray.Persistence;

namespace BlogArray.Infrastructure.Repositories;

public class AccountRepository(AppDbContext db) : IAccountRepository
{
    public bool Authenticate(string userName, string password)
    {
        return false;
    }
}
