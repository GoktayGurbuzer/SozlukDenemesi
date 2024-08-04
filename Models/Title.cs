using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sozluk42.Models
{
    public class Title
    {
        [Key]
        public int TitleId { get; set; }
        public string Name { get; set; }

        public ICollection<Entry> Entries { get; set; } = new List<Entry>(); // Zorunlu değil
    }
}
