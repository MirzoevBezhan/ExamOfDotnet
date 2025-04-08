using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Booking
{
    [Key]
    public int id { get; set; }
    public int UserId { get; set; }
    public int CarId { get; set; }
    [Required]
    public DateTimeOffset StartDate { get; set; }
    [Required]
    public DateTimeOffset EndDate { get; set; }
    public decimal TotalPrice { get; set; }

    public User User { get; set; }
    public Car Car { get; set; }
}
