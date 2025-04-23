namespace Domain.Filters;

public class SupplierFilter
{
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public int PageSize { get; set; }
    public int PagesNum { get; set; }
}
