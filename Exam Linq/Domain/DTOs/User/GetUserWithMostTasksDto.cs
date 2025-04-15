namespace Domain.DTOs.User;

public class GetUserWithMostTasksDto : GetUserDto
{
    public int TasksCount { get; set; }
}
