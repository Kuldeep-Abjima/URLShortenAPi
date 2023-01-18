using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Net;
using UrlShortenAPI.Data;
using UrlShortenAPI.repository;

namespace UrlShortenAPI.Controllers
{
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly IUrlRepsitory _urlRepsitory;
        public UrlController(IUrlRepsitory urlRepsitory)
        {
            _urlRepsitory = urlRepsitory;
        }
        [Route("api/[controller]")]
        [HttpPost]
        public async Task<IActionResult> AddUrl(string originalUrl)
        {
            var ShortUrl = await _urlRepsitory.UrlShortCodeGenerate(originalUrl);
            return Ok(ShortUrl);
        }
        
        [HttpGet]
        [Route("{shortcode}")]
        public async Task<IActionResult> revertUrl(string shortcode)
        {

            var reverting = await _urlRepsitory.RedirectingUrl(shortcode);
            var count = await _urlRepsitory.CountIncrement(reverting);
            
            return RedirectPermanent(reverting);
            
        }
        //[HttpGet("{shorturl}")]
        //public async Task<IActionResult> TotalCount(string shorturl)
        //{


        //    var result = await _urlRepsitory.TotalCount(shorturl);
        //    return Ok(result);

        //}


    }
}
