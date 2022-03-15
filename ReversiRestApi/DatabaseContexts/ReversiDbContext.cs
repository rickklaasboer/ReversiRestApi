using System;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ReversiRestApi.DatabaseContexts
{
    public class ReversiDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new SqlConnectionStringBuilder
            {
                {"Server", "127.0.0.1"},
                {"Database", "ReversiRestApi"},
                {"User Id", "sa"},
                {"Password", "SuperSecretP@ssw0rd!"},
                {"TrustServerCertificate", true},
                {"Trusted_Connection", false},
                {"MultipleActiveResultSets", true},
                {"Encrypt", false}
            };

            optionsBuilder.UseSqlServer(builder.ConnectionString);
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Convert the PlayerTurn enumeration
            modelBuilder
                .Entity<Game>()
                .Property(s => s.PlayerTurn)
                .HasConversion(
                    s => s.ToString(), 
                    s => (Color)Enum.Parse(typeof(Color), s)); 

            // Convert the 2D Bord array
            modelBuilder
                .Entity<Game>()
                .Property(s => s.Board)
                .HasConversion(
                    bord => JsonConvert.SerializeObject(bord),
                    bord => JsonConvert.DeserializeObject<Color[,]>(bord));
        }

    }
}