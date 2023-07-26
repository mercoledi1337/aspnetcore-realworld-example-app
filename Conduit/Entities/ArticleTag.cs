namespace Conduit.Entities
{
    public class ArticleTag
    {
        public string? TagId { get; set; }
        public virtual Tag? Tag { get; set; }
        public int ArticleId { get; set; }
        public virtual Article? Article { get; set; }
    }
}
