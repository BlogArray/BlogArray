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

    [StringLength(160)]
    public string? Cover { get; set; }

    /// <summary>
    /// Latest revision parsed HTML content
    /// </summary>
    public string Content { get; set; } = default!;

    public int Views { get; set; }

    public PostType PostType { get; set; }

    public PostStatus PostStatus { get; set; } = PostStatus.Published;

    public DateTime? PublishedOn { get; set; }

    public bool IsFeatured { get; set; }

    /// <summary>
    /// Indicates whether the post layout should be full-width
    /// </summary>
    public bool IsFullWidth { get; set; }

    public bool EnableContactForm { get; set; }

    public bool DisplayPostTitle { get; set; }

    public bool DisplayAuthorInfo { get; set; }

    public bool EnableSocialSharing { get; set; }

    public bool EnableComments { get; set; }

    public bool DisplayCoverImage { get; set; }

    public bool EnableTableOfContents { get; set; }

    public int ReadingTimeEstimate { get; set; }

    public int CommentsCount { get; set; }

    public virtual List<PostTerm>? Terms { get; set; }

    public virtual List<Comment>? Comments { get; set; }

    public virtual List<PostRevision>? PostRevisions { get; set; }

}
