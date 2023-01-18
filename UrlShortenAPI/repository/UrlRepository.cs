using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System;
using System.Net.WebSockets;
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
    
           var condition = Uri.IsWellFormedUriString(LongUrl, UriKind.Absolute);

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
                    return url.ShortCode;
                }

                return originalUrl.ShortCode;
            }
            else
            {
                return "Url Not Valid";
            }


        }
        public async Task<string> RedirectingUrl(string shortCode)
        {

            var revert = await _context.urls.Where(x => x.ShortCode == shortCode).Select(x => x.OriginalUrl).FirstOrDefaultAsync();



            if (revert != null)
            {

                return revert;
            }
            else
            {
                return "Not found";
            }

        }

       public async Task<int> CountIncrement(string longurl)
       {
            string cs = _configuration.GetConnectionString("UrlDB");
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("CountIncrement", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@longurl", longurl);
                con.Open();
                cmd.ExecuteReader();
                con.Close();
            }
            await _context.SaveChangesAsync();
            var count = await _context.urls.Where(x => x.OriginalUrl == longurl).Select(x => x.ShortLinkCount).FirstOrDefaultAsync();
            return count;
        }

    }
}
