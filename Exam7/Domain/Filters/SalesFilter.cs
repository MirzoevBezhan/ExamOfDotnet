namespace Domain.Filters;

public class SalesFilter
{
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public int PagesNum { get; set; }
    public int PageSize { get; set; }
}