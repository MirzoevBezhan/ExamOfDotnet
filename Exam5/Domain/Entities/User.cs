using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class User
{
    [Key]
    public int id { get; set; }
    [Required]
    [MaxLength(50)]
    public string Username { get; set; }
    [Required]
    [MaxLength(100)]
    public string Email { get; set; }
    [MaxLength(20)]
    public string Phone { get; set; }

    public List<Booking> Bookings { get; set; }
}
