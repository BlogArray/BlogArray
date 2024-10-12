using BlogArray.Domain.Enums;
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

/// <summary>
/// Basic site information
/// </summary>
public class SiteInfo
{
    /// <summary>
    /// The title of the site (e.g., website name)
    /// </summary>
    public string Title { get; set; } = default!;

    /// <summary>
    /// A brief tagline or motto for the site
    /// </summary>
    public string Tagline { get; set; } = default!;

    /// <summary>
    /// A short description of the site, recommended length between 60 and 220 characters
    /// </summary>
    [Display(Name = "Description")]
    [StringLength(220, ErrorMessage = "We recommend keeping your description between {2} and {1} characters.", MinimumLength = 60)]
    public string? Description { get; set; } = default!;

    /// <summary>
    /// Path or URL for the site icon (favicon)
    /// </summary>
    public string? IconUrl { get; set; }

    /// <summary>
    /// Path or URL for the site logo
    /// </summary>
    public string? LogoUrl { get; set; }

    /// <summary>
    /// The base URL for the site (new field for easier site configuration)
    /// </summary>
    [Url(ErrorMessage = "Please provide a valid URL.")]
    public string? SiteAddress { get; set; }

    /// <summary>
    /// Email address of the site administrator
    /// </summary>
    [EmailAddress(ErrorMessage = "Please provide a valid email address.")]
    public string? AdminEmail { get; set; }

    /// <summary>
    /// Specifies whether anyone can register for the site without an invitation
    /// </summary>
    public bool AllowUserRegistration { get; set; } = false;

    /// <summary>
    /// Default role assigned to new users, represented by an integer (e.g., 1 = Subscriber, 2 = Editor)
    /// </summary>
    public int DefaultUserRole { get; set; }

}

/// <summary>
/// Email Settings
/// </summary>
public class EmailSettings
{
    /// <summary>
    /// The username for SMTP authentication
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// The password for SMTP authentication
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// The SMTP server host address
    /// </summary>
    public string Host { get; set; } = string.Empty;

    /// <summary>
    /// The port number for the SMTP server
    /// </summary>
    public int Port { get; set; } = 587; // Common default port for SMTP

    /// <summary>
    /// Indicates whether to use SSL for the connection
    /// </summary>
    public bool UseSSL { get; set; } = true; // Default to true for security
}

/// <summary>
/// Content Settings such as writing and reading settings
/// </summary>
public class ContentSettings
{
    /// <summary>
    /// Defines the type of content displayed on the homepage (e.g., "posts", "page")
    /// </summary>
    public string HomePageContentType { get; set; } = "posts";

    /// <summary>
    /// The static page to be used as the homepage if HomePageContentType is set to "page"
    /// </summary>
    public string? StaticHomePageUrl { get; set; }

    /// <summary>
    /// URL of the posts page (only applicable if a static home page is set)
    /// </summary>
    //public string? PostsPageUrl { get; set; }

    /// <summary>
    /// The number of posts or items to display per page (for pagination)
    /// </summary>
    public int ItemsPerPage { get; set; } = 10;

    /// <summary>
    /// Indicates whether the site should be indexed by search engines
    /// </summary>
    public bool SearchEngineVisibility { get; set; } = true;

    /// <summary>
    /// The default category ID assigned to new posts or pages
    /// </summary>
    public int? DefaultCategoryId { get; set; }

    /// <summary>
    /// URL or path to the default cover image used for posts or pages
    /// </summary>
    public string? DefaultCoverImageUrl { get; set; }

    /// <summary>
    /// Enable comments by default on new posts or pages
    /// </summary>
    public bool EnableCommentsByDefault { get; set; } = true;

    /// <summary>
    /// Maximum number of featured posts displayed on the homepage based on theme
    /// </summary>
    public int MaxFeaturedPosts { get; set; } = 5;

    /// <summary>
    /// Maximum number of revisions allowed per post. 
    /// Set to 0 for unlimited revisions.
    /// </summary>
    public int MaxPostRevisions { get; set; } = 0;

    /// <summary>
    /// Set whether pagination uses numbered pages or 'load more' (infinite scroll) based on theme
    /// </summary>
    public bool UseInfiniteScroll { get; set; } = false;
}

/// <summary>
/// Media upload settings
/// </summary>
public class MediaSettings
{
    // Settings for generating thumbnails
    //public bool GenerateThumbnails { get; set; } = true; // Default to true if required

    // Settings for resizing images
    //public bool ResizeImages { get; set; } = true; // Default to true if required

    /// <summary>
    /// Sizes for Small media
    /// </summary>
    public MediaSize SmallSize { get; set; } = new MediaSize { MaxHeight = 150, MaxWidth = 150 };
    /// <summary>
    /// Sizes for Medium media
    /// </summary>
    public MediaSize MediumSize { get; set; } = new MediaSize { MaxHeight = 500, MaxWidth = 500 };
    /// <summary>
    /// Sizes for Large media
    /// </summary>
    public MediaSize LargeSize { get; set; } = new MediaSize { MaxHeight = 1024, MaxWidth = 1024 };

    /// <summary>
    /// Indicates whether to optimize images
    /// </summary>
    public bool OptimizeImages { get; set; } = true;

    /// <summary>
    /// Quality level for optimized images (between 10 and 100)
    /// </summary>
    [Range(10, 100, ErrorMessage = "The value should be between 10 and 100.")]
    public int? OptimizedQuality { get; set; } = 75;

    /// <summary>
    /// Indicates whether to organize uploaded files into folders
    /// </summary>
    public bool OrganizeUploads { get; set; } = true;

}

public class MediaSize
{
    public int MaxWidth { get; set; } = default!;
    public int MaxHeight { get; set; } = default!;
}

/// <summary>
/// Comment settings
/// </summary>
public class CommentSettings
{
    /// <summary>
    /// Indicates whether the user must be logged in to post a comment
    /// </summary>
    public bool RequireLogin { get; set; } = false;

    /// <summary>
    /// Specifies whether an email is required for commenting.
    /// If false, users will not be prompted to provide an email, and the comment will be marked as anonymous.
    /// </summary>
    public bool RequireEmailForCommenting { get; set; } = true;

    /// <summary>
    /// Specifies whether comments need to be manually approved by moderators/admins
    /// </summary>
    public bool RequireManualApproval { get; set; } = false;

    /// <summary>
    /// URL or path to the default avatar image for commenters
    /// </summary>
    public string DefaultAvatarUrl { get; set; } = string.Empty;

    /// <summary>
    /// Number of initial comments to show per post before loading additional threads in separate pages
    /// </summary>
    public int InitialCommentsPerPost { get; set; } = 10;

    /// <summary>
    /// Time limit for editing a comment after it is posted (in minutes)
    /// </summary>
    public int CommentEditWindowMinutes { get; set; } = 15;

    /// <summary>
    /// Maximum depth for threaded/nested comments
    /// </summary>
    public int MaxThreadDepth { get; set; } = 3;
}
