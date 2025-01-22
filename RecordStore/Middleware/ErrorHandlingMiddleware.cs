using Microsoft.Data.SqlClient;

namespace RecordStore.Api.Middleware
{
	public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);

            }
            catch (Exception ex) when (ex.InnerException is SqlException e && (e.Number == 2601 || e.Number == 2627))
            {
                // Unique index violation
                var errorMessage = "An entry with the given key values already exists in the database.";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(errorMessage);
            }
            catch (Exception ex) when (ex.InnerException is SqlException e && e.Number == 547)
            {
                // Foreign key violation
                var errorMessage = "The entry contained unrecognised values. Please try again.";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(errorMessage);
            }
            catch (Exception ex) when (ex.InnerException is SqlException e)
            {
                context.Response.StatusCode = e.Number switch
                {
                    53 => StatusCodes.Status503ServiceUnavailable,  // Unreachable db
                    18456 => StatusCodes.Status401Unauthorized,     // Failed login
                    _ => StatusCodes.Status500InternalServerError   // Default
                };

                await context.Response.WriteAsync("A database error occurred. Please try again later.");
            }
        }
    }
}
