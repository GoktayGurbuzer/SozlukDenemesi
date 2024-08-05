using Microsoft.EntityFrameworkCore;
using Sozluk42.Models;

namespace Sozluk42.Data
{
    public class SozlukContext : DbContext
    {
        public SozlukContext(DbContextOptions<SozlukContext> options)
            : base(options)
        {
        }

        public DbSet<Entry> Entries { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; } // Burada Likes ekliyoruz

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entry>()
                .HasOne(e => e.User)
                .WithMany(u => u.Entries)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Entry>()
                .HasOne(e => e.Title)
                .WithMany(t => t.Entries)
                .HasForeignKey(e => e.TitleId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Entry)
                .WithMany(e => e.Comments)
                .HasForeignKey(c => c.EntryId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Like>()
                .HasOne(l => l.Entry)
                .WithMany(e => e.Likes)
                .HasForeignKey(l => l.EntryId);

            modelBuilder.Entity<Like>()
                .HasOne(l => l.User)
                .WithMany(u => u.Likes)
                .HasForeignKey(l => l.UserId);
        }
    }
}
