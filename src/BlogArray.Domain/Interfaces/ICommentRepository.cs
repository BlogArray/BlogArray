using BlogArray.Domain.DTOs;

namespace BlogArray.Domain.Interfaces;

public interface ICommentRepository
{
    Task AddAsync(CreateCommentDto comment);

    Task UpdateAsync(CreateCommentDto comment);

    Task<CommentDTO?> GetCommentByIdAsync(int id);

    Task<IEnumerable<CommentDTO>> GetCommentsForPostAsync(int postId);
}
