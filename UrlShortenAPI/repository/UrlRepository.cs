using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using UrlShortenAPI.Data;
using UrlShortenAPI.library;
using UrlShortenAPI.Models;

namespace UrlShortenAPI.repository
{
    public class UrlRepository : IUrlRepsitory
    {
        private readonly UrlStoreContext _context;
        private readonly IConfiguration _configuration;


        public UrlRepository(UrlStoreContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        public async Task<string> UrlShortCodeGenerate(string LongUrl)
        {

            var condition = LongUrl.StartsWith("Https:") || LongUrl.StartsWith("Http:") || LongUrl.StartsWith("http:") || LongUrl.StartsWith("https:");
            if (condition == true)
            {
                var originalUrl = await _context.urls.FirstOrDefaultAsync(x => x.OriginalUrl == LongUrl);
                if (originalUrl == null)
                {
                    var SystemUrl = _configuration.GetValue<string>("SystemUrl");
                    var shortcode = ShortcodeGenerator.ShortCode();
                    var url = new UrlTable
                    {
                        OriginalUrl = LongUrl,
                        ShortCode = shortcode,
                        ShortUrl = $"{SystemUrl}" + $"{shortcode}",
                        CreatedDate = DateTime.Now,
                    };
                    _context.urls.Add(url);
                    await _context.SaveChangesAsync();
                    return url.ShortUrl;
                }

                return originalUrl.ShortUrl;
            }
            else
            {
                return "Url Not Valid";
            }


        }
        public async Task<string> RedirectingUrl(string shortUrl)
        {
            var revert = await _context.urls.Where(x => x.ShortUrl == shortUrl).Select(x => x.OriginalUrl).FirstOrDefaultAsync();
            
            if(revert != null)
            {
             
                return revert;
            }
            else
            {
                return "Not found";
            }

        }

    }
}
