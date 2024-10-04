using BlogArray.Domain.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogArray.Domain.DTOs;

public class AppOptionsBase
{
    [StringLength(256)]
    public string Key { get; set; } = default!;

    public string Value { get; set; } = default!;

    public OptionType OptionType { get; set; } = default!;

    public bool AutoLoad { get; set; } = true;
}

public class SiteInfo
{
    // The title of the site (e.g., website name)
    public string Title { get; set; } = default!;

    // A brief tagline or motto for the site
    public string Tagline { get; set; } = default!;

    // A short description of the site, recommended length between 60 and 220 characters
    [Display(Name = "Description")]
    [StringLength(220, ErrorMessage = "We recommend keeping your description between {2} and {1} characters.", MinimumLength = 60)]
    public string? Description { get; set; } = default!;

    // Path or URL for the site icon (favicon)
    public string? IconUrl { get; set; }

    // Path or URL for the site logo
    public string? LogoUrl { get; set; }

    // The base URL for the site (new field for easier site configuration)
    [Url(ErrorMessage = "Please provide a valid URL.")]
    public string? SiteAddress { get; set; }

    // Email address of the site administrator
    [EmailAddress(ErrorMessage = "Please provide a valid email address.")]
    public string? AdminEmail { get; set; }

    // Specifies whether anyone can register for the site without an invitation
    public bool AllowUserRegistration { get; set; } = false;

    // Default role assigned to new users, represented by an integer (e.g., 1 = Subscriber, 2 = Editor)
    public int DefaultUserRole { get; set; }

}

public class EmailSettings
{
    // The username for SMTP authentication
    public string Username { get; set; } = string.Empty;

    // The password for SMTP authentication
    public string Password { get; set; } = string.Empty;

    // The SMTP server host address
    public string Host { get; set; } = string.Empty;

    // The port number for the SMTP server
    public int Port { get; set; } = 587; // Common default port for SMTP

    // Indicates whether to use SSL for the connection
    public bool UseSSL { get; set; } = true; // Default to true for security
}

public class ContentSettings
{
    // Defines the type of content displayed on the homepage (e.g., "posts", "page")
    public string HomePageContentType { get; set; } = "posts";

    // The static page to be used as the homepage if HomePageContentType is set to "page"
    public string? StaticHomePageUrl { get; set; }

    // URL of the posts page (only applicable if a static home page is set)
    //public string? PostsPageUrl { get; set; }

    // The number of posts or items to display per page (for pagination)
    public int ItemsPerPage { get; set; } = 10;

    // Indicates whether the site should be indexed by search engines
    public bool SearchEngineVisibility { get; set; } = true;

    // The default category ID assigned to new posts or pages
    public int? DefaultCategoryId { get; set; }

    // URL or path to the default cover image used for posts or pages
    public string? DefaultCoverImageUrl { get; set; }

    // Enable comments by default on new posts or pages
    public bool EnableCommentsByDefault { get; set; } = true;

    // Maximum number of featured posts displayed on the homepage based on theme
    public int MaxFeaturedPosts { get; set; } = 5;

    // Set whether pagination uses numbered pages or 'load more' (infinite scroll) based on theme
    public bool UseInfiniteScroll { get; set; } = false;
}

public class MediaSettings
{
    // Settings for generating thumbnails
    //public bool GenerateThumbnails { get; set; } = true; // Default to true if required

    // Settings for resizing images
    //public bool ResizeImages { get; set; } = true; // Default to true if required

    // Sizes for media (Small, Medium, Large)
    public MediaSize SmallSize { get; set; } = new MediaSize { MaxHeight = 150, MaxWidth = 150 };
    public MediaSize MediumSize { get; set; } = new MediaSize { MaxHeight = 500, MaxWidth = 500 };
    public MediaSize LargeSize { get; set; } = new MediaSize { MaxHeight = 1024, MaxWidth = 1024 };

    // Indicates whether to optimize images
    public bool OptimizeImages { get; set; } = true;

    // Quality level for optimized images (between 10 and 100)
    [Range(10, 100, ErrorMessage = "The value should be between 10 and 100.")]
    public int? OptimizedQuality { get; set; } = 75;

    // Indicates whether to organize uploaded files into folders
    public bool OrganizeUploads { get; set; } = true;

}

public class MediaSize
{
    public int MaxWidth { get; set; } = default!;
    public int MaxHeight { get; set; } = default!;
}

public class CommentSettings
{
    // Indicates whether the user must be logged in to post a comment
    public bool RequireLogin { get; set; } = false;

    // Indicates whether anonymous commenting is allowed
    public bool AllowAnonymous { get; set; } = true;

    // Specifies whether comments need to be manually approved by moderators/admins
    public bool RequireManualApproval { get; set; } = false;

    // URL or path to the default avatar image for commenters
    public string DefaultAvatarUrl { get; set; } = string.Empty;

    // Number of initial comments to show per post before loading additional threads in separate pages
    public int InitialCommentsPerPost { get; set; } = 10;

    // Time limit for editing a comment after it is posted (in minutes)
    public int CommentEditWindowMinutes { get; set; } = 15;
}