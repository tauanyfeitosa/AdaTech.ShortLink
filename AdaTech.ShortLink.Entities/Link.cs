namespace AdaTech.ShortLink.Entities
{
    public class Link
    {
        public int Id { get; set; }
        public string OriginalUrl { get; set; }
        public string ShortUrl { get; set; }
        public DateTime Expiration { get; set; }
    }
}
