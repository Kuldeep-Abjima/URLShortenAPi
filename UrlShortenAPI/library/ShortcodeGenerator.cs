using UrlShortenAPI.Data;

namespace UrlShortenAPI.library
{
    public class ShortcodeGenerator
    {
        
        public static string ShortCode()
        {
            Guid guid = Guid.NewGuid();
            string shortCode = $"{guid.ToString().Substring(0, 4)}" + $"{guid.ToString().Substring(15, 1)}"+ $"{guid.ToString().Substring(25, 2)}";
            return shortCode;
        }
    }
}
