using System.ComponentModel.DataAnnotations;

namespace BlogArray.Domain.Entities;

public class Category : KeyBase
{
    [StringLength(180)]
    public string Name { get; set; } = default!;

    [StringLength(180)]
    public string Slug { get; set; } = default!;

    [StringLength(255)]
    public string? Description { get; set; } = default!;

    public List<PostCategory>? PostCategories { get; set; }
}

