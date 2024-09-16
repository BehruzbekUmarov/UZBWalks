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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("8255bba3-7613-4f1d-9d2d-3fa1da0f4117"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("d86b62d0-a0ff-4cca-b747-8104e23a2af7"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("fa0ea5c5-66ef-4f2d-985f-4606f40cda49"),
                    Name = "Hard"
                }
            };

            modelBuilder.Entity<Difficulty>().HasData(difficulties);
        }
    }
}
