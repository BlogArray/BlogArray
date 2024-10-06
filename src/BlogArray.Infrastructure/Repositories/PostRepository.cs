using BlogArray.Domain.Interfaces;
using BlogArray.Persistence;
using NetCore.AutoRegisterDi;

namespace BlogArray.Infrastructure.Repositories;

[RegisterAsScoped]
public class PostRepository(AppDbContext db) : IPostRepository
{

}
