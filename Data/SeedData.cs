using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sozluk42.Models;
using System;
using System.Linq;

namespace Sozluk42.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SozlukContext(
                serviceProvider.GetRequiredService<DbContextOptions<SozlukContext>>()))
            {
                // Look for any users.
                if (context.Users.Any())
                {
                    return;   // DB has been seeded
                }

                // Add demo user
                var demoUser = new User
                {
                    Username = "demoUser",
                    Email = "demo@user.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("password")
                };
                context.Users.Add(demoUser);
                context.SaveChanges();

                // Add some titles
                var titles = new[]
                {
                    new Title { Name = "Technology" },
                    new Title { Name = "Science" },
                    new Title { Name = "Movies" }
                };
                context.Titles.AddRange(titles);
                context.SaveChanges();

                // Add some entries
                var entries = new[]
                {
                    new Entry { Content = "This is an entry about technology.", TitleId = titles[0].TitleId, UserId = demoUser.UserId },
                    new Entry { Content = "Science is fascinating.", TitleId = titles[1].TitleId, UserId = demoUser.UserId },
                    new Entry { Content = "Have you seen the latest movie?", TitleId = titles[2].TitleId, UserId = demoUser.UserId }
                };
                context.Entries.AddRange(entries);
                context.SaveChanges();
            }
        }
    }
}
