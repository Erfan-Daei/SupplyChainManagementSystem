using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Persistence.ExceptionHandler.DatabaseExceptionHandler
{
    public static class DatabaseExceptionHandler
    {
        //custom handler to check EF proccess Exception and give proper Message and StatusCode
        public static void Handle(Exception ex)
        {
            throw ex switch
            {
                ArgumentNullException _ =>
                    new ArgumentNullException("ورودی معتبر نیست. لطفاً اطلاعات را بررسی کنید.", ex),

                InvalidOperationException _ =>
                    new InvalidOperationException("عملیات نامعتبر بود.", ex),

                TimeoutException _ =>
                    new TimeoutException("زمان اجرای عملیات دیتابیس به پایان رسید.", ex),

                SqlException _ =>
                    new Exception("خطای دیتابیس رخ داد. لطفاً بعداً تلاش کنید.", ex),

                DbUpdateException _ =>
                    new Exception("ذخیره‌سازی در دیتابیس با مشکل مواجه شد.", ex),

                Exception _ =>
                    new Exception("خطای ناشناخته رخ داد.", ex),
            };
        }
    }
}
