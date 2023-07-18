using System.ComponentModel.DataAnnotations;

namespace Conduit.Features.Users.Domain
{
    public class Person
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Bio { get; set; }
        public string? Image { get; set; }
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        public string? Role { get; set; } = "nieadmin";
        // here we need list
        public string? Followed { get; set; }
        // here we need list
        public string? Followers { get; set; }
        // here we need list
        public string? FavoriteArticles { get; set; }
        //todo sprawdzanie email itp
        //mail ma pl image musi coś mieć

        public Person(string email)
        {
            string tmp = email;
            var extension = tmp.Split(new string[] { "." }, StringSplitOptions.None);
            if (extension[extension.Length-1] == "pl")
                Image = "FlagaPolski";
        }
    }
}
