namespace Domain.Filters;

public class BranchFilter
{
    public string? Name { get; set; }
    public string? Location { get; set; }
    
    public int PagesNumber { get; set; }
    public int PageSize { get; set; }
}