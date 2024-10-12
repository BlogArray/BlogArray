using BlogArray.Domain.Enums;

namespace BlogArray.Domain.Entities;

public class PostRevision : AuthorEntityBase
{
    public string RawContent { get; set; } = default!;

    public bool IsLatest { get; set; } = true;

    public EditorType EditorType { get; set; } = default!;

    public int PostId { get; set; }

    public virtual Post Post { get; set; } = default!;
}
