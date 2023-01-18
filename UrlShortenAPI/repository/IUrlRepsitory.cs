using UrlShortenAPI.Models;

namespace UrlShortenAPI.repository
{
    public interface IUrlRepsitory
    {
        Task<string> UrlShortCodeGenerate(string LongUrl);
        Task<string> RedirectingUrl(string shortCode);
        Task<int> CountIncrement(string longurl);

        //Task<int> TotalCount(string shortUrl);


    }
}
