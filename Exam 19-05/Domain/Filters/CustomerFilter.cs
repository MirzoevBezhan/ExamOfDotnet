namespace Domain.Filters;

public class CustomerFilter
{
    public string? FullName { get; set; }   
    public string? Email { get; set; }
    
    public int PagesNumber { get; set; }
    public int PageSize { get; set; }
}