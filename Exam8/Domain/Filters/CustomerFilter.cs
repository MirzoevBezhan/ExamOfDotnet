namespace Domain.Filters;

public class CustomerFilter
{
    public string? FullName { get; set; }   
    public string? PhoneNumber { get; set; }   
    public string? Email { get; set; }   
    public int PageSize { get; set; }   
    public int PagesNum { get; set; }   
}
