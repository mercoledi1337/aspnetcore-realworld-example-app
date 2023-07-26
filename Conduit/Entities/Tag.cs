namespace Conduit.Entities
{
    public class Tag
    {
        public string? TagId { get; set; }
        public virtual List<ArticleTag> ArticleTags { get; set;} = new();
    }
}
