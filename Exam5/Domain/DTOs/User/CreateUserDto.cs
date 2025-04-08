namespace Domain.DTOs.User;

public class CreateUserDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int BookingCount { get; set; }
}
