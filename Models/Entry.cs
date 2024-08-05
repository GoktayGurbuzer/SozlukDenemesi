using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sozluk42.Models
{
    public class Entry
    {
        [Key]
        public int EntryId { get; set; }

        [Required]
        public string Content { get; set; }

        [ForeignKey("Title")]
        public int TitleId { get; set; }

        public Title Title { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }

        public ICollection<Comment> Comments { get; set; }

        // Eğer Likes ilişkisini eklemek istiyorsanız
        public ICollection<Like> Likes { get; set; }
    }
}
