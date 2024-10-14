using BlogArray.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BlogArray.Domain.DTOs;

public class CreatePostDTO
{
    [Required]
    [StringLength(160)]
    public string Title { get; set; } = default!;

    [Required]
    [StringLength(450)]
    public string Description { get; set; } = default!;

    public string? Cover { get; set; }

    [Required]
    public string Content { get; set; } = default!;

    public PostType PostType { get; set; }

    public PostStatus PostStatus { get; set; } = PostStatus.Published;

    public bool IsFeatured { get; set; }

    public bool IsFullWidth { get; set; }

    public bool EnableContactForm { get; set; }

    public bool DisplayPostTitle { get; set; }

    public bool DisplayAuthorInfo { get; set; }

    public bool EnableSocialSharing { get; set; }

    public bool EnableComments { get; set; }

    public bool DisplayCoverImage { get; set; }

    public bool EnableTableOfContents { get; set; }

    public List<int>? TermIds { get; set; }
}

public class EditPostDTO : CreatePostDTO
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(160)]
    public string Slug { get; set; } = default!;

}
