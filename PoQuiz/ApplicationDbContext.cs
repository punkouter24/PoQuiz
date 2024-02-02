using Microsoft.EntityFrameworkCore;
using PoQuiz.Models;

namespace PoQuiz
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<HighScore> HighScores { get; set; }
        public DbSet<NbaAllStarGame> NbaAllStarGames { get; set; } // Add this line

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // If you need to configure the key explicitly:
            _ = modelBuilder.Entity<NbaAllStarGame>().HasKey(n => n.Id);
        }
    }
}
