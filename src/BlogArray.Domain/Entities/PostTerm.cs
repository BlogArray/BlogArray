using System.ComponentModel;

namespace BlogArray.Domain.Entities;

public class PostTerm
{
    public int TermId { get; set; }

    public int PostId { get; set; }

    [DefaultValue(0)]
    public int Order { get; set; }

    public virtual Term Term { get; set; } = default!;

    public virtual Post Post { get; set; } = default!;

}