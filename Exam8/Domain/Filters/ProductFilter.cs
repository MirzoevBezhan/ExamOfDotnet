namespace Domain.Filters;

public class ProductFilter
{
    public string? Name { get; set; }
    public decimal? From  { get; set; }
    public decimal? To  { get; set; }
    public int PageSize  { get; set; }
    public int PagesNum  { get; set; }
}