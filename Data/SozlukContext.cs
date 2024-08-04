using Microsoft.EntityFrameworkCore;
using Sozluk42.Models;

namespace Sozluk42.Data
{
    public class SozlukContext : DbContext
{
    public SozlukContext(DbContextOptions<SozlukContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Title> Titles { get; set; }
    public DbSet<Entry> Entries { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Like> Likes { get; set; }
}
}
