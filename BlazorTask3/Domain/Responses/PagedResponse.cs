using System.Net;
using Domain.Responces;

namespace Domain.Responses;

public class PagedResponse<T> : Response<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }

    public PagedResponse(T data , int pageNumber , int pageSize , int totalRecords) : base(data)
    {
        this.PageNumber = pageNumber;
        this.PageSize = pageSize;
        this.TotalRecords = totalRecords;
        this.TotalPages = (int)Math.Ceiling(totalRecords / (double)PageSize);
    }

    public PagedResponse(HttpStatusCode code , string message) : base(code , message)
    {
        
    }
}