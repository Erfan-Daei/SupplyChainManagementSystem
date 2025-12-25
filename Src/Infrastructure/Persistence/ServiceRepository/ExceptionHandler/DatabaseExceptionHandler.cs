using Common.Output;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Persistence.ServiceRepository.ExceptionHandler
{
    public static class DatabaseExceptionHandler
    {
        //custom handler to check EF proccess Exception and give proper Message and StatusCode
        public static ResultDto Handle(Exception ex)
        {
            return ex switch
            {
                ArgumentNullException => new ResultDto
                {
                    IsSuccess = false,
                    Message = "ورودی معتبر نیست. لطفاً اطلاعات را بررسی کنید.",
                    StatusCode = HttpStatusCode.BadRequest   // 400
                },
                InvalidOperationException => new ResultDto
                {
                    IsSuccess = false,
                    Message = "عملیات نامعتبر بود.",
                    StatusCode = HttpStatusCode.Conflict   // 409
                },
                TimeoutException => new ResultDto
                {
                    IsSuccess = false,
                    Message = "زمان اجرای عملیات دیتابیس به پایان رسید.",
                    StatusCode = HttpStatusCode.RequestTimeout   // 408
                },
                SqlException => new ResultDto
                {
                    IsSuccess = false,
                    Message = "خطای دیتابیس رخ داد. لطفاً بعداً تلاش کنید.",
                    StatusCode = HttpStatusCode.InternalServerError   // 500
                },
                DbUpdateException => new ResultDto
                {
                    IsSuccess = false,
                    Message = "ذخیره‌سازی در دیتابیس با مشکل مواجه شد.",
                    StatusCode = HttpStatusCode.InternalServerError   // 500
                },
                _ => new ResultDto
                {
                    IsSuccess = false,
                    Message = "خطای ناشناخته رخ داد.",
                    StatusCode = HttpStatusCode.InternalServerError   // 500
                }
            };
        }
    }
}
