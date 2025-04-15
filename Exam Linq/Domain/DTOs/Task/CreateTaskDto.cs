namespace Domain.DTOs;

public class CreateTaskDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public int ProjectId { get; set; }
    public int UserId { get; set; }
}
