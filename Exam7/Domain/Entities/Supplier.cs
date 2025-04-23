using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Supplier
{
    [Key] 
    public int Id { get; set; }
    [MaxLength(50)] 
    public string Name { get; set; }
    [MaxLength(20)] 
    public string Phone { get; set; }
    public virtual List<Product> Products { get; set; }
}