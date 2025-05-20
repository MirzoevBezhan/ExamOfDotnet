
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Customer
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string FullName { get; set; }
    [Required]
    [MaxLength(20)]
    public string Phone { get; set; }

    public virtual List<Reservation> Reservations { get; set; }
}
