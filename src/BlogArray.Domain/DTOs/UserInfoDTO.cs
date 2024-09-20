using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using UoN.ExpressiveAnnotations.NetCore.Attributes;

namespace BlogArray.Domain.DTOs;

public class BasicUserInfo
{

    public required string UserName { get; set; }

    public string? DisplayName { get; set; }

    public required string Email { get; set; }

    public string? Bio { get; set; }
}

public class UserInfo : BasicUserInfo
{
    public int Id { get; set; }

    public bool EmailConfirmed { get; set; }

    public bool LockoutEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public string? Role { get; set; }

}

public class CreateUser : UserInfo
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Enter a strong password")]
    [DataType(DataType.Password)]
    public required string Password { get; set; }

    public int RoleId { get; set; }
}

public class EditUserInfo : UserInfo
{
    [DisplayName("Set new password")]
    public bool ChangePassword { get; set; }

    [RequiredIf("ChangePassword", AllowEmptyStrings = false, ErrorMessage = "Enter a strong password")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

}
