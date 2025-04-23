using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Product
{
    [Key]
    public int Id { get; set; }
    [MaxLength(60)]
    public string Name { get; set; }
    [Required]
    public decimal Price { get; set; }
    public int QuantityInStock { get; set; }
    public int CategoryId { get; set; }
    public int SupplierId { get; set; }
    public virtual Category Category { get; set; }
    public virtual Supplier Supplier { get; set; }
    public virtual List<Sale> Sales { get; set; }
    public virtual List<StockAdjustment> StockAdjustments { get; set; }
}