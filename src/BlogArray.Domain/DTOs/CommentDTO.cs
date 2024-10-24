namespace BlogArray.Domain.DTOs;

public class CommentDTO: CreateCommentDto
{
}

public class CreateCommentDto
{
    public string Author { get; set; } = default!;
    public string? AuthorEmail { get; set; }
    public string? AuthorSite { get; set; }
    public string Content { get; set; } = default!;
    public int? ParentId { get; set; }
    public int PostId { get; set; }
}

public class EditCommentDto : CreateCommentDto
{
    public int Id { get; set; }
}