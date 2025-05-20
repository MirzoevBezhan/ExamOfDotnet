namespace Domain.Filters;

public class RentalFilter
{
    public int? CarId { get; set; }
    public int? CustomerId { get; set; }
    
    public decimal From { get; set; }
    public decimal To { get; set; }

    public int PagesNumber { get; set; }
    public int PageSize { get; set; }
}