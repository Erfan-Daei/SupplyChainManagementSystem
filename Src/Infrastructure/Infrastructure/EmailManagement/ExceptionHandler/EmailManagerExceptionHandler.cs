using Common.Output;
using System.Net;
using System.Net.Mail;

namespace Infrastructure.EmailManagement.ExceptionHandler
{
    //custom handler to check EmailSender proccess Exception and give proper Message and StatusCode
    public static class EmailManagerExceptionHandler
    {
        public static ResultDto Handle(Exception ex)
        {
            return ex switch
            {
                SmtpFailedRecipientsException => new ResultDto
                {
                    IsSuccess = false,
                    Message = "ارسال به چند گیرنده نامعتبر شکست خورد: " + ex.Message,
                    StatusCode = HttpStatusCode.BadRequest   // 400
                },
                SmtpFailedRecipientException => new ResultDto
                {
                    IsSuccess = false,
                    Message = "ارسال به گیرنده نامعتبر شکست خورد: " + ex.Message,
                    StatusCode = HttpStatusCode.BadRequest   // 400
                },
                SmtpException => new ResultDto
                {
                    IsSuccess = false,
                    Message = "خطای SMTP (اتصال یا احراز هویت): " + ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError   // 500
                },
                FormatException => new ResultDto
                {
                    IsSuccess = false,
                    Message = "فرمت ایمیل اشتباه است: " + ex.Message,
                    StatusCode = HttpStatusCode.UnprocessableEntity   // 422
                },
                InvalidOperationException => new ResultDto
                {
                    IsSuccess = false,
                    Message = "وضعیت نادرست در ارسال ایمیل: " + ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError   // 500
                },
                Exception => new ResultDto
                {
                    IsSuccess = false,
                    Message = "خطای ناشناخته در ارسال ایمیل: " + ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError   // 500
                }
            };
        }
    }
}
