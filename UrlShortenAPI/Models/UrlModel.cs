using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace UrlShortenAPI.Models
{
    public class UrlModel
    { 

        [Required]
        [NotNull]
        public string OriginalUrl { get; set; }

        public string ShortCode { get; set; }

        public string ShortUrl { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
