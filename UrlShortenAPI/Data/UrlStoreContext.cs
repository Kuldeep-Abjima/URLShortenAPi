using Microsoft.EntityFrameworkCore;

namespace UrlShortenAPI.Data
{
    public class UrlStoreContext : DbContext
    {
        public UrlStoreContext(DbContextOptions<UrlStoreContext> options) : base(options)
        
        { 
        
        
        
        
        }

        public DbSet<UrlTable> urls { get; set; }
      
    }
}
