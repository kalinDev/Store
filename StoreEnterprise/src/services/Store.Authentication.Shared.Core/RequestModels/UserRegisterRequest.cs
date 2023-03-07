using System.ComponentModel.DataAnnotations;

namespace Store.Authentication.Shared.Core.RequestModels;

public class UserRegisterRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; }
    
    [Required]
    [Compare("Password")]
    [StringLength(100, MinimumLength = 6)]
    public string ConfirmPassword { get; set; }
}