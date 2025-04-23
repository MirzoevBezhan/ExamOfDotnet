using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class StockAdjustment
{
    [Key]
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int AdjustmentAmount { get; set; }
    public string Reason { get; set; }
    public DateTime AdjustmentDate { get; set; }
    public virtual Product Product { get; set; }
}
