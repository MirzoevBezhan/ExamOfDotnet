namespace Domain.DTOs.User;

public class CreateUserDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime RegistrationDate { get; set; }
}
