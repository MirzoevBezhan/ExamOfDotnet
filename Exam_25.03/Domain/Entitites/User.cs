using System.Data.Common;

namespace Domain.Entitites;

public class User
{
    public int id { get; set; }
    public string username { get; set; }
    public string email { get; set; }
    public string passwordHash { get; set; }
    public string bio { get; set; }
    public DateTime createdAt { get; set; }
}
