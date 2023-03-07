using System.ComponentModel.DataAnnotations;

namespace Store.Authentication.Shared.Core.RequestModels;

public class UserLoginRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; }

}