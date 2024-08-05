namespace Sozluk42.Models
{
    public class Comment
{
    public int CommentId { get; set; }
    public string Content { get; set; }
    public int EntryId { get; set; }
    public Entry Entry { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}

}
