namespace BlogArray.Domain.DTOs;

public class BasicCategoryInfo
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public string Slug { get; set; } = default!;
}

public class CategoryInfo : CategoryInfoDescription
{
    public int PostsCount { get; set; } = default!;
}

public class CategoryInfoDescription : BasicCategoryInfo
{
    public string? Description { get; set; } = default!;
}