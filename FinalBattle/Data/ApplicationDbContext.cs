using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FinalBattle.Models;

namespace FinalBattle.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Backing> Backings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<PlacePhoto> PlacePhotos { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<SongAuthor> SongAuthors { get; set; }
        public DbSet<SongCategory> SongCategories { get; set; }
        public DbSet<Place> Places { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<PlacePhoto>()
            .HasKey(c => new { c.PlaceID, c.PhotoID });

            builder.Entity<SongAuthor>()
            .HasKey(c => new { c.SongID, c.AuthorID });

            builder.Entity<SongCategory>()
            .HasKey(c => new { c.SongID, c.CategoryID });

            builder.Entity<ApplicationUser>()
            .HasMany(e => e.Roles)
            .WithOne()
            .HasForeignKey(e => e.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }
    }
}
