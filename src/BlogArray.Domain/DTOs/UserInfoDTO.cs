using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogArray.Domain.DTOs;

public class BasicUserInfo
{
    public int Id { get; set; }

    public required string Username { get; set; }

    public string? DisplayName { get; set; }

    public required string Email { get; set; }

}

public class BasicUserInfoBio : BasicUserInfo
{

    public string? Bio { get; set; }

}

public class BasicUserInfoRole : BasicUserInfo
{
    public string? Role { get; set; }
}

public class UserInfo : BasicUserInfoBio
{

    public bool EmailConfirmed { get; set; }

    public bool LockoutEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public int RoleId { get; set; }

    public string? Role { get; set; }

}

public class CreateUser : UserInfo
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Enter a strong password")]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}

public class EditUserInfo : UserInfo
{
    [DisplayName("Set new password")]
    public bool ChangePassword { get; set; }

    [DataType(DataType.Password)]
    public string? Password { get; set; }

}
