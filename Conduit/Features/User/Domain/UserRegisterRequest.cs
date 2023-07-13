using System.ComponentModel.DataAnnotations;

namespace Conduit.Features.User.Domain
{
    public class UserRegisterRequest
    {
        [Required]
        public string username { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string emailAddress { get; set; } = string.Empty;
        [Required, MinLength(6)]
        public string password { get; set; } = string.Empty;
    }
}
