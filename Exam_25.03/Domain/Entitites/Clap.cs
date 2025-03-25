namespace Domain.Entitites;

public class Clap
{
    public int id { get; set; }
    public int articleId { get; set; }
    public int userId { get; set; }
    public int count { get; set; }
    public DateTime createdAt { get; set; }
}
