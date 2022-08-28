using Microsoft.EntityFrameworkCore;
using NoteApi.Models.Entities;

namespace NoteApi
{
    public class AppDatabaseContext : DbContext
    {
        public AppDatabaseContext(DbContextOptions<AppDatabaseContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<PathWrapper> PathWrappers { get; set; }
        public DbSet<Point> Points { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

    }
}
