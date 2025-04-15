using System.ComponentModel.DataAnnotations;
namespace Domain.Entites;

public class Task
{
    [Key]
    public int id { get; set; }
    [MaxLength(100)]
    public string Title { get; set; }
    [MaxLength(200)]
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public int ProjectId { get; set; }
    public int UserId { get; set; }

   public List<TaskAssignment>  TaskAssignments { get; set; }
    public User User { get; set; }
    public Project Project { get; set; }
}
