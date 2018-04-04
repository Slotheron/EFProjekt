using Game.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Game.Data
{
    public class GameContext : DbContext
    {
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<SpecialMove> Moves { get; set; }


        public static readonly LoggerFactory MovieLoggerFactory
            = new LoggerFactory(new[] {
            new ConsoleLoggerProvider((category, level)
                => category == DbLoggerCategory.Database.Command.Name
                && level == LogLevel.Information, true)
            });

       protected override void OnModelCreating(ModelBuilder modelBuilder)
       {
            modelBuilder.Entity<PlayerMatch>().HasKey(p => new { p.PlayerId, p.MatchId });
       }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .EnableSensitiveDataLogging()
                .UseLoggerFactory(MovieLoggerFactory)
                .UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = GameTournyDb; Trusted_Connection = True;");
        }
    }
}
