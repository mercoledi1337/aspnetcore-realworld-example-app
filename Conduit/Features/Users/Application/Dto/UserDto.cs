using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Manage.Internal;
using System.Text.Json.Serialization;

namespace Conduit.Features.Users.Application
{
    public class UserDto
    {
        public string? Email { get; set; }
        public string? Token { get; set; }
        public string? Username { get; set; }
        public string? Bio { get; set; }
        public string? Image { get; set; }
    }

    public record UserEnvelope(UserDto User);
}
