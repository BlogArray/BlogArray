using System.ComponentModel.DataAnnotations;

namespace BlogArray.Domain.Entities;

public class AppUser : EntityBase
{
    /// <summary>
    /// Gets or sets the user name for this user.
    /// </summary>
    [Required]
    [MaxLength(64)]
    public required string Username { get; set; }

    /// <summary>
    /// Gets or sets a salted and hashed representation of the password for this user.
    /// </summary>
    [DataType(DataType.Password)]
    public string? PasswordHash { get; set; }

    /// <summary>
    /// Gets or sets the email address for this user.
    /// </summary>
    [Required]
    [MaxLength(256)]
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }

    /// <summary>
    /// Gets or sets a flag indicating if a user has confirmed their email address.
    /// </summary>
    /// <value>True if the email address has been confirmed, otherwise false.</value>
    public bool EmailConfirmed { get; set; }

    /// <summary>
    /// Gets or sets the display name for this user.
    /// </summary>
    [MaxLength(64)]
    public string? DisplayName { get; set; }

    /// <summary>
    /// Gets or sets the bio for this user.
    /// </summary>
    [MaxLength(512)]
    public string? Bio { get; set; }

    /// <summary>
    /// Gets or sets a flag indicating if two factor authentication is enabled for this user.
    /// </summary>
    /// <value>True if 2fa is enabled, otherwise false.</value>
    public bool TwoFactorEnabled { get; set; }

    /// <summary>
    /// Gets or sets the date and time, in UTC, when any user lockout ends.
    /// </summary>
    /// <remarks>
    /// A value in the past means the user is not locked out.
    /// </remarks>
    public DateTimeOffset? LockoutEnd { get; set; }

    /// <summary>
    /// Gets or sets a flag indicating if the user could be locked out.
    /// </summary>
    /// <value>True if the user could be locked out, otherwise false.</value>
    public bool LockoutEnabled { get; set; }

    /// <summary>
    /// Gets or sets the number of failed login attempts for the current user.
    /// </summary>
    public int AccessFailedCount { get; set; }

    /// <summary>
    /// Gets or sets the hashed representation of the security code, used for email confirmation, two-factor authentication (2FA), or account lockout.
    /// </summary>
    public string? SecurityCodeHash { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the security code was issued.
    /// </summary>
    public DateTime? SecurityCodeIssuedAt { get; set; }

    /// <summary>
    /// Gets or sets the number of times the security code has been issued.
    /// </summary>
    public int SecurityCodeIssueCount { get; set; }

    /// <summary>
    /// Gets or sets the identifier for the user's role.
    /// </summary>
    public int RoleId { get; set; }

    public AppRole Role { get; set; } = default!;
}
