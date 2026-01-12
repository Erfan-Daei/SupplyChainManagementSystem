using Common.Output;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Persistence.DatabaseManagement.ExceptionHandler.DatabaseExceptionHandler
{
    public static class DatabaseExceptionHandler
    {
        //custom handler to check EF proccess Exception and give proper Message and StatusCode
        public static void Handle(Exception ex) => throw ex switch
        {
            ArgumentNullException _ =>
                    new ArgumentNullException(DatabaseExceptionMessageLibrary.ArgumentNull, ex),

            InvalidOperationException _ =>
                new InvalidOperationException(DatabaseExceptionMessageLibrary.Invalidoperation, ex),

            TimeoutException _ =>
                new TimeoutException(DatabaseExceptionMessageLibrary.TimeOut, ex),

            SqlException _ =>
                new Exception(DatabaseExceptionMessageLibrary.Sql, ex),

            DbUpdateException _ =>
                new Exception(DatabaseExceptionMessageLibrary.DbUpdate, ex),

            Exception _ =>
                new Exception(DatabaseExceptionMessageLibrary.Unknown, ex),
        };
    }
}
