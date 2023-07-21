namespace Conduit.Features.Users.Application.Dto
{
    public class ProfileDto
    {
        public string? Email { get; set; }
        public string? Token { get; set; }
        public string? Username { get; set; }
        public string? Bio { get; set; }
        public string? Image { get; set; }
    }

    public record ProfileEnvelope(ProfileDto profile);
}
