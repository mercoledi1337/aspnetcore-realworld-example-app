using System.ComponentModel.DataAnnotations;

namespace Conduit.Features.Users
{
    public class UserLoginRequest
    {
        [Required, EmailAddress]
        public string EmailAddress { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
