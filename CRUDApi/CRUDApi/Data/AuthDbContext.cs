using CRUDApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApi.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {}
        public DbSet<User> Users { get; set; }
        public DbSet<RefeshToken> RefeshTokens {get; set;}

        // cấu hình key giữa user với refeshToken
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RefeshToken>().HasOne(e => e.User).WithMany(e => e.RefeshTokens).HasForeignKey(e => e.userId).OnDelete(DeleteBehavior.ClientCascade);
            base.OnModelCreating(modelBuilder);
        }
    }
}
