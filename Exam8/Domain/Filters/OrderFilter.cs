namespace Domain.Filters;

public class OrderFilter
{
    public int? CustomerId  { get; set; }
    public int? From { get; set; }
    public int? To { get; set; }
    public int PageSize { get; set; }
    public int PagesNum { get; set; }
}