using System.Net;

namespace Domain.Responces;

public class Responce<T>
{
    public bool IsSuccsses { get; set; }
    public T data { get; set; }
    public HttpStatusCode code { get; set; }
    public string message { get; set; }

    public Responce(HttpStatusCode httpStatusCode, string message)
    {
        this.IsSuccsses = false;
        this.code = httpStatusCode;
        this.message = message;
    }
    public Responce(T data)
    {
        this.code = HttpStatusCode.OK;
        this.IsSuccsses = true;
        this.data = data;
    }

}
