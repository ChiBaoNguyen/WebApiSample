using Microsoft.EntityFrameworkCore;
using WebAPISample.Entities;

namespace WebAPISample.EF
{
    public class SampleDbContext : DbContext
    {
        public SampleDbContext(DbContextOptions<SampleDbContext> options) : base(options)
        {
        }

        public DbSet<Sample> Samples { get; set; }
    }
}
