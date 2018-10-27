using ElearnerApi.Data.Models;
using ElearnerApi.Data.Models.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ElearnerApi.Data.DataContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly string serverName = "localhost";
        private readonly string databaseName = "ElearnerApiDb";
        private readonly string databaseUser = "root";
        private readonly string databasePass = "password";

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
            var connectionString
                = $"Server={serverName};" +
                    $"database={databaseName};" +
                        $"uid={databaseUser};" +
                            $"pwd={databasePass};" +
                                $"pooling=true;";

            optionsBuilder.UseMySql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}