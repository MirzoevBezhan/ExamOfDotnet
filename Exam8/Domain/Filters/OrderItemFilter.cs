namespace Domain.Filters;

public class OrderItemFilter
{
    public int? From { get; set; }
    public int? To { get; set; }
    public int PageSize { get; set; }
    public int PagesNum { get; set; }
}