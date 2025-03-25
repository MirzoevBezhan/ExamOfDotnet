using System.Net;

namespace Domain.Responses;

public class Response<T>
{
    public bool IsSuccess { get; set; }
    public T? data { get; set; }
    public HttpStatusCode statusCode { get; set; }
    public string Message { get; set; }

    public Response(HttpStatusCode httpStatusCode, string Message)
    {
        this.IsSuccess = false;
        this.statusCode = httpStatusCode;
        this.Message = Message;
    }
    public Response(T data)
    {
        this.IsSuccess = true;
        this.statusCode = HttpStatusCode.OK;
        this.data = data;
    }
}
