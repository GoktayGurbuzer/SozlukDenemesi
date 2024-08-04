using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Sozluk42.Models
{
    public class Entry
    {
        [Key]
        public int EntryId { get; set; }
        
        [Required]
        public string Content { get; set; }

        [Required]
        public int TitleId { get; set; }

        [JsonIgnore]
        public Title Title { get; set; }

        [Required]
        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }
    }
}
