using System.ComponentModel.DataAnnotations;

namespace Conduit.Features.User.Domain
{
    public class UserLoginRequest
    {
        [Required, EmailAddress]
        public string EmailAddress {  get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
