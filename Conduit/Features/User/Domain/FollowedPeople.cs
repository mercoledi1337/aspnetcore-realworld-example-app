namespace Conduit.Features.User.Domain
{
    public class FollowedPeople
    {
        public int Id { get; set; }
        public User? User { get; set; }

        public int FollowedId { get; set; }
        public User? Followed { get; set; }
    }
}
