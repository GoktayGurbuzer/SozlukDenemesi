namespace Sozluk42.Models
{
    public class Like
    {
        public int LikeId { get; set; }
        public int EntryId { get; set; }
        public Entry Entry { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
