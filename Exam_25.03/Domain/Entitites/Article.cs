namespace Domain.Entitites;

public class Article
{
    public int id { get; set; }
    public int userId { get; set; }
    public string title { get; set; }
    public string content { get; set; }
    public string decription { get; set; }
    public DateTime createdAt { get; set; }
    public string status { get; set; }
}
