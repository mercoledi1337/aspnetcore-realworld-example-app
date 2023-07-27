namespace Conduit.Entities
{
    public class Tag
    {
        public Guid TagId { get; set; }
        public string Name { get; set; }
        private ICollection<Article> _articles = new List<Article>();
        public IEnumerable<Article> Articles => _articles;

    }
}
