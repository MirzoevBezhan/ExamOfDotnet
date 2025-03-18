namespace Domain.Entities;

public class Comment
{
    public int CommentId { get; set; }
    public int CommenterUserId { get; set; }
    public int CommenterPostId { get; set; }
    public string Content { get; set; }
    public DateTime CreationDate { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }
}
