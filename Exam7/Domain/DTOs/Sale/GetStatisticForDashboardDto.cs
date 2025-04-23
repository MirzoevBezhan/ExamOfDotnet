namespace Domain.DTOs.Sale;

public class GetStatisticForDashboardDto
{
    public int TotalProduct { get; set; }
    public decimal TotalRevenue { get; set; }
    public int TotalSales { get; set; }
}