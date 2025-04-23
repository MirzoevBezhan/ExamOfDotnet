using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Category
{
    [Key]
    public int Id { get; set; }
    [MaxLength(40)]
    public string Name { get; set; }
    public virtual List<Product> Products { get; set; }
}
