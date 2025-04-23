namespace Domain.DTOs.Supplier;

public class GetSupplierWithProductsDto
{
    public int SupplierId { get; set; }
    public string SupplierName { get; set; }
    public List<string> Products { get; set; }
}