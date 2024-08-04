using System.ComponentModel.DataAnnotations;

namespace Sozluk42.Models
{
    public class CreateEntryModel
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public int TitleId { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
