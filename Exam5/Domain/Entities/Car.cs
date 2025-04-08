using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Car
{
    [Key]
    public int id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Model { get; set; }
    [Required]    
    public decimal PricePerDay { get; set; }
    public bool IsAvailable { get; set; } = true;

    public List<Booking> Bookings { get; set; }
}
