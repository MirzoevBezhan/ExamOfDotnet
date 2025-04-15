using System.ComponentModel.DataAnnotations;

namespace Domain.Entites;

public class Project
{
    [Key]
    public int id { get; set; }
    [MaxLength(50)]
    public string Name { get; set; }
    [MaxLength(200)]
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public List<Task> Tasks { get; set; }
}
