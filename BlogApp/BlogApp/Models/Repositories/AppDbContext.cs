using BlogApp.Migrations;
using BlogApp.Models.Repositories.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
namespace BlogApp.Models.Repositories
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, AppRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

           
            modelBuilder.Entity<Blog>()
          .HasOne(b => b.User)
          .WithMany(u => u.Blogs)
          .HasForeignKey(b => b.UserId)
          .OnDelete(DeleteBehavior.Cascade); 

            // Blog - Category ilişkisi (Many-to-One)
            modelBuilder.Entity<Blog>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Blogs)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            // Blog entity configuration
            modelBuilder.Entity<Blog>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Title)
                      .HasMaxLength(200)
                      .IsRequired();

                entity.Property(e => e.Content)
                      .IsRequired();

                entity.Property(e => e.PublishDate)
                      .IsRequired();

                entity.Property(e => e.ImageUrl)
                      .HasMaxLength(500);

                entity.HasOne(b => b.Category)
                      .WithMany(c => c.Blogs)
                      .HasForeignKey(b => b.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict); // İsteğe bağlı olarak .Cascade olabilir
            });

            // Category
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                      .HasMaxLength(100)
                      .IsRequired();
            });
        }
    }
}


