namespace Domain.Filters;

public class ProductFilter
{
    public int? From { get; set; }
    public int? To { get; set; }
    public string? Name { get; set; }
    public decimal? PriceFrom { get; set; }
    public decimal? PriceTo { get; set; }
    public int PagesNumber { get; set; }
    public int PageSize { get; set; }
}