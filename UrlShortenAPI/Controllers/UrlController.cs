using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UrlShortenAPI.repository;

namespace UrlShortenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly IUrlRepsitory _urlRepsitory;
        public UrlController(IUrlRepsitory urlRepsitory)
        {
            _urlRepsitory = urlRepsitory;
        }

        [HttpPost]
        public async Task<IActionResult> AddUrl(string originalUrl)
        {
            var ShortUrl = await _urlRepsitory.UrlShortCodeGenerate(originalUrl);
            return Ok(ShortUrl);
        }

        [HttpGet("")]
        public async Task<IActionResult> revertUrl(string shorturl)
        {
           
            var reverting = await _urlRepsitory.RedirectingUrl(shorturl);
            return Redirect(reverting);
        }
    }
}
