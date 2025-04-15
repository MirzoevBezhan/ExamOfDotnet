using System.ComponentModel.DataAnnotations;

namespace Domain.Entites;

public class TaskAssignment
{
    [Key]
    public int id { get; set; }
    public int  TaskId { get; set; }
    public int  UserId { get; set; }
    public DateTime AssignedDate { get; set; }

    public Task Task { get; set; }
    public User User { get; set; }
}
