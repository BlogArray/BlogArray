using BlogArray.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BlogArray.Domain.Entities;

public class Post : EntityBase
{
    [Required]
    [StringLength(160)]
    public string Title { get; set; } = default!;

    [Required]
    [StringLength(160)]
    public string Slug { get; set; } = default!;

    [Required]
    [StringLength(450)]
    public string Description { get; set; } = default!;

    public string RawContent { get; set; } = default!;

    public string ParsedContent { get; set; } = default!;

    [StringLength(160)]
    public string? Cover { get; set; }

    public int Views { get; set; }

    public PostType PostType { get; set; }

    public bool IsPublished { get; set; }

    public DateTime? PublishedOn { get; set; }

    public bool IsFeatured { get; set; }

    //public bool IsWidePage { get; set; }

    //public bool ShowContactPage { get; set; }

    public bool ShowHeading { get; set; }

    public bool ShowAuthor { get; set; }

    public bool ShowSharingIcon { get; set; }

    public bool AllowComments { get; set; }

    public bool ShowCover { get; set; }

    public bool PendingReview { get; set; }

    public virtual List<PostCategory>? Categories { get; set; }
}
