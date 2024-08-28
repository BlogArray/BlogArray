using BlogArray.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BlogArray.Domain.Entities;

public class AppOption : KeyBase
{
    [StringLength(256)]
    public string Key { get; set; } = default!;

    public string Value { get; set; } = default!;

    public OptionType OptionType { get; set; } = default!;

    public bool AutoLoad { get; set; } = true;
}
