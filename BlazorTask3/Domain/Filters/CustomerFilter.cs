namespace Domain.Filters;

public class CustomerFilter
{
    public string? Name { get; set; }
    public int PageSize { get; set; }
    public int PagesNumber { get; set; }
}