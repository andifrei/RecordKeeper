using Microsoft.EntityFrameworkCore;

namespace RecordKeeper.Models
{
    public class RecordKeeperContext : DbContext
    {
        public RecordKeeperContext (DbContextOptions<RecordKeeperContext> options)
            : base(options)
        {
        }

        public DbSet<RecordKeeper.Models.RecordItem> RecordItem { get; set; }
    }
}