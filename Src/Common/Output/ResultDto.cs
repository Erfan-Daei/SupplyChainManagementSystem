using System.Net;

namespace Common.Output
{
    //centeral generic dto for result output of Application and Presentation and Persistence
    public class ResultDto<T>
    {
        public bool IsSuccess { get; set; } = false;
        public string? Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public T? Data { get; set; }
    }

    ////centeral "non" generic dto for result output of Application and Presentation and Persistence
    public class ResultDto
    {
        public bool IsSuccess { get; set; } = false;
        public string? Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }

}
