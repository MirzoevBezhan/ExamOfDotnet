namespace Domain.Filters;

public class CarFilter
{
    public string? Model { get; set; }
    public string? Manufacturer { get; set; }
    public int? From { get; set; }
    public int? To { get; set; }
    public decimal? PriceFrom { get; set; }
    public decimal? PriceTo { get; set; }
    
    public int PagesNumber { get; set; }
    public int PageSize { get; set; }
}