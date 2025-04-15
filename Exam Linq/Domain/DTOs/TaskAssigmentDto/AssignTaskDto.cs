namespace Domain.DTOs.TaskAssigmentDto;

public class AssignTaskDto
{
    public int TaskId { get; set; }
    public int UserId { get; set; }
    public DateTime AssignedDate { get; set; }
}
