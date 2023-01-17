using UrlShortenAPI.Models;

namespace UrlShortenAPI.repository
{
    public interface IUrlRepsitory
    {
        Task<string> UrlShortCodeGenerate(string LongUrl);
        Task<string> RedirectingUrl(string shortCode);

    }
}
