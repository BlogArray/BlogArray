using System.ComponentModel;

namespace BlogArray.Domain.Entities;

public class PostCategory
{
    public int CategoryId { get; set; }

    public int PostId { get; set; }

    [DefaultValue(0)]
    public int Order { get; set; }

    public virtual Category Category { get; set; } = default!;

    public virtual Post Post { get; set; } = default!;

}