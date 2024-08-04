using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sozluk42.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        [Required]
        public string? Content { get; set; }
        [Required]
        public int EntryId { get; set; }
        [ForeignKey("EntryId")]
        public Entry? Entry { get; set; }
        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
