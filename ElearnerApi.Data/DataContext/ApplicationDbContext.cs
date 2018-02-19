using ElearnerApi.Data.Models;
using ElearnerApi.Data.Models.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ElearnerApi.Data.DataContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {
        }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Unit> Units { get; set; }

        public DbSet<LearningObject> LearningObjects { get; set; }

        public DbSet<Lesson> Lessons { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql( GetConnectionString( ) );
        }

        private static string GetConnectionString()
        {
            const string serverName = "localhost";
            const string databaseName = "Elearner";
            const string databaseUser = "root";
            const string databasePass = "";

            return $"Server={serverName};" +
                   $"database={databaseName};" +
                   $"uid={databaseUser};" +
                   $"pwd={databasePass};" +
                   $"pooling=true;";
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating( builder );
        }
    }
}