using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using commentingAndReviewSystem.Domain.Entities; 

namespace ReviewManagementSystem.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public DbSet<Review> Reviews { get; set; }
        //public DbSet<User> Users { get; set; }
        
        public DbSet<ContentItem> ContentItems { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Product> Products { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define the relationship between Review and User
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Define the relationship between Review and Product
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Define the relationship between Product and Comment (if needed)
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Product)
                .HasForeignKey(c => c.ProductId);
        }

    }
}
