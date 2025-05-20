using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Car
{
    [Key]
    public int Id { get; set; }
    public int BranchId { get; set; }
    [MaxLength(100)]
    public string Model { get; set; }
    [MaxLength(100)]
    public string Manufacturer { get; set; }
    public int Year { get; set; }
    public decimal PricePerDay { get; set; }
    
    public virtual List<Rental> Rentals { get; set; }
    public virtual Branch Branch { get; set; }
}
