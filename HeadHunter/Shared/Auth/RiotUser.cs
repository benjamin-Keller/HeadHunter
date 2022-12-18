using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HeadHunter.Shared.Auth;

public class RiotUser
{
    [Required]
    public string Username { get; set; }
    [Required]
    [PasswordPropertyText(true)]
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}
