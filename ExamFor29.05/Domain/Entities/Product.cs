using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Product
{
    [Key] public int Id { get; set; }
    [Required] [StringLength(50)] public string Name { get; set; }
    [Required] 
    public decimal Price { get; set; } 
    
     public DateTime? CreatedAt { get; set; } = DateTime.Now;
}