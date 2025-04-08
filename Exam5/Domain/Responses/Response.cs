    using System.Net;

    namespace Domain.Responces;

    public class Response<T>
    {
        public bool IsSuccsses { get; set; }
        public T Data { get; set; }
        public HttpStatusCode Code { get; set; }
        public string Message { get; set; }

        public Response(HttpStatusCode httpStatusCode, string message)
        {
            this.IsSuccsses = false;
            this.Code = httpStatusCode;
            this.Message = message;
        }
        public Response(T data)
        {
            this.Code = HttpStatusCode.OK;
            this.IsSuccsses = true;
            this.Data = data;
        }

    }