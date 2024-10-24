using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using BlogArray.Persistence;
using NetCore.AutoRegisterDi;

namespace BlogArray.Infrastructure.Repositories;

[RegisterAsScoped]
public class CommentRepository(AppDbContext db) : ICommentRepository
{
    public Task AddAsync(CreateCommentDto comment)
    {
        throw new NotImplementedException();
    }

    public Task<CommentDTO?> GetCommentByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CommentDTO>> GetCommentsForPostAsync(int postId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(CreateCommentDto comment)
    {
        throw new NotImplementedException();
    }
}
