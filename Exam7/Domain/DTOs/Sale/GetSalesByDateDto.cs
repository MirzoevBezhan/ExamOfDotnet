namespace Domain.DTOs.Sale;

public class GetSalesByDateDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int QuantitySold { get; set; }
    public DateTime SaleDate { get; set; }
}
