using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Customer
{
    [Key]
    public int Id { get; set; }
    [MaxLength(150)]
    public string FullName { get; set; }
    [MaxLength(20)]
    public string Phone { get; set; }
    [MaxLength(150)]
    [EmailAddress]
    public string Email { get; set; }
   
    public virtual List<Rental> Rentals { get; set; }
}