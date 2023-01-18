using UrlShortenAPI.Data;

namespace UrlShortenAPI.library
{
    public class ShortcodeGenerator
    {
        
        public static string ShortCode()
        {
            Random random = new Random();
            string alpha = "abcdefghijklmnopqrstuvwxyz";
            int num = 2;
            string sc = "";
            for(int i = 0; i < num; i++)
            {
                int x = random.Next(26);
                sc = sc + alpha[x];
            }
            Guid guid = Guid.NewGuid();
            string shortCode = $"{guid.ToString().Substring(0, 2)}" +$"{guid.ToString().Substring(10,1)}" + $"{sc}"+ $"{guid.ToString().Substring(25, 1)}";
            return shortCode;
        }
    }
}
