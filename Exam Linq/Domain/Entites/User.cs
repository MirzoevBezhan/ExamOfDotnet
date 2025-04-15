using System.ComponentModel.DataAnnotations;

namespace Domain.Entites;

public class User
{
    [Key]
    public int id { get; set; }
    [MaxLength(50)]
    public string Name { get; set; }
    [MaxLength(200)]
    public string Email { get; set; }
    public DateTime ReistrationDate { get; set; }

    public List<Task> Tasks { get; set; }
    public List<TaskAssignment> TaskAssignments  { get; set; }
}
