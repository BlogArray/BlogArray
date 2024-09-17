using BlogArray.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BlogArray.Domain.Entities;

public class Term : KeyBase
{
    [StringLength(180)]
    public string Name { get; set; } = default!;

    [StringLength(180)]
    public string Slug { get; set; } = default!;

    [StringLength(255)]
    public string? Description { get; set; } = default!;

    public TermType TermType { get; set; }

    public List<PostTerm>? PostTerms { get; set; }
}
