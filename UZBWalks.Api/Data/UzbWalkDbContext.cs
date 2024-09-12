using Microsoft.EntityFrameworkCore;
using UZBWalks.Api.Models.Domain;

namespace UZBWalks.Api.Data
{
    public class UzbWalkDbContext : DbContext
    {
        public UzbWalkDbContext(DbContextOptions<UzbWalkDbContext> options):base(options)
        { }

        public DbSet<Walk> Walks { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
    }
}
