using System.Text.Json.Serialization;
using static Conduit.Features.Users.Application.Update;

namespace Conduit.Entities
{
    public class Person
    {
        [JsonIgnore]
        public int Id { get; private set; }
        public string? Username { get; private set; }
        public string? Email { get; private set; }
        public string? Bio { get; private set; }
        public string? Image { get; private set; }
        [JsonIgnore]
        public byte[] PasswordHash { get; private set; } = new byte[32];
        [JsonIgnore]
        public byte[] PasswordSalt { get; private set; } = new byte[32];
        public string? Role { get; private set; } = "nieadmin";
        // here we need list
        [JsonIgnore]
        public string? Followed { get; private set; }
        // here we need list
        [JsonIgnore]
        public string? Followers { get; private set; }
        // here we need list
        [JsonIgnore]
        public string? FavoriteArticles { get; private set; }
        private void ChangeMail(string Email)
        {
            string tmp = Email;
            var extension = tmp.Split(new string[] { "." }, StringSplitOptions.None);
            if (extension[extension.Length - 1] == "pl")
                Image = "FlagaPolski";
        }
        private void ChangeBio(string Bio)
        {
            if (Bio.Length > 300)
                throw new ArgumentException("za długie");
            this.Bio = Bio;
        }
        public static Person CreatePerson(string username, string mail, byte[] hash, byte[] salt)
        {

            Person person = new(username, mail, hash, salt);
            person.ChangeMail(mail);
            return person;
        }
        //musi mieć ten obiek co aktualizuje plus hash/salt
        // znaleźć tą osobe co chcemu update
        public void UpdatePerson(UserUpdateRequest user, byte[] hash, byte[] salt)
        {
            Image = user.Image;
            Username = user.Username;
            ChangeBio(user.Bio);
            Email = user.Email;
            PasswordHash = hash;
            PasswordSalt = salt;
        }

        private Person(string Username, string Email, byte[] Hash, byte[] Salt)
        {
            this.Username = Username;
            this.Email = Email;
            PasswordHash = Hash;
            PasswordSalt = Salt;
        }
        public Person()
        {
        }
    }
}