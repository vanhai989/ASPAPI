using CRUDApi.Models.AuthModels;
using Microsoft.EntityFrameworkCore;

namespace CRUDApi.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {}
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefeshTokens {get; set;}

        // cấu hình key giữa user với refeshToken
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RefreshToken>().HasOne(e => e.User).WithMany(e => e.RefeshTokens).HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.ClientCascade);
            base.OnModelCreating(modelBuilder);
        }
    }
}
