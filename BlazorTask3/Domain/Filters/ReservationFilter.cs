namespace Domain.Filters;

public class ReservationFilter
{
    public int? TableId { get; set; }
    public int? CustomerId { get; set; }
    public int? From { get; set; }
    public int? To { get; set; }
    public int PagesNumber { get; set; }
    public int PageSize { get; set; }
}