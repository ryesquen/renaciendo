using System.Net;

namespace TiendaAPI.Models
{
    public class BaseResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public List<string>? ErrorsMessages { get; set; }
        public object? Result { get; set; }
    }
}
