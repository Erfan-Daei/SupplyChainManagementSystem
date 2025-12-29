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

        //successful creator method
        public static ResultDto<T> Succeeded(T data, string? message, HttpStatusCode httpStatusCode)
            => new() { Data = data, IsSuccess = true, Message = message, StatusCode = httpStatusCode };

        //failed creator method
        public static ResultDto<T> Failed(string? message, HttpStatusCode httpStatusCode)
            => new() { Data = default, IsSuccess = false, Message = message, StatusCode = httpStatusCode };
    }

    ////centeral "non" generic dto for result output of Application and Presentation and Persistence
    public class ResultDto
    {
        public bool IsSuccess { get; set; } = false;
        public string? Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        //successful creator method
        public static ResultDto Succeeded(string? message, HttpStatusCode httpStatusCode)
            => new() { IsSuccess = true, Message = message, StatusCode = httpStatusCode };

        //failed creator method
        public static ResultDto Failed(string? message, HttpStatusCode httpStatusCode)
            => new() { IsSuccess = false, Message = message, StatusCode = httpStatusCode };
    }

}
