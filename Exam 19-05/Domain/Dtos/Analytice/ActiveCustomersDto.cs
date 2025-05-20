namespace Domain.DTOs.Analytics;

public class ActiveCustomersDto
{
    public string CustomerName { get; set; }
    public int RentalsCount { get; set; }
    public decimal TotalSpent { get; set; }
}