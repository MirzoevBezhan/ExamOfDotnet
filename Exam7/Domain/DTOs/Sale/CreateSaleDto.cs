namespace Domain.DTOs.Sale;

public class CreateSaleDto
{
    
    public int ProductId { get; set; }
    public int QuantitySold { get; set; }
    public DateTime StartDate { get; set; }

}