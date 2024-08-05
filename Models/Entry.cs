using System.Collections.Generic;

namespace Sozluk42.Models
{
    public class Entry
    {
        public int EntryId { get; set; }
        public string Content { get; set; }
        public int TitleId { get; set; }
        public Title Title { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; } // Burada Likes ilişkisini ekliyoruz
    }
}
