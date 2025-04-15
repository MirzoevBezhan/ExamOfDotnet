namespace Domain.DTOs.Project;

public class GetProjectWithMostTasksDto : GetProjectDto
{
    public int TasksCount { get; set; }
}
