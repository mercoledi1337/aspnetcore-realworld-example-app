namespace Conduit.Entities
{
    public class ArticleTag
    {
        public string? TagId { get; set; }
        public Tag? Tag { get; set; }
        public int ArticleId { get; set; }
        public Article? Article { get; set; }
    }
}
