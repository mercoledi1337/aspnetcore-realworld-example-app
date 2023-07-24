namespace Conduit.Entities
{
    public class Tag
    {
        public string? TagId { get; set; }
        public List<ArticleTag> ArticleTags { get; set;} = new();
    }
}
