namespace Domain.Entitites;

public class Comment
{
    public int id { get; set; }
    public int articleId { get; set; }
    public int userId { get; set; }
    public string content { get; set; }
    public DateTime createdAt { get; set; }
}
