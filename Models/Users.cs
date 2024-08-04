using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sozluk42.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public ICollection<Entry> Entries { get; set; } // ICollection<Entry> türünde olmalı
    }
}
