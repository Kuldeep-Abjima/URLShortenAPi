namespace UrlShortenAPI.Data
{
    public class UrlTable
    {
        public int Id { get; set; }

        public string OriginalUrl  { get; set; }

        public string ShortCode { get; set; }

        public string ShortUrl { get; set; }

        public DateTime CreatedDate { get; set; }


    }
}
