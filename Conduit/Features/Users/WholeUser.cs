namespace Conduit.Features.Users
{
    public class WholeUser
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Bio { get; set; }
        public string? Image { get; set; }
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        // here we need list
        public string? Followed { get; set; }
        // here we need list
        public string? Followers { get; set; }
        // here we need list
        public string? FavoriteArticles { get; set; }

    }
}
