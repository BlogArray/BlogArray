using System.ComponentModel.DataAnnotations;

namespace BlogArray.Domain.DTOs;

public class BasicCategoryInfo
{
    public int Id { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Category is required."), StringLength(180)]
    public string Name { get; set; } = default!;

    [StringLength(180)]
    public string Slug { get; set; } = default!;
}

public class CategoryInfoCount : BasicCategoryInfo
{
    public int Count { get; set; } = default!;
}

public class CategoryInfo : CategoryInfoCount
{
    [StringLength(255)]
    public string? Description { get; set; } = default!;
}