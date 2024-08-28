using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using UoN.ExpressiveAnnotations.NetCore.Attributes;

namespace BlogArray.Domain.DTOs;

public class AppOptionBase
{
    public string Key { get; set; } = default!;

    public string Value { get; set; } = default!;
}

public class SiteInfoVM
{
    public string Title { get; set; } = default!;

    public string Tagline { get; set; } = default!;

    [Display(Name = "Description")]
    [StringLength(220, ErrorMessage = "We recommend keeping your description between {2} and {1} characters.", MinimumLength = 60)]
    public string? Description { get; set; } = default!;

    public string? Icon { get; set; }

    public string? Logo { get; set; }
}

public class SMTPOptionsVM
{
    public string Username { get; set; } = default!;

    public string Password { get; set; } = default!;

    public string Host { get; set; } = default!;

    public int Port { get; set; }

    public bool UseSSL { get; set; }
}

public class PageOptionsVM
{
    //Accepts posts, page
    public string HomePage { get; set; } = default!;

    //If HomePage is page
    [Required(ErrorMessage = "Select a page")]
    [RequiredIf("HomePage == 'page'", AllowEmptyStrings = false, ErrorMessage = "Select a page")]
    [AssertThat("StaticHomePage != null && StaticHomePage != ''", ErrorMessage = "Select a page")]
    public string StaticHomePage { get; set; } = default!;

    //If HomePage is page
    //public string PostsPage { get; set; } = default!;

    public int? DefaultCategory { get; set; }

    //Post or Page default cover
    public string? DefaultCover { get; set; }

    //Posts per page
    public int ItemsPerPage { get; set; }

    //Searchengine visibility
    public bool SearchEngineVisibility { get; set; }
}

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

public class MediaOptions
{
    //public bool GenerateThumbnails { get; set; } = default!;

    //public bool ResizeImage { get; set; } = default!;

    public MediaSize SmallSize { get; set; } = default!;

    public MediaSize MediumSize { get; set; } = default!;

    public MediaSize LargeSize { get; set; } = default!;

    public bool OptimizeImage { get; set; } = default!;

    [Range(10, 100, ErrorMessage = "The value should be between 10 and 100.")]
    [RequiredIf("OptimizeImage == true", AllowEmptyStrings = false, ErrorMessage = "Enter image quality to optimise")]
    public int? OptimizedQuality { get; set; } = default!;

    public bool OrganizeUploads { get; set; } = default!;

    public MediaOptions()
    {
        SmallSize = new MediaSize { MaxHeight = 150, MaxWidth = 150 };
        MediumSize = new MediaSize { MaxHeight = 500, MaxWidth = 500 };
        LargeSize = new MediaSize { MaxHeight = 1024, MaxWidth = 1024 };

        OptimizeImage = true;

        OptimizedQuality = 75;

        OrganizeUploads = true;
    }
}

public class MediaSize
{
    public int MaxWidth { get; set; } = default!;
    public int MaxHeight { get; set; } = default!;
}