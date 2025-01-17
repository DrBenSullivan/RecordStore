using System.Text;
using RecordStore.Core.Exceptions;
using RecordStore.Core.Models;

namespace RecordStore.Api.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (DatabaseException<Album> ex)
            {
                var error = Encoding.UTF8.GetBytes(ex.Value.ToString() + ex.Message);
                await context.Response.Body.WriteAsync(error);
            }
            return;
        }
    }
}
