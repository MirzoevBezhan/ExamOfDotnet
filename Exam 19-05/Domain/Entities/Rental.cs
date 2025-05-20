using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Rental
{
    [Key]
    public int Id { get; set; }
    public int CarId { get; set; }
    public int CustomerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalCost { get; set; }
    
    public virtual Car Car { get; set; }
    public virtual Customer Customer { get; set; }
}
