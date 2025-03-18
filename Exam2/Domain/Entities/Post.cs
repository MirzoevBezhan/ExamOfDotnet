namespace Domain.Entities;

public class Post
{
    public int PostId { get; set; }
    public int PostUserId { get; set; }
    public string Content { get; set; }
    public DateTime CreationDate { get; set; }
    public int LikesCount { get; set; }
    public int UserId { get; set; }
}
