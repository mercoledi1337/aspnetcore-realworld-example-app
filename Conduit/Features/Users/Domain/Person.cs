using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Conduit.Features.Users.Domain
{
    public class Person
    {
        public int Id { get; private set; }
        public string? Username { get; private set; }
        public string? Email { get; private set; }
        public string? Bio { get; private set; }
        public string? Image { get; private set; }
        public byte[] PasswordHash { get; private set; } = new byte[32];
        public byte[] PasswordSalt { get; private set; } = new byte[32];
        public string? Role { get; private set; } = "nieadmin";
        // here we need list
        public string? Followed { get; private set; }
        // here we need list
        public string? Followers { get; private set; }
        // here we need list
        public string? FavoriteArticles { get; private set; }
        //todo sprawdzanie email itp
        //mail ma pl image musi coś mieć
        //wszystko na private setter /check
        //sprawdzanie mappingu czy działa /nie działa, najpierw trzeba metode do tworzenia?
        //wyizolwać ustawienie maila do metody prywatnej
        //utworzyć prywatny konstruktor
        //utworzyć publiczną metode statyczną do tworzenia tego obiektu faktory method
        //dodać metode zmień mail
        //metoda do zmiany bio z walidacją długości jak jest złamana rzuć exception
        //put i obsługa w middleware
        private void ChangeMail(string Email)
        {
            string tmp = Email;
            var extension = tmp.Split(new string[] { "." }, StringSplitOptions.None);
            if (extension[extension.Length - 1] == "pl")
                Image = "FlagaPolski";
        }
        private void ChangeBio(string Bio)
        {
            if (Bio.Length > 10)
                throw new ArgumentException("za długie");
            this.Bio = Bio;
        }
        public static Person CreatePerson(string Username, string Email, byte[] Hash, byte[] Salt, string Bio)
        {

            Person person = new(Username, Email, Hash, Salt);
            person.ChangeBio(Bio);
            person.ChangeMail(Email);
            //przy rejestracji jest zrobione statyczne bio do sprawdzania czy działa
            return person;
        }

        private Person(string Username, string Email, byte[] Hash, byte[] Salt)
        {
            this.Username = Username;
            this.Email = Email;
            PasswordHash = Hash;
            PasswordSalt = Salt;
        }
        private Person()
        {
        }
    }
}
