using System.ComponentModel;

namespace BlogArray.Domain.DTOs;

public class MenuItem
{
    [DisplayName("Menu name")]
    public string Name { get; set; } = default!;

    public int Id { get; set; } = default!;

    public List<MenuLink> Links { get; set; } = [];
}

public class MenuLink
{
    public string Page { get; set; } = default!;

    public string Link { get; set; } = default!;

    public List<MenuLink> SubLinks { get; set; } = []; // Nested links
}

public class AppMenus
{
    [DisplayName("Header menu 1")]
    public string HeaderMenu { get; set; } = default!;

    [DisplayName("Header menu 2")]
    public string HeaderMenu2 { get; set; } = default!;

    [DisplayName("Header menu 3")]
    public string HeaderMenu3 { get; set; } = default!;

    [DisplayName("Social menu 1")]
    public string SocialMenu { get; set; } = default!;

    [DisplayName("Social menu 2")]
    public string SocialMenu2 { get; set; } = default!;

    [DisplayName("Footer menu 1")]
    public string FooterMenu { get; set; } = default!;

    [DisplayName("Footer menu 2")]
    public string FooterMenu2 { get; set; } = default!;

    [DisplayName("Footer menu 3")]
    public string FooterMenu3 { get; set; } = default!;

    [DisplayName("Footer menu 4")]
    public string FooterMenu4 { get; set; } = default!;
}
